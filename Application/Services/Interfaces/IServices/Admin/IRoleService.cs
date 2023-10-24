using Application.DtoModels.Response.Admin;

namespace Application.Services.Interfaces.IServices.Admin
{
    public interface IRoleService
    {
        Task<List<RoleResponseDto>> GetAllRolesAsync();

        //Task<RoleResponseDto> AssignUserRoleAsync(Guid userId, string roleName);

        Task<RoleResponseDto> CreateRoleAsync(string roleName);

        Task<RoleResponseDto> DeleteRoleAsync(Guid roleId);
    }
}
