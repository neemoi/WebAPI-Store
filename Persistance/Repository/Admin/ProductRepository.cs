using Application.DTOModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WebAPIKurs;

namespace Persistance.Repository.Admin
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ProductRepository(WebsellContext websellContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _websellContext = websellContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var result = await _websellContext.Products.AddAsync(product);

            if (result != null)
            {
                await _websellContext.SaveChangesAsync();

                return product;
            }
            else
            {
                throw new Exception("Error create product");
            }
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            var result = await _websellContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (result != null)
            {
                _websellContext.Products.Remove(result);

                await _websellContext.SaveChangesAsync();

                return result;
            }
            else
            {
                throw new Exception("Error delete product");
            }
        }

        public async Task<Product> UpdateProductAsync(int productId, ProductDto productModel)
        {
            var product = await _websellContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null) 
            {
                _mapper.Map(productModel, product);

                await _websellContext.SaveChangesAsync();

                return product;
            }
            else
            {
                throw new Exception("Error update product");
            }
        }
    }
}
