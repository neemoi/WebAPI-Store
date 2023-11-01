using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;
using Application.Services.Interfaces.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPIKurs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistance.Repository.Admin
{
    public class PaginationRepository : IPaginationRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public PaginationRepository(WebsellContext websellContext, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _websellContext = websellContext;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUserWithPaginationAsync(UserQueryParametersDto parametersModel)
        {
            try
            {
                var usersQuery = _websellContext.Users
                    .Join(_websellContext.UserRoles,
                        user => user.Id,
                        userRole => userRole.UserId,
                        (user, userRole) => new { User = user, userRole.RoleId })
                    .Join(_websellContext.Roles,
                        userRole => userRole.RoleId,
                        role => role.Id,
                        (userRole, role) => new { userRole.User, Role = role.Name })
                    .AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                    usersQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? usersQuery.OrderByDescending(u => EF.Property<object>(u.User, parametersModel.SortField))
                        : usersQuery.OrderBy(u => EF.Property<object>(u.User, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchUserId))
                {
                    usersQuery = usersQuery.Where(u => u.User.Id.Contains(parametersModel.SearchUserId));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchUserName))
                {
                    usersQuery = usersQuery.Where(u => u.User.UserName.Contains(parametersModel.SearchUserName));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchEmail))
                {
                    usersQuery = usersQuery.Where(u => u.User.Email.Contains(parametersModel.SearchEmail));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchPhoneNumber))
                {
                    usersQuery = usersQuery.Where(u => u.User.PhoneNumber.Contains(parametersModel.SearchPhoneNumber));
                }

                var users = await usersQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var userResponseDto = users.Select(user =>
                {
                    var userResponseDto = _mapper.Map<UserResponseDto>(user.User);
                    userResponseDto.Role = user.Role;

                    return userResponseDto;

                }).ToList();

                return userResponseDto;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error fetching users with pagination")
                {
                    throw new Exception("Error fetching users", ex);
                }
                else
                {
                    throw new Exception("Internal Server Error", ex);
                }
            }
        }


        public async Task<IEnumerable<IdentityRole>> GetRoleWithPaginationAsync(RoleQueryParametersDto parametersModel)
        {
            var roles = _roleManager.Roles.AsQueryable();

            parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

            roles = parametersModel.SortOrder.ToUpper() == "desc"
                    ? roles.OrderByDescending(u => EF.Property<object>(u, parametersModel.SortField))
                    : roles.OrderBy(u => EF.Property<object>(u, parametersModel.SortField));

            if (!string.IsNullOrEmpty(parametersModel.IdRole))
            {
                roles = roles.Where(r => r.Id.Contains(parametersModel.IdRole));
            }

            if (!string.IsNullOrEmpty(parametersModel.NameRole))
            {
                roles = roles.Where(r => r.Name.Contains(parametersModel.NameRole));
            }

            var rolesPage = await roles
                .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                .Take(parametersModel.PageSize)
                .ToListAsync();

            return rolesPage;
        }
    }
}
