using Application.DTOModels.Models.Admin;
using Application.DTOModels.Response.Admin;
using WebAPIKurs;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(Payment paymentModel);

        Task<PaymentResponseDto> UpdatePaymentAsync(PaymentDto paymentModel);

        Task<PaymentResponseDto> DeletePaymentAsync(int paymentId);
    }
}
