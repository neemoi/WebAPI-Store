using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;
using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Response.Admin;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPIKurs;

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
                if (!string.IsNullOrEmpty(parametersModel.SearchState))
                {
                    usersQuery = usersQuery.Where(u => u.User.State.Contains(parametersModel.SearchState));
                }
                if (!string.IsNullOrEmpty(parametersModel.SearchCity))
                {
                    usersQuery = usersQuery.Where(u => u.User.City.Contains(parametersModel.SearchCity));
                }
                if (!string.IsNullOrEmpty(parametersModel.SearchAddress))
                {
                    usersQuery = usersQuery.Where(u => u.User.Address.Contains(parametersModel.SearchAddress));
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

            if (!string.IsNullOrEmpty(parametersModel.SortField))
            {
                parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                if (parametersModel.SortOrder.ToUpper() == "DESC")
                {
                    roles = roles.OrderByDescending(r => r.Name);
                }
                else
                {
                    roles = roles.OrderBy(r => r.Name);
                }
            }

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

        public async Task<IEnumerable<ProductResponseDto>> GetProductsWithPaginationAsync(ProductQueryParametersDto parametersModel)
        {
            try
            {
                var productsQuery = _websellContext.Products.AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1);

                    productsQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? productsQuery.OrderByDescending(p => EF.Property<object>(p, parametersModel.SortField))
                        : productsQuery.OrderBy(p => EF.Property<object>(p, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchProductId))
                {
                    productsQuery = productsQuery.Where(p => p.Id.ToString().Contains(parametersModel.SearchProductId));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchProductName))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(parametersModel.SearchProductName));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchProductDescription))
                {
                    productsQuery = productsQuery.Where(p => p.Description.Contains(parametersModel.SearchProductDescription));
                }

                if (!string.IsNullOrEmpty(parametersModel.SearchProductPrice))
                {
                    productsQuery = productsQuery.Where(p => p.Price.ToString().Contains(parametersModel.SearchProductPrice));
                }

                var products = await productsQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var productResponseDtos = products.Select(product => _mapper.Map<ProductResponseDto>(product)).ToList();

                return productResponseDtos;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error fetching products with pagination")
                {
                    throw new Exception("Error fetching products", ex);
                }
                else
                {
                    throw new Exception("Internal Server Error", ex);
                }
            }
        }

    }
}
