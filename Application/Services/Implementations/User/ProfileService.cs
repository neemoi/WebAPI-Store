using Application.CustomException;
using Application.DTOModels.Models.User;
using Application.DTOModels.Response.User;
using Application.Services.Interfaces.IServices.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WebAPIKurs;

namespace Application.Services.Implementations.User
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomUser> _logger;

        public ProfileService(IMapper mapper, UserManager<CustomUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<CustomUser> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<EditProfileResposneDto> EditProfileAsync(EditProfileDto model)
        {
            try
            {
                _logger.LogInformation("Attempt to edit profile an user: {@EditProfileDto}", model);

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
                                throw new CustomRepositoryException("Error changing password", "DATABASE_ERROR");
                            }
                        }

                        _mapper.Map(model, user);

                        var updateResult = await _userManager.UpdateAsync(user);

                        if (updateResult.Succeeded)
                        {
                            _logger.LogInformation("User prodile successfully edit: {@CustomUser}", updateResult);

                            return _mapper.Map<EditProfileResposneDto>(user);
                        }
                        else
                        {
                            throw new CustomRepositoryException("Failed to update user profile", "DATABASE_ERROR");
                        }
                    }
                    else
                    {
                        throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");
                    }
                }
                else
                {
                    throw new CustomRepositoryException($"User ID {userId} not found", "NOT_FOUND_ERROR_CODE");
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error in ProfileService.EditProfileAsync: ", ex.Message);

                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the CustomUser: {@CustomUser}");

                throw new CustomRepositoryException("Error occurred during CustomUser mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }

        public async Task<EditProfileResposneDto> GetAllInfoAsync()
        {
            try
            {
                _logger.LogInformation("Attempt to get info an user: {@CustomUser}");

                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        _logger.LogInformation("Get info successfully: {@CustomUser}");

                        return _mapper.Map<EditProfileResposneDto>(user);
                    }
                    else
                    {
                        throw new CustomRepositoryException($"User ID {userId} not found", "NOT_FOUND_ERROR_CODE");
                    }
                }
                else
                {
                    throw new CustomRepositoryException("User not found", "NOT_FOUND_ERROR_CODE");
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error in ProfileService.GetAllInfoAsync: ", ex.Message);

                throw new CustomRepositoryException(ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the CustomUser: {@CustomUser}");

                throw new CustomRepositoryException("Error occurred during CustomUser mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }
    }
}
