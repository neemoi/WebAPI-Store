using Application.DTOModels.Models.Admin.Product;
using Application.DTOModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.UnitOfWork;
using AutoMapper;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);

            var result = await _unitOfWork.ProductRepository.CreateProductAsync(product);

            return _mapper.Map<ProductResponseDto>(result);
        }

        public async Task<ProductResponseDto> DeleteProductAsync(int productId)
        {
            var result = await _unitOfWork.ProductRepository.DeleteProductAsync(productId);

            return _mapper.Map<ProductResponseDto>(result);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(int productId, ProductEditDto productModel)
        {
            var result = await _unitOfWork.ProductRepository.EditProductAsync(productId, productModel);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductResponseDto>(result);
        }
    }
}
