﻿using Application.DtoModels.Models.Admin;
using Application.DtoModels.Response.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IUserService
    {
        Task<UserResponseDto> EditUserAsync(Guid userId, UserDto model);

        Task<UserResponseDto> DeleteUserAsync(Guid userId);
    }
}