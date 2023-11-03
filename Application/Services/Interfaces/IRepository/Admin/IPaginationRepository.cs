using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;
using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Response.Admin;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IPaginationRepository
    {
        Task<IEnumerable<IdentityRole>> GetRoleWithPaginationAsync(RoleQueryParametersDto parametersModel);

        Task<IEnumerable<UserResponseDto>> GetUserWithPaginationAsync(UserQueryParametersDto parametersModel);

        Task<IEnumerable<ProductResponseDto>> GetProductsWithPaginationAsync(ProductQueryParametersDto parametersModel);
    }
}
