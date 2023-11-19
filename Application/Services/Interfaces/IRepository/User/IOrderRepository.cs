using Application.DTOModels.Models.User.Order;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository.User
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(OrderCreateDto orderModel);

        Task<Order> EditOrderAsync(OrderEditDto orderModel);

        Task<Order> DeleteOrderAsync(int orderId);
    }
}
