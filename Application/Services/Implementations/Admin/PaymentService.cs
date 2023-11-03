﻿using Application.DTOModels.Models.Admin;
using Application.DTOModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.UnitOfWork;
using AutoMapper;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaymentResponseDto> CreatePaymentAsync(Payment paymentModel)
        {
            var payment = _mapper.Map<Payment>(paymentModel);
            
            var result = await _unitOfWork.PaymentsRepository.CreatePaymentAsync(payment);

            return _mapper.Map<PaymentResponseDto>(result);
        }

        public async Task<PaymentResponseDto> DeletePaymentAsync(int paymentId)
        {
            var result = await _unitOfWork.PaymentsRepository.DeletePaymentAsync(paymentId);

            return _mapper.Map<PaymentResponseDto>(result);
        }

        public async Task<PaymentResponseDto> UpdatePaymentAsync(PaymentDto paymentModel)
        {
            var result = await _unitOfWork.PaymentsRepository.UpdatePaymentAsync(paymentModel);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PaymentResponseDto>(result);
        }
    }
}
