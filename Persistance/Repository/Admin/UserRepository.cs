using Application.CustomException;
using Application.DtoModels.Models.Admin;
using Application.DtoModels.Response.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebAPIKurs;

namespace Persistance.Repository.Admin
{
    public class UserRepository : IUserRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<IdentityRole> _logger;

        public UserRepository(RoleManager<IdentityRole> roleManager, UserManager<CustomUser> userManager, IMapper mapper, ILogger<IdentityRole> logger)
        {
            _logger= logger;
            _roleManager= roleManager;
            _userManager= userManager;
            _mapper= mapper;
        }
        
        public async Task<CustomUser> DeleteUserAsync(string userId)
        {
            try
            {
                var adminId = "e532e613-6ebb-4bff-abee-4eda9e69f13d";

                var user = await _userManager.FindByIdAsync(userId.ToString())
                    ?? throw new CustomRepositoryException($"User ID ({userId}) not found", "NOT_FOUND_ERROR_CODE");

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
                        throw new CustomRepositoryException($"Role not found", "NOT_FOUND_ERROR_CODE");
                    }

                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    string userRole = roleNames.FirstOrDefault();

                    var userResponseDto = _mapper.Map<UserResponseDto>(user);
                    userResponseDto.Role = userRole;

                    return user;
                }
                else
                {
                    throw new Exception("Error adding user to role: " + string.Join(", ", result.Errors));
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when delet an user: {@CustomUser}", userId);

                throw new CustomRepositoryException("Error occurred while delete an user: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }

        public async Task<CustomUser> EditUserAsync(UserDto userModel)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userModel.Id.ToString())
                    ?? throw new CustomRepositoryException($"User ID ({userModel.Id}) not found", "NOT_FOUND_ERROR_CODE");

                _mapper.Map(userModel, user);

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    string userRole = roles.FirstOrDefault();

                    var userResponseDto = _mapper.Map<UserResponseDto>(user);
                    userResponseDto.Role = userRole;

                    return user;
                }
                else
                {
                    throw new Exception($"Error update data: " + string.Join(", ", updateResult.Errors));
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when edit an user: {@UserDto}", userModel);

                throw new CustomRepositoryException("Error occurred while edit an user: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }
    }
}
