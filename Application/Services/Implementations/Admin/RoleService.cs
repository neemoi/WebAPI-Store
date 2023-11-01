using Application.DtoModels.Response.Admin;
using Application.DTOModels.Models.Admin.Roles;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RoleResponseDto> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole(roleName);

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return _mapper.Map<RoleResponseDto>(role);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task<RoleResponseDto> DeleteRoleAsync(Guid roleId)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(roleId.ToString());

            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded && role != null)
            {
                return _mapper.Map<RoleResponseDto>(role);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task<List<RoleResponseDto>> GetAllRolesAsync()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            var result = roles.Select(_mapper.Map<RoleResponseDto>).ToList();

            return result;
        }

        public async Task<RoleResponseDto> EditRoleByIdAsync(EditRoleByIdDto editModel)
        {
            var role = await _roleManager.FindByIdAsync(editModel.Id);

            if (role != null)
            {
                role.Name = editModel.Name;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    var updatedRole = await _roleManager.FindByIdAsync(editModel.Id);

                    var roleResponseDto = _mapper.Map<RoleResponseDto>(updatedRole);

                    return roleResponseDto;
                }
                else
                {
                    throw new Exception("Error updating role: " + string.Join(", ", result.Errors));
                }
            }
            else
            {
                throw new Exception("Role not found");
            }
        }


        public async Task<UserResponseDto> EditUserRoleAsync(EditUserRoleDto modelUser)
        {
            var user = await _userManager.FindByIdAsync(modelUser.UserId);

            var role = await _roleManager.FindByIdAsync(modelUser.RoleId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (role == null)
            {
                throw new Exception("Role not found");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                throw new Exception("Error removing user from roles: " + string.Join(", ", removeResult.Errors));
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (!addToRoleResult.Succeeded)
            {
                throw new Exception("Error adding user to role: " + string.Join(", ", addToRoleResult.Errors));
            }

            var userWithRoleDto = _mapper.Map<UserResponseDto>(user);
                userWithRoleDto.Role = role.Name;

            return userWithRoleDto;
        }
    }
}
