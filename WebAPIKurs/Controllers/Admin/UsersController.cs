using Application.DtoModels.Models.Admin;
using Application.DtoModels.Models.Pagination;
using Application.DTOModels.Models.Admin.Pagination;
using Application.DTOModels.Models.Admin.Roles;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Application.DTOModels.Models.Admin;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPaginationService _paginationService;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUserService userService, IPaginationService paginationService, IRoleService roleService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _paginationService = paginationService;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Admin/User")]
        public async Task<IActionResult> PaginationUserAsync([FromQuery] UserQueryParametersDto parametersModel)
        {
            return Ok(await _paginationService.GetUserWithPaginationAsync(parametersModel));
        }

        [HttpGet("Admin/User/Order")]
        public async Task<IActionResult> UserGetOrderWithPaginationAsync([FromQuery] GeUsertOrderQueryParametersDto parametersModel)
        {
            return Ok(await _paginationService.GeUsertOrderWithPaginationAsync(parametersModel.UserId, parametersModel));
        }

        [HttpPut("Admin/User/Order")]
        public async Task<IActionResult> EditUserOrderAsync(UserOrderEditDto orderModel)
        {
            return Ok(await _unitOfWork.UserOrderRepository.EditUserOrderAsync(orderModel));
        }

        [HttpPut("Admin/User/Role")]
        public async Task<IActionResult> EditUserRoleAsync(EditUserRoleDto editUser)
        {
            return Ok(await _roleService.EditUserRoleAsync(editUser));
        }

        [HttpPut("Admin/User/")]
        public async Task<IActionResult> EditUserAsync(UserDto userModel)
        {
            return Ok(await _userService.EditUserAsync(userModel));
        }

        [HttpDelete("Admin/User/")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            return Ok(await _userService.DeleteUserAsync(id));
        }
    }
}