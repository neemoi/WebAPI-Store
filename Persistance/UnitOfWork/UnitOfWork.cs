using Application.Services.Interfaces.IRepository.Admin;
using Application.Services.Interfaces.IServices;
using Application.Services.UnitOfWork;
using WebAPIKurs;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPaginationRepository PaginationRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPaymentsRepository PaymentsRepository { get; }

        private readonly WebsellContext _websellContext;

        public UnitOfWork(IPaginationRepository paginationRepository, IProductRepository productRepository, IPaymentsRepository paymentsRepository, WebsellContext websellContext)
        {
            PaginationRepository = paginationRepository;
            ProductRepository = productRepository;
            PaymentsRepository = paymentsRepository;
            _websellContext = websellContext;
        }

        public Task SaveChangesAsync()
        {
            return _websellContext.SaveChangesAsync();
        }
    }
}
