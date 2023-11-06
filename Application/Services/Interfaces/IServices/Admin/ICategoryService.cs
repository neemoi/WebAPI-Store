﻿using Application.DTOModels.Models.Admin.Category;
using Application.DTOModels.Response.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto categoryModel);

        Task<CategoryResponseDto> UpdateCategoryAsync(int categorytId, CategoryEditDto categoryModel);

        Task<CategoryResponseDto> DeleteCategoryAsync(int categorytId);
    }
}
