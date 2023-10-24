using AutoMapper;
using Application.DTOModels.Models.Admin;
using Application.DTOModels.Response.Admin;
using WebAPIKurs;

namespace Application.MappingProfile.Admin
{
    public class MappingAccount : Profile
    {
        public MappingAccount()
        {
            //Account
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name));
            CreateMap<User, LoginResponseDto>();
            CreateMap<User, RegisterResponseDto>();
            CreateMap<User, LogoutResponseDto>();
        }
    }
}