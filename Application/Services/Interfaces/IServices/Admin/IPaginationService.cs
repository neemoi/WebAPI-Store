using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;

namespace Application.Services.Interfaces.IServices
{
    public interface IPaginationService
    {
        Task<IEnumerable<RoleResponseDto>> GetRoleWithPaginationAsync(RoleQueryParametersDto parametersModel);

        Task<IEnumerable<UserResponseDto>> GetUserWithPaginationAsync(UserQueryParametersDto parametersModel);
    }
}
