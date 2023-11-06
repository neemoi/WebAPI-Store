using Application.DtoModels.Models.Pagination;
using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Models.Admin.Product;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPaginationService _paginationService;

        public ProductController(IProductService productService, IPaginationService paginationService)
        {
            _productService = productService;
            _paginationService = paginationService;
        }

        [HttpGet("Admin/Product/")]
        public async Task<IActionResult> GetProductWithPaginationAsync([FromQuery] ProductQueryParametersDto productModel)
        {
            return Ok(await _paginationService.GetProductWithPaginationAsync(productModel));
        }

        [HttpPost("Admin/Product/")]
        public async Task<IActionResult> CreateProductAsync([FromQuery] ProductCreateDto productModel)
        {
            return Ok(await _productService.CreateProductAsync(productModel));
        }

        [HttpPut("Admin/Product/")]
        public async Task<IActionResult> UpdateProductAsync([FromQuery] ProductEditDto productModel)
        {
            return Ok(await _productService.UpdateProductAsync(productModel.Id, productModel));
        }

        [HttpDelete("Admin/Product/{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            return Ok(await _productService.DeleteProductAsync(id));
        }

    }
}
