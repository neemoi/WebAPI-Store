using Application.DTOModels.Models.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountService.LoginAsync(model));
            }
            else
            {
                throw new Exception("Error");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromQuery] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _accountService.RegisterAsync(model));
            }
            else
            {
                throw new Exception("Error");
            }
        }

        [HttpPost("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok(await _accountService.LogoutAsync(HttpContext));
        }
    }
}