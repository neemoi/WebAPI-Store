using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService _adminRolesService;

        public RolesController(IRoleService adminRolesService)
        {
            _adminRolesService = adminRolesService;
        }

        [HttpGet("api/Role")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            return Ok(await _adminRolesService.GetAllRolesAsync());
        }

        [HttpPost("api/Role")]
        public async Task<IActionResult> CreateRoleAsync(string name)
        {
            return Ok(await _adminRolesService.CreateRoleAsync(name));
        }

        //[HttpPut("api/Role/{id}")]
        //public async Task<IActionResult> AssignUserRoleAsync(Guid id, string role)
        //{
        //    return Ok(await _adminRolesService.AssignUserRoleAsync(id, role));
        //}

        [HttpDelete("api/Role/{id}")]
        public async Task<IActionResult> DeleteRoleAsync(Guid id)
        {
            return Ok(await _adminRolesService.DeleteRoleAsync(id));
        }
    }
}
