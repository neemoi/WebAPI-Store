using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;
using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Response.Admin;
using Application.DTOModels.Response.User;
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

                if (!string.IsNullOrEmpty(parametersModel.Id))
                {
                    usersQuery = usersQuery.Where(u => u.User.Id.Contains(parametersModel.Id));
                }
                if (!string.IsNullOrEmpty(parametersModel.UserName))
                {
                    usersQuery = usersQuery.Where(u => u.User.UserName.Contains(parametersModel.UserName));
                }
                if (!string.IsNullOrEmpty(parametersModel.Email))
                {
                    usersQuery = usersQuery.Where(u => u.User.Email.Contains(parametersModel.Email));
                }
                if (!string.IsNullOrEmpty(parametersModel.PhoneNumber))
                {
                    usersQuery = usersQuery.Where(u => u.User.PhoneNumber.Contains(parametersModel.PhoneNumber));
                }
                if (!string.IsNullOrEmpty(parametersModel.State))
                {
                    usersQuery = usersQuery.Where(u => u.User.State.Contains(parametersModel.State));
                }
                if (!string.IsNullOrEmpty(parametersModel.City))
                {
                    usersQuery = usersQuery.Where(u => u.User.City.Contains(parametersModel.City));
                }
                if (!string.IsNullOrEmpty(parametersModel.Address))
                {
                    usersQuery = usersQuery.Where(u => u.User.Address.Contains(parametersModel.Address));
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
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<IdentityRole>> GetRoleWithPaginationAsync(RoleQueryParametersDto parametersModel)
        {
            try
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

                if (!string.IsNullOrEmpty(parametersModel.Id))
                {
                    roles = roles.Where(p => p.Id.ToString().Contains(parametersModel.Id));
                }
                if (!string.IsNullOrEmpty(parametersModel.Name))
                {
                    roles = roles.Where(r => r.Name.Contains(parametersModel.Name));
                }

                var rolesPage = await roles
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                return rolesPage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsWithPaginationAsync(ProductQueryParametersDto parametersModel)
        {
            try
            {
                var productsQuery = _websellContext.Products
                    .Include(p => p.Category)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                    productsQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? productsQuery.OrderByDescending(p => EF.Property<object>(p, parametersModel.SortField))
                        : productsQuery.OrderBy(p => EF.Property<object>(p, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.Id))
                {
                    productsQuery = productsQuery.Where(p => p.Id.ToString().Contains(parametersModel.Id));
                }
                if (!string.IsNullOrEmpty(parametersModel.Name))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(parametersModel.Name));
                }
                if (!string.IsNullOrEmpty(parametersModel.Description))
                {
                    productsQuery = productsQuery.Where(p => p.Description.Contains(parametersModel.Description));
                }
                if (!string.IsNullOrEmpty(parametersModel.Price))
                {
                    productsQuery = productsQuery.Where(p => p.Price.ToString().Contains(parametersModel.Price));
                }
                if (!string.IsNullOrEmpty(parametersModel.Color))
                {
                    productsQuery = productsQuery.Where(p => p.Color == parametersModel.Color);
                }
                if (!string.IsNullOrEmpty(parametersModel.Memory))
                {
                    productsQuery = productsQuery.Where(p => p.Memory == parametersModel.Memory);
                }
                if (!string.IsNullOrEmpty(parametersModel.CategoryName))
                {
                    productsQuery = productsQuery.Where(p => p.Category.Name == parametersModel.CategoryName);
                }

                var products = await productsQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var productResponseDtos = products.Select(_mapper.Map<ProductResponseDto>).ToList();

                return productResponseDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetPaymentsWithPaginationAsync(PaymentQueryParametersDto parametersModel)
        {
            try
            {
                var paymentsQuery = _websellContext.Payments.AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                    paymentsQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? paymentsQuery.OrderByDescending(p => EF.Property<object>(p, parametersModel.SortField))
                        : paymentsQuery.OrderBy(p => EF.Property<object>(p, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.Id))
                {
                    paymentsQuery = paymentsQuery.Where(p => p.Id.ToString().Contains(parametersModel.Id));
                }
                if (!string.IsNullOrEmpty(parametersModel.Amount))
                {
                    paymentsQuery = paymentsQuery.Where(p => p.Amount.ToString().Contains(parametersModel.Amount));
                }
                if (!string.IsNullOrEmpty(parametersModel.Type))
                {
                    paymentsQuery = paymentsQuery.Where(p => p.Type == parametersModel.Type);
                }

                var payments = await paymentsQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var paymentsResponseDtos = payments.Select(_mapper.Map<PaymentResponseDto>).ToList();

                return paymentsResponseDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetCategoryWithPaginationAsync(CategoryQueryParametersDto parametersModel)
        {
            try
            {
                var categoryQuery = _websellContext.Categorys.AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                    categoryQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? categoryQuery.OrderByDescending(p => EF.Property<object>(p, parametersModel.SortField))
                        : categoryQuery.OrderBy(p => EF.Property<object>(p, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.Id))
                {
                    categoryQuery = categoryQuery.Where(p => p.Id.ToString().Contains(parametersModel.Id));
                }
                if (!string.IsNullOrEmpty(parametersModel.Description))
                {
                    categoryQuery = categoryQuery.Where(p => p.Description == parametersModel.Description);
                }
                if (!string.IsNullOrEmpty(parametersModel.Name))
                {
                    categoryQuery = categoryQuery.Where(p => p.Name == parametersModel.Name);
                }

                var categorys = await categoryQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var categorysResponseDtos = categorys.Select(_mapper.Map<CategoryResponseDto>).ToList();

                return categorysResponseDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<DeliveryResponseDto>> GetDeliveryWithPaginationAsync(DeliveryQueryParametersDto parametersModel)
        {
            try
            {
                var deliveryQuery = _websellContext.Deliveries.AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                    deliveryQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? deliveryQuery.OrderByDescending(p => EF.Property<object>(p, parametersModel.SortField))
                        : deliveryQuery.OrderBy(p => EF.Property<object>(p, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.Id))
                {
                    deliveryQuery = deliveryQuery.Where(p => p.Id.ToString().Contains(parametersModel.Id));
                }
                if (!string.IsNullOrEmpty(parametersModel.Price))
                {
                    deliveryQuery = deliveryQuery.Where(p => p.Price.ToString().Contains(parametersModel.Price));
                }
                if (!string.IsNullOrEmpty(parametersModel.Type))
                {
                    deliveryQuery = deliveryQuery.Where(p => p.Type == parametersModel.Type);
                }

                var deliverys = await deliveryQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var deliveryResponseDtos = deliverys.Select(_mapper.Map<DeliveryResponseDto>).ToList();

                return deliveryResponseDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IEnumerable<UserProductResponseDto>> UserGetProductWithPaginationAsync(UserProductQueryParametersDto parametersModel)
        {
            try
            {
                var productsQuery = _websellContext.Products
                    .Include(p => p.Category)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(parametersModel.SortField))
                {
                    parametersModel.SortField = char.ToUpper(parametersModel.SortField[0]) + parametersModel.SortField.Substring(1); //changing the case of the first letter to the uppercase

                    productsQuery = parametersModel.SortOrder.ToUpper() == "DESC"
                        ? productsQuery.OrderByDescending(p => EF.Property<object>(p, parametersModel.SortField))
                        : productsQuery.OrderBy(p => EF.Property<object>(p, parametersModel.SortField));
                }

                if (!string.IsNullOrEmpty(parametersModel.Name))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(parametersModel.Name));
                }
                if (!string.IsNullOrEmpty(parametersModel.Price))
                {
                    productsQuery = productsQuery.Where(p => p.Price.ToString().Contains(parametersModel.Price));
                }
                if (!string.IsNullOrEmpty(parametersModel.Color))
                {
                    productsQuery = productsQuery.Where(p => p.Color == parametersModel.Color);
                }
                if (!string.IsNullOrEmpty(parametersModel.Memory))
                {
                    productsQuery = productsQuery.Where(p => p.Memory == parametersModel.Memory);
                }
                if (!string.IsNullOrEmpty(parametersModel.CategoryName))
                {
                    productsQuery = productsQuery.Where(p => p.Category.Name == parametersModel.CategoryName);
                }

                var products = await productsQuery
                    .Skip((parametersModel.Page - 1) * parametersModel.PageSize)
                    .Take(parametersModel.PageSize)
                    .ToListAsync();

                var productResponseDtos = products.Select(_mapper.Map<UserProductResponseDto>).ToList();

                return productResponseDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }
    }
}
