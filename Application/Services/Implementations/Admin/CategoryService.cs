using Application.DTOModels.Models.Admin.Category;
using Application.DTOModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.UnitOfWork;
using AutoMapper;
using Domain.Models;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);

            var result = await _unitOfWork.CategoryRepository.CreateCategoryAsync(category);

            return _mapper.Map<CategoryResponseDto>(result);
        }

        public async Task<CategoryResponseDto> DeleteCategoryAsync(int categorytId)
        {
            var result = await _unitOfWork.CategoryRepository.DeleteCategoryAsync(categorytId);

            return _mapper.Map<CategoryResponseDto>(result);
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync(int categorytId, CategoryEditDto categoryModel)
        {
            var result = await _unitOfWork.CategoryRepository.EditCategoryAsync(categorytId, categoryModel);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryResponseDto>(result);
        }
    }
}
