using Application.DtoModels.Models.Admin;
using Application.DtoModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> DeleteUserAsync(Guid userId)
        {
            var adminId = "e532e613-6ebb-4bff-abee-4eda9e69f13d";

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (userId.ToString() == adminId)
            {
                if (user == null)
                {
                    throw new Exception($"User not found");
                }

                throw new Exception($"Error. You are trying to remove role the main administrator");
            }

            var roleNames = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role == null)
                {
                    throw new Exception($"Deletion Error. Role user not found");
                }

                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                string userRole = roleNames.FirstOrDefault();

                var userResponseDto = _mapper.Map<UserResponseDto>(user);
                userResponseDto.Role = userRole;

                return userResponseDto;
            }
            else
            {
                throw new Exception($"Deletion Error. Role user not found");
            }
        }

        public async Task<UserResponseDto> EditUserAsync(Guid userId, UserDto model)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            _mapper.Map(model, user);

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded && user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                string userRole = roles.FirstOrDefault(); 
                                                          
                var userResponseDto = _mapper.Map<UserResponseDto>(user);

                userResponseDto.Role = userRole;

                return userResponseDto;
            }
            else
            {
                throw new Exception($"Internal Server Error");
            }
        }
    }
}