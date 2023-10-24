using Application.DTOModels.Models.Admin;
using Application.DTOModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ILogger<AccountService> logger)
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
            var user = _mapper.Map<User>(model);

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
