using Application.CustomException;
using Application.DtoModels.Models.Admin;
using Application.DtoModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<IdentityRole> _logger;

        public UserService(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<IdentityRole> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserResponseDto> DeleteUserAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation("Attempt to delete an user: {@CustomUser}", userId);

                var adminId = "e532e613-6ebb-4bff-abee-4eda9e69f13d";

                var user = await _userManager.FindByIdAsync(userId.ToString()) 
                    ?? throw new CustomRepositoryException($"User ID ({userId}) not found", "INVALID_INPUT_DATA"); 

                if (userId.ToString() == adminId)
                {
                    throw new CustomRepositoryException("You are trying to remove role the main administrator", "INVALID_INPUT_DATA");
                }

                var roleNames = await _userManager.GetRolesAsync(user);

                foreach (var roleName in roleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);

                    if (role == null)
                    {
                        throw new CustomRepositoryException($"Role user not found", "INVALID_INPUT_DATA");
                    }

                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    string userRole = roleNames.FirstOrDefault();

                    var userResponseDto = _mapper.Map<UserResponseDto>(user);
                    userResponseDto.Role = userRole;

                    _logger.LogInformation("User successfully delete: {@CustomUser}", result);

                    return userResponseDto;
                }
                else
                {
                    throw new CustomRepositoryException($"Role user not found", "INVALID_INPUT_DATA");
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when deleting an user: {@CustomUser}", userId);

                throw new CustomRepositoryException("Error occurred while delete an user: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the user: {@CustomUser}", userId);

                throw new CustomRepositoryException("Error occurred during user mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }

        public async Task<UserResponseDto> EditUserAsync(UserDto userModel)
        {
            try
            {
                _logger.LogInformation("Attempt to edit an user: {@UserDto}", userModel);

                var user = await _userManager.FindByIdAsync(userModel.Id.ToString())
                    ?? throw new CustomRepositoryException($"User ID ({userModel.Id}) not found", "INVALID_INPUT_DATA");

                _mapper.Map(userModel, user);

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded && user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    string userRole = roles.FirstOrDefault();

                    var userResponseDto = _mapper.Map<UserResponseDto>(user);

                    userResponseDto.Role = userRole;

                    _logger.LogInformation("User successfully edit: {@CustomUser}", result);

                    return userResponseDto;
                }
                else
                {
                    throw new CustomRepositoryException($"User Editing error", "DATABASE_ERROR");
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when edit an user: {@UserDto}", userModel);

                throw new CustomRepositoryException("Error occurred while edit an user: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the user: {@UserDto}", userModel);

                throw new CustomRepositoryException("Error occurred during user mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }
    }
}