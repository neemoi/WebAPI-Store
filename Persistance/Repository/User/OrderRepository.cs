using Application.CustomException;
using Application.DTOModels.Models.User.Order;
using Application.DTOModels.Response.User;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using WebAPIKurs;

namespace Persistance.Repository.User
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<Order> _logger;
        private readonly IMapper _mapper;

        public OrderRepository(WebsellContext websellContext, IHttpContextAccessor httpContextAccessor, ILogger<Order> logger, IMapper mapper)
        {
            _websellContext = websellContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto orderModel)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");

                var order = new Order
                {
                    UserId = userId,
                    PaymentId = orderModel.PaymentId,
                    DeliverId = orderModel.DeliverId

                } ?? throw new CustomRepositoryException("Order not found", "NOT_FOUND_ERROR_CODE");

                await _websellContext.AddAsync(order);

                foreach (var productId in orderModel.ListProductId)
                {
                    var product = await _websellContext.Products.FindAsync(productId) ?? throw new CustomRepositoryException("Product not found", "NOT_FOUND_ERROR_CODE");

                    var orderItem = new Orderitem
                    {
                        Order = order,
                        ProductId = productId,
                        Quantity = orderModel.Quantity
                    };

                    _websellContext.Orderitems.Add(orderItem);
                }

                order.TotalPrice = await CalculateTotalPriceAsync(orderModel.PaymentId, orderModel.DeliverId, orderModel.ListProductId, orderModel.Quantity);

                await _websellContext.SaveChangesAsync();

                await LoadOrderDetailsAsync(order);

                return order;
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error in OrderRepository.CreateOrderAsync: ", ex.Message);

                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }

        public async Task<Order> DeleteOrderAsync(int orderId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");

                var orderItemsToDelete = await _websellContext.Orderitems
                    .Where(oi => oi.OrderId == orderId)
                    .Include(oi => oi.Product.Category)
                    .Include(oi => oi.Order.Payments)
                    .Include(oi => oi.Order.Deliveries)
                    .ToListAsync() ?? throw new CustomRepositoryException("OrderItems not found", "NOT_FOUND_ERROR_CODE");

                if (orderItemsToDelete != null && orderItemsToDelete.Any())
                {
                    _websellContext.Orderitems.RemoveRange(orderItemsToDelete);

                    var orderToDelete = await _websellContext.Orders
                        .Where(o => o.Id == orderId)
                        .FirstOrDefaultAsync();

                    if (orderToDelete != null && orderToDelete.UserId == userId)
                    {
                        _websellContext.Orders.Remove(orderToDelete);

                        await _websellContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new CustomRepositoryException("Order deletion error. Order ID not found", "NOT_FOUND_ERROR_CODE");
                    }

                    return orderToDelete;
                }
                else
                {
                    throw new CustomRepositoryException("No products found to delete for the specified order ID", "NOT_FOUND_ERROR_CODE");
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error in OrderRepository.DeleteOrderAsync: ", ex.Message);

                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }

        public async Task<Order> EditOrderAsync(int orderId, OrderEditDto orderModel)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");

                var order = await _websellContext.Orders
                    .Include(o => o.Orderitems)
                    .FirstOrDefaultAsync(p => p.Id == orderId && p.UserId == userId) ?? throw new CustomRepositoryException("Order not found", "NOT_FOUND_ERROR_CODE");

                _mapper.Map(orderModel, order);

                if (orderModel.ListProductId != null && orderModel.ListProductId.Any())
                {
                    order.Orderitems.Clear();

                    foreach (var productId in orderModel.ListProductId)
                    {
                        var orderItem = new Orderitem
                        {
                            ProductId = productId,
                            Quantity = orderModel.Quantity
                        };

                        order.Orderitems.Add(orderItem);
                    }
                }
                else
                {
                    throw new CustomRepositoryException("Order not found", "INVALID_INPUT_DATA");
                }

                order.TotalPrice = await CalculateTotalPriceAsync(orderModel.PaymentId, orderModel.DeliverId, orderModel.ListProductId, orderModel.Quantity);

                await LoadOrderDetailsAsync(order);

                await _websellContext.SaveChangesAsync();

                return order;
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error in OrderRepository.EditOrderAsync: ", ex.Message);

                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }

        private async Task LoadOrderDetailsAsync(Order order)
        {
            await _websellContext.Entry(order)
                .Collection(o => o.Orderitems)
                .Query()
                .Include(oi => oi.Product)
                .LoadAsync();

            await _websellContext.Entry(order)
                .Reference(o => o.Payments)
                .LoadAsync();

            await _websellContext.Entry(order)
                .Reference(o => o.Deliveries)
                .LoadAsync();

            var productPrices = order.Orderitems.Select(oi => oi.Product.Price).ToList() ?? throw new CustomRepositoryException("Price name not found", "NOT_FOUND_ERROR_CODE");
            var colorList = order.Orderitems.Select(oi => oi.Product.Color).ToList() ?? throw new CustomRepositoryException("Color name not found", "NOT_FOUND_ERROR_CODE");
            var memoryList = order.Orderitems.Select(oi => oi.Product.Memory).ToList() ?? throw new CustomRepositoryException("Memory name not found", "NOT_FOUND_ERROR_CODE");
            var nameList = order.Orderitems.Select(oi => oi.Product.Name).ToList() ?? throw new CustomRepositoryException("Product name not found", "NOT_FOUND_ERROR_CODE");

            var orderResponse = _mapper.Map<OrderResponseDto>(order);
            orderResponse.ListProductPrices = productPrices;
            orderResponse.ListProductName = nameList;
            orderResponse.ListProductColor = colorList;
            orderResponse.ListProductMemory = memoryList;
        }

        private async Task<decimal> CalculateTotalPriceAsync(int paymentId, int deliverId, List<int> productIds, int quantity)
        {
            decimal totalPrice = 0;

            foreach (var productId in productIds)
            {
                var product = await _websellContext.Products.FindAsync(productId) ?? throw new CustomRepositoryException("Product not found", "NOT_FOUND_ERROR_CODE");

                totalPrice += product.Price * quantity;
            }

            var payment = await _websellContext.Payments.FindAsync(paymentId) ?? throw new CustomRepositoryException("Payment ID not found", "NOT_FOUND_ERROR_CODE");
            var delivery = await _websellContext.Deliveries.FindAsync(deliverId) ?? throw new CustomRepositoryException("Delivery ID not found", "NOT_FOUND_ERROR_CODE");

            return totalPrice + payment.Amount + delivery.Price;
        }
    }
}
