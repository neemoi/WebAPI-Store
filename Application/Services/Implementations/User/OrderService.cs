using Application.DTOModels.Models.User.Order;
using Application.DTOModels.Response.User;
using Application.Services.Interfaces.IServices.User;
using Application.Services.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebAPIKurs;

namespace Application.Services.Implementations.User
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<Order> _logger;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Order> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderModel)
        {
            try
            {
                _logger.LogInformation("Attempt to create an order: {@OrderCreateDto}", orderModel);

                var result = await _unitOfWork.OrderRepository.CreateOrderAsync(orderModel);

                _logger.LogInformation("Order successfully created: {@Order}", result);

                return _mapper.Map<OrderResponseDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when creating an order: {@OrderCreateDto}", orderModel);
                throw;
            }
        }
        
        public async Task<OrderResponseDto> DeleteOrderAsync(int orderId)
        {
            try
            {
                _logger.LogInformation("Attempt to delete an order: {@orderId}", orderId);
                
                var result = await _unitOfWork.OrderRepository.DeleteOrderAsync(orderId);

                _logger.LogInformation("Order successfully deleted: {@Order}", result);

                return _mapper.Map<OrderResponseDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when creating an order: {@odrerId}", orderId);
                throw;
            }
        }

        public async Task<OrderResponseDto> EditOrderAsync(int orderId, OrderEditDto orderModel)
        {
            try
            {
                _logger.LogInformation("Attempt to edit an order: {@OrderEditDto}", orderModel);

                var result = await _unitOfWork.OrderRepository.EditOrderAsync(orderId, orderModel);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Order successfully deleted: {@Order}", result);

                return _mapper.Map<OrderResponseDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when creating an order: {@odrerId}", orderId);
                throw;
            }
        }
    }
}
