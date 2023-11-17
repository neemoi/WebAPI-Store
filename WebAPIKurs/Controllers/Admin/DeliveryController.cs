using Application.Services.Interfaces.IServices.Admin;
using Application.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Models.Admin.Delivery;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIKurs.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IPaginationService _paginationService;

        public DeliveryController(IDeliveryService deliveryService, IPaginationService paginationService)
        {
            _deliveryService = deliveryService;
            _paginationService = paginationService;
        }

        [HttpGet("Admin/Delivery/")]
        public async Task<IActionResult> GetDeliveryWithPaginationAsync([FromQuery] DeliveryQueryParametersDto deliveryModel)
        {
            return Ok(await _paginationService.GetDeliveryWithPaginationAsync(deliveryModel));
        }

        [HttpPost("Admin/Delivery/")]
        public async Task<IActionResult> CreateDeliveryAsync([FromQuery] DeliveryCreateDto deliveryModel)
        {
            return Ok(await _deliveryService.CreateDeliveryAsync(deliveryModel));
        }

        [HttpPut("Admin/Delivery/")]
        public async Task<IActionResult> EditDeliveryAsync([FromQuery] DeliveryEditDto deliveryModel)
        {
            return Ok(await _deliveryService.EditDeliveryAsync(deliveryModel));
        }

        [HttpDelete("Admin/Delivery/")]
        public async Task<IActionResult> DeleteDeliveryAsync([FromQuery] int id)
        {
            return Ok(await _deliveryService.DeleteDeliveryAsync(id));
        }
    }
}
