using Application.Services.Interfaces.IRepository;
using Application.Services.Interfaces.IServices;
using Application.Services.UnitOfWork;
using WebAPIKurs;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPaginationRepository PaginationRepository { get; }

        private readonly WebsellContext _websellContext;

        public UnitOfWork(IPaginationRepository paginationRepository, WebsellContext websellContext)
        {
            PaginationRepository = paginationRepository;
            _websellContext = websellContext;
        }

        public Task SaveChangesAsync()
        {
            return _websellContext.SaveChangesAsync();
        }
    }
}
