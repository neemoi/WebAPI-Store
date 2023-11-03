using Application.DTOModels.Models.Admin;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IPaymentsRepository
    {
        Task<Payment> CreatePaymentAsync(Payment payment);

        Task<Payment> UpdatePaymentAsync(PaymentDto paymentModel);

        Task<Payment> DeletePaymentAsync(int paymentId);
    }
}
