using Application.DTOModels.Models.Admin;
using Application.DTOModels.Response.Admin;
using WebAPIKurs;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateProductAsync(Product productModel);

        Task<ProductResponseDto> UpdateProductAsync(int productId, ProductDto productModel);

        Task<ProductResponseDto> DeleteProductAsync(int productId);
    }
}
