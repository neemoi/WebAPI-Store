using Application.DTOModels.Models.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs;

namespace Persistance.Repository.Admin
{
    public class PaymentRepository : IPaymentsRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly IMapper _mapper;

        public PaymentRepository(WebsellContext websellContext, IMapper mapper)
        {
            _websellContext = websellContext;
            _mapper = mapper;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            var result = await _websellContext.Payments.AddAsync(payment);

            if (result != null)
            {
                await _websellContext.SaveChangesAsync();

                return payment;
            }
            else
            {
                throw new Exception("Error create payment");
            }
        }

        public async Task<Payment> DeletePaymentAsync(int paymentId)
        {
            var result = await _websellContext.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);

            if (result != null)
            {
                _websellContext.Payments.Remove(result);

                await _websellContext.SaveChangesAsync();

                return result;
            }
            else
            {
                throw new Exception("Error delete payment");
            }
        }

        public async Task<Payment> UpdatePaymentAsync(PaymentDto paymentModel)
        {
            var result = await _websellContext.Payments.FirstOrDefaultAsync(p => p.Id == paymentModel.Id);

            if (result != null) 
            {
                _mapper.Map(paymentModel, result);

                await _websellContext.SaveChangesAsync();

                return result;
            }
            else
            {
                throw new Exception("Error update payment");
            }
        }
    }
}
