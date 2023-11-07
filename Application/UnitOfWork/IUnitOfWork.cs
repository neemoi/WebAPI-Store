using Application.Services.Interfaces.IRepository.Admin;

namespace Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPaginationRepository PaginationRepository { get; }

        IProductRepository ProductRepository { get; }

        IPaymentsRepository PaymentsRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IDeliveryRepository DeliveryRepository { get; }
        Task SaveChangesAsync();
    }
}
