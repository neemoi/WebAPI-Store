using Application.DTOModels.Models.Admin.Payment;
using Application.DTOModels.Response.Admin;
using AutoMapper;
using WebAPIKurs;

namespace Application.MappingProfile.Admin
{
    public class MappingPayments : Profile
    {
        public MappingPayments()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentResponseDto>();

            CreateMap<Payment, PaymentCreateDto>();
            CreateMap<PaymentCreateDto, Payment>();

            CreateMap<Payment, PaymentEditDto>();
            CreateMap<PaymentEditDto, Payment>();
        }
    }
}
