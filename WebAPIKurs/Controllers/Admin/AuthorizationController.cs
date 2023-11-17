using Application.CustomException;
using Application.DTOModels.Models.Admin.Authorization;
using Application.Services.Interfaces.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers.Admin
{
    public class AuthorizationController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthorizationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _accountService.LoginAsync(model));
                }
                else
                {
                    throw new CustomRepositoryException("User not found. Check the correctness of the data", "INVALID_INPUT_DATA");
                }
            }
            catch (CustomRepositoryException ex)
            {
                throw new CustomRepositoryException("An error occurred while trying to LogIn: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromQuery] RegisterDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _accountService.RegisterAsync(model));
                }
                else
                {
                    throw new CustomRepositoryException("Check the correctness of the data", "INVALID_INPUT_DATA");
                }
            }
            catch (CustomRepositoryException ex)
            {
                throw new CustomRepositoryException("An error occurred while trying to Register: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
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