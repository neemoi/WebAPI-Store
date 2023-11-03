using AutoMapper;
using WebAPIKurs;
using Application.DTOModels.Response.Admin;
using Application.DTOModels.Models.Admin;

namespace Application.MappingProfile.Admin
{
    public class MappingProducts : Profile
    {
        public MappingProducts()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}