using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;
using Microsoft.AspNetCore.Identity;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository
{
    public interface IPaginationRepository
    {
        Task<IEnumerable<IdentityRole>> GetRoleWithPaginationAsync(RoleQueryParametersDto parametersModel);

        Task<IEnumerable<UserResponseDto>> GetUserWithPaginationAsync(UserQueryParametersDto parametersModel);
    }
}
