using Application.DTOModels.Models.User.Order;
using Application.DTOModels.Response.User;

namespace Application.Services.Interfaces.IServices.User
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderModel);

        Task<OrderResponseDto> EditOrderAsync(OrderEditDto orderModel);

        Task<OrderResponseDto> DeleteOrderAsync(int orderId);
    }
}
