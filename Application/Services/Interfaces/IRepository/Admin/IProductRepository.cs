using Application.DTOModels.Models.Admin;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product productModel);

        Task<Product> UpdateProductAsync(int productId, ProductDto productModel);

        Task<Product> DeleteProductAsync(int productId);
    }
}
