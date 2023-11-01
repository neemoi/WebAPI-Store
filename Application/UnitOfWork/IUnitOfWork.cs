using Application.Services.Interfaces.IRepository;
using Application.Services.Interfaces.IServices;

namespace Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPaginationRepository PaginationRepository { get; }

        Task SaveChangesAsync();
    }
}
