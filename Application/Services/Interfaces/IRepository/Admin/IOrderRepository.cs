using Application.DTOModels.Models.User.Order;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(OrderCreateDto orderModel);

        Task<Order> EditOrderAsync(int orderId, OrderEditDto orderModel);

        Task<Order> DeleteOrderAsync(int orderId);
    }
}
