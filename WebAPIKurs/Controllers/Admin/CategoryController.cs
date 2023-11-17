using Application.DTOModels.Models.Admin.Pagination;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Application.DTOModels.Models.Admin.Category;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIKurs.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPaginationService _paginationService;

        public CategoryController(ICategoryService categoryService, IPaginationService paginationService)
        {
            _categoryService = categoryService;
            _paginationService = paginationService;
        }

        [HttpGet("Admin/Category/")]
        public async Task<IActionResult> GetCategoryWithPaginationAsync([FromQuery] CategoryQueryParametersDto productModel)
        {
            return Ok(await _paginationService.GetCategoryWithPaginationAsync(productModel));
        }

        [HttpPost("Admin/Category/")]
        public async Task<IActionResult> CreateCategoryAsync([FromQuery] CategoryCreateDto productModel)
        {
            return Ok(await _categoryService.CreateCategoryAsync(productModel));
        }

        [HttpPut("Admin/Category/")]
        public async Task<IActionResult> UpdateCategoryAsync([FromQuery] CategoryEditDto productModel)
        {
            return Ok(await _categoryService.EditCategoryAsync(productModel));
        }

        [HttpDelete("Admin/Category/")]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery] int id)
        {
            return Ok(await _categoryService.DeleteCategoryAsync(id));
        }
    }
}
