using AutoMapper;
using WebAPIKurs;
using Application.DTOModels.Response.Admin;
using Application.DTOModels.Models.Admin.Product;

namespace Application.MappingProfile.Admin
{
    public class MappingProducts : Profile
    {
        public MappingProducts()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductResponseDto>();

            CreateMap<Product, ProductCreateDto>();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Product, ProductEditDto>();
            CreateMap<ProductEditDto, Product>();
        }
    }
}