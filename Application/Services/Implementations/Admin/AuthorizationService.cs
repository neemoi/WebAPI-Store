using Application.DTOModels.Models.Admin.Authorization;
using Application.DTOModels.Response.Admin.Authorization;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class AuthorizationService : IAccountService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IMapper _mapper;

        public AuthorizationService(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, IMapper mapper, ILogger<AuthorizationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);

            var user = await _signInManager.UserManager.FindByNameAsync(model.UserName);

            if (result.Succeeded && user != null)
            {
                return _mapper.Map<LoginResponseDto>(user);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task<LogoutResponseDto> LogoutAsync(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);

            await _signInManager.SignOutAsync();

            return _mapper.Map<LogoutResponseDto>(user);
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterDto model)
        {
            var user = _mapper.Map<CustomUser>(model);

            var emailAlreadyExists = await _userManager.FindByEmailAsync(user.Email);

            if (emailAlreadyExists != null)
            {
                _logger.LogError("User registration failed: Email already exists");

                throw new Exception("User registration failed: Email already exists");
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded && user != null)
            {
                await _signInManager.SignInAsync(user, true);

                await _userManager.AddToRoleAsync(user, "User");

                return _mapper.Map<RegisterResponseDto>(user);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(error => error.Description));

                _logger.LogError("User registration failed: " + errors);
                
                throw new Exception("User registration failed: " + errors);
            }
        }
    }
}
