using Application.Services.Interfaces.IRepository.Admin;

namespace Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPaginationRepository PaginationRepository { get; }

        IProductRepository ProductRepository { get; }

        IPaymentsRepository PaymentsRepository { get; }

        Task SaveChangesAsync();
    }
}
