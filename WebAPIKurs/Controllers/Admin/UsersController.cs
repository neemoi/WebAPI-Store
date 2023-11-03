using Application.DtoModels.Models.Admin;
using Application.DtoModels.Models.Pagination;
using Application.DTOModels.Models.Admin.Roles;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPaginationService _paginationService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService adminService, IPaginationService paginationService, IRoleService roleService)
        {
            _userService = adminService;
            _paginationService = paginationService;
            _roleService = roleService;
        }


        [HttpGet("Admin/User/")]
        public async Task<IActionResult> PaginationUserAsync([FromQuery] UserQueryParametersDto parametersModel)
        {
            return Ok(await _paginationService.GetUserWithPaginationAsync(parametersModel));
        }

        [HttpPut("Admin/User/Role")]
        public async Task<IActionResult> EditUserRoleAsync(EditUserRoleDto editUser)
        {
            return Ok(await _roleService.EditUserRoleAsync(editUser));
        }

        [HttpPut("Admin/User/{id}")]
        public async Task<IActionResult> EditUserAsync(Guid id, UserDto userModel)
        {
            return Ok(await _userService.EditUserAsync(id, userModel));
        }

        [HttpDelete("Admin/User/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            return Ok(await _userService.DeleteUserAsync(id));
        }
    }
}