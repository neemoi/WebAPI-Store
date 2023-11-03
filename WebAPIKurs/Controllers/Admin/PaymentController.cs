using Application.DTOModels.Models.Admin;
using Application.DTOModels.Models.Admin.Pagination;
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

        [HttpPost("Admin/Payment/")]
        public async Task<IActionResult> CreatePaymentAsync(Payment paymentModel)
        {
            return Ok(await _paymentService.CreatePaymentAsync(paymentModel));
        }

        [HttpPut("Admin/Payment/")]
        public async Task<IActionResult> UpdatePaymentAsync(PaymentDto paymentModel)
        {
            return Ok(await _paymentService.UpdatePaymentAsync(paymentModel));
        }

        [HttpDelete("Admin/Payment/")]
        public async Task<IActionResult> DeletePaymentAsync(int paymentId)
        {
            return Ok(await _paymentService.DeletePaymentAsync(paymentId));
        }
    }
}
