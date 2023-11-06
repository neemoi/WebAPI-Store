using Application.DTOModels.Models.Admin.Category;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs;

namespace Persistance.Repository.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly IMapper _mapper;

        public CategoryRepository(WebsellContext websellContext, IMapper mapper)
        {
            _websellContext = websellContext;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var result = await _websellContext.Categorys.AddAsync(category);

            if (result != null)
            {
                await _websellContext.SaveChangesAsync();

                return category;
            }
            else
            {
                throw new Exception("Error create Category");
            }
        }

        public async Task<Category> DeleteCategoryAsync(int categorytId)
        {
            var result = await _websellContext.Categorys.FirstOrDefaultAsync(p => p.Id == categorytId);

            if (result != null)
            {
                _websellContext.Categorys.Remove(result);

                await _websellContext.SaveChangesAsync();

                return result;
            }
            else
            {
                throw new Exception("Error delete Category");
            }
        }

        public async Task<Category> EditCategoryAsync(int categorytId, CategoryEditDto categoryModel)
        {
            var category = await _websellContext.Categorys.FirstOrDefaultAsync(p => p.Id == categorytId);

            if (category != null)
            {
                _mapper.Map(categoryModel, category);

                await _websellContext.SaveChangesAsync();

                return category;
            }
            else
            {
                throw new Exception("Error update Category");
            }
        }
    }
}
