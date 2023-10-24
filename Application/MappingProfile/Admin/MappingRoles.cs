using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Application.DtoModels.Response.Admin;
using WebAPIKurs;

namespace Application.MappingProfile.Admin
{
    public class MappingRoles : Profile
    {
        public MappingRoles()
        {
            CreateMap<RoleResponseDto, User>();
            CreateMap<IdentityRole, RoleResponseDto>();
        }
    }
}