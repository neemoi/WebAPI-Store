using Application.DTOModels.Models.User;
using Application.DTOModels.Response.User;
using Application.Services.Interfaces.IServices.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPIKurs;

namespace Application.Services.Implementations.User
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(IMapper mapper, UserManager<CustomUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EditProfileResposneDto> EditProfileAsync(EditProfileDto model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                        if (!changePasswordResult.Succeeded)
                        {
                            throw new Exception($"Error changing password: {string.Join(", ", changePasswordResult.Errors.Select(e => e.Description))}");
                        }
                    }

                    _mapper.Map(model, user);

                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        return _mapper.Map<EditProfileResposneDto>(user);
                    }
                    else
                    {
                        throw new Exception("Не удалось обновить профиль пользователя.");
                    }
                }
                else
                {
                    throw new Exception("Пользователь не найден.");
                }
            }
            else
            {
                throw new Exception("Пользователь не определен.");
            }
        }

        public async Task<EditProfileResposneDto> GetAllInfoAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    return _mapper.Map<EditProfileResposneDto>(user);
                }
            }

            throw new ArgumentNullException("User not found");
        }
    }
}
