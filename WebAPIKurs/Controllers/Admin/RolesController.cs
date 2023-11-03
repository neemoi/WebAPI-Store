using Application.DtoModels.Models.Pagination;
using Application.DTOModels.Models.Admin.Roles;
using Application.Services.Interfaces.IServices;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers.Admin
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService _adminRolesService;
        private readonly IPaginationService _paginationService;

        public RolesController(IRoleService adminRolesService, IPaginationService paginationService)
        {
            _adminRolesService = adminRolesService;
            _paginationService = paginationService;
        }


        [HttpGet("Admin/Role")]
        public async Task<IActionResult> RolePagination([FromQuery] RoleQueryParametersDto parametersModel)
        {
            return Ok(await _paginationService.GetRoleWithPaginationAsync(parametersModel)) ;
        }

        [HttpPost("Admin/Role")]
        public async Task<IActionResult> CreateRoleAsync(string name)
        {
            return Ok(await _adminRolesService.CreateRoleAsync(name));
        }

        [HttpPut("Admin/Role")]
        public async Task<IActionResult> EditRoleByIdAsync(EditRoleByIdDto editModel)
        {
            return Ok(await _adminRolesService.EditRoleByIdAsync(editModel));
        }

        [HttpDelete("Admin/Role/{id}")]
        public async Task<IActionResult> DeleteRoleAsync(Guid id)
        {
            return Ok(await _adminRolesService.DeleteRoleAsync(id));
        }
    }
}
