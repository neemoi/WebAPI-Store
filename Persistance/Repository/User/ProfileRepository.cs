using Application.CustomException;
using Application.DTOModels.Models.User;
using Application.Services.Interfaces.IRepository.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPIKurs;

namespace Persistance.Repository.User
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileRepository(IMapper mapper, UserManager<CustomUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomUser> EditProfileAsync(EditProfileDto model)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");

                var user = await _userManager.FindByIdAsync(userId)
                    ?? throw new CustomRepositoryException($"User ID ({userId}) not found", "NOT_FOUND_ERROR_CODE");

                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    if (!changePasswordResult.Succeeded)
                    {
                        throw new Exception($"Error changing password: " + string.Join(", ", changePasswordResult.Errors));
                    }
                }

                _mapper.Map(model, user);

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    return user;
                }
                else
                {
                    throw new Exception($"Error update data: " + string.Join(", ", updateResult.Errors));
                }
            }
            catch (CustomRepositoryException ex)
            {
                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }

        public async Task<CustomUser> GetAllInfoAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");

                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new CustomRepositoryException($"User ID {userId} not found", "NOT_FOUND_ERROR_CODE");
                }
            }
            catch (CustomRepositoryException ex)
            {
                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }
    }
}
