﻿using Application.DTOModels.Models.User;
using Application.DTOModels.Response.User;

namespace Application.Services.Interfaces.IServices.User
{
    public interface IProfileService
    {
        Task<EditProfileResposneDto> EditProfileAsync(EditProfileDto model);

        Task<EditProfileResposneDto> GetAllInfoAsync();
    }
}
