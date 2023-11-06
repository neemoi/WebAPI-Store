using Application.DTOModels.Models.Admin.Category;
using Application.DTOModels.Response.Admin;
using AutoMapper;
using Domain.Models;

namespace Application.MappingProfile.Admin
{
    public class MappingCategory : Profile
    {
        public MappingCategory()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryResponseDto>();

            CreateMap<Category, CategoryCreateDto>();
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<Category, CategoryEditDto>();
            CreateMap<CategoryEditDto, Category>();
        }
    }
}
