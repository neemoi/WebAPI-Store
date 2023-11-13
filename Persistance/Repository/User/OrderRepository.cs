using Application.DTOModels.Models.User.Order;
using Application.DTOModels.Response.User;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPIKurs;

namespace Persistance.Repository.User
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public OrderRepository(WebsellContext websellContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _websellContext = websellContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto orderModel)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User null");

                var order = new Order
                {
                    UserId = userId,
                    PaymentId = orderModel.PaymentId,
                    DeliverId = orderModel.DeliverId

                } ?? throw new Exception("Order null");

                await _websellContext.AddAsync(order);

                foreach (var productId in orderModel.ListProductId)
                {
                    var product = await _websellContext.Products.FindAsync(productId) ?? throw new Exception("Product null");

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Order> DeleteOrderAsync(int orderId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User null");

                var orderItemsToDelete = await _websellContext.Orderitems
                    .Where(oi => oi.OrderId == orderId)
                    .Include(oi => oi.Product.Category)
                    .Include(oi => oi.Order.Payments)
                    .Include(oi => oi.Order.Deliveries)
                    .ToListAsync();

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
                        throw new Exception("Order deletion error. Order ID not found");
                    }

                    return orderToDelete;
                }
                else
                {
                    throw new Exception("No products found to delete for the specified order ID");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Order> EditOrderAsync(int orderId, OrderEditDto orderModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User null");

            var order = await _websellContext.Orders
                .Include(o => o.Orderitems)
                .FirstOrDefaultAsync(p => p.Id == orderId && p.UserId == userId);

            if (order == null) { throw new Exception("Order edit error. Order ID not found or user does not have permission."); }
            
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

            order.TotalPrice = await CalculateTotalPriceAsync(orderModel.PaymentId, orderModel.DeliverId, orderModel.ListProductId, orderModel.Quantity);

            await LoadOrderDetailsAsync(order);

            await _websellContext.SaveChangesAsync();

            return order;
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

            var productPrices = order.Orderitems.Select(oi => oi.Product.Price).ToList() ?? throw new Exception("Price name not found");
            var colorList = order.Orderitems.Select(oi => oi.Product.Color).ToList() ?? throw new Exception("Color name not found");
            var memoryList = order.Orderitems.Select(oi => oi.Product.Memory).ToList() ?? throw new Exception("Memory name not found");
            var nameList = order.Orderitems.Select(oi => oi.Product.Name).ToList() ?? throw new Exception("Product name not found");

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
                var product = await _websellContext.Products.FindAsync(productId) ?? throw new Exception("Product null");

                totalPrice += product.Price * quantity;
            }

            var payment = await _websellContext.Payments.FindAsync(paymentId) ?? throw new Exception("Payment ID null");
            var delivery = await _websellContext.Deliveries.FindAsync(deliverId) ?? throw new Exception("Delivery ID null");

            return totalPrice + payment.Amount + delivery.Price;
        }
    }
}
