using Application.DTOModels.Models.User;
using Application.Services.Interfaces.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers.User
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("User/Profile")]
        public async Task<IActionResult> GetAllInfoAsync()
        {
            return Ok(await _profileService.GetAllInfoAsync());
        }

        [HttpPut("User/Profile")]
        public async Task<IActionResult> EditProfileAsync(EditProfileDto editModel)
        {
            return Ok(await _profileService.EditProfileAsync(editModel));
        }
    }
}
