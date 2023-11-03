using Application.DtoModels.Models.Admin;
using Application.DtoModels.Response.Admin;
using AutoMapper;
using WebAPIKurs;

namespace Application.MappingProfile.Admin
{
    public class MappingUsers : Profile
    {
        public MappingUsers()
        {
            CreateMap<UserDto, CustomUser>();
            CreateMap<CustomUser, UserResponseDto>();
        }
    }
}