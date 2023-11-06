using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Models.Admin.Payment;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaginationService _paginationService;

        public PaymentController(IPaymentService paymentService, IPaginationService paginationService)
        {
            _paymentService = paymentService;
            _paginationService = paginationService;
        }


        [HttpGet("Admin/Payment/")]
        public async Task<IActionResult> GetPaymentsWithPaginationAsync([FromQuery] PaymentQueryParametersDto productModel)
        {
            return Ok(await _paginationService.GetPaymentsWithPaginationAsync(productModel));
        }

        [HttpPost("Admin/Payment/")]
        public async Task<IActionResult> CreatePaymentAsync(PaymentCreateDto paymentModel)
        {
            return Ok(await _paymentService.CreatePaymentAsync(paymentModel));
        }

        [HttpPut("Admin/Payment/")]
        public async Task<IActionResult> EditPaymentAsync(PaymentEditDto paymentModel)
        {
            return Ok(await _paymentService.EditPaymentAsync(paymentModel));
        }

        [HttpDelete("Admin/Payment/")]
        public async Task<IActionResult> DeletePaymentAsync(int id)
        {
            return Ok(await _paymentService.DeletePaymentAsync(id));
        }
    }
}
