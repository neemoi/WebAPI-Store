using Application.CustomException;
using Application.DTOModels.Models.Admin.Authorization;
using Application.DTOModels.Response.Admin.Authorization;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using WebAPIKurs;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginModel)
        {
            try
            {
                _logger.LogInformation("Attempt to login an user: {@LoginDto}", loginModel);

                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, lockoutOnFailure: false)
                    ?? throw new CustomRepositoryException("Account login error. Сheck the details", "NOT_FOUND_ERROR_CODE");
                    
                var user = await _signInManager.UserManager.FindByNameAsync(loginModel.UserName)
                    ?? throw new CustomRepositoryException("UserName not foumd", "NOT_FOUND_ERROR_CODE"); 

                if (result.Succeeded && user != null)
                {
                    _logger.LogInformation("User successfully login: {@CustomUser}", result);

                    return _mapper.Map<LoginResponseDto>(user);
                }
                else
                {
                    throw new CustomRepositoryException("User not foumd", "NOT_FOUND_ERROR_CODE");
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when login user: {@LoginDto}", loginModel);

                throw new CustomRepositoryException("Error occurred while login: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the CustomUser: {@LoginDto}", loginModel);

                throw new CustomRepositoryException("Error occurred during CustomUser mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }

        public async Task<LogoutResponseDto> LogoutAsync(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation("Attempt to logout an user: {@LoginDto}", httpContext);

                var user = await _userManager.GetUserAsync(httpContext.User);

                await _signInManager.SignOutAsync();

                _logger.LogInformation("User successfully logout: {@CustomUser}", user);

                return _mapper.Map<LogoutResponseDto>(user);
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when logout user: {@httpContext}", httpContext);

                throw new CustomRepositoryException("Error occurred while logout: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the CustomUser: {@httpContext}", httpContext);

                throw new CustomRepositoryException("Error occurred during CustomUser mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterDto registerModel)
        {
            try
            {
                _logger.LogInformation("Attempt to register an user: {@RegisterDto}", registerModel);

                var user = _mapper.Map<CustomUser>(registerModel);

                var emailAlreadyExists = await _userManager.FindByEmailAsync(user.Email);

                if (emailAlreadyExists != null)
                {
                    throw new CustomRepositoryException($"User Email ({user.Email}) already exists", "DATABASE_ERROR");
                }

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded && user != null)
                {
                    await _signInManager.SignInAsync(user, true);

                    await _userManager.AddToRoleAsync(user, "User");

                    _logger.LogInformation("User successfully register: {@CustomUser}", user);

                    return _mapper.Map<RegisterResponseDto>(user);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(error => error.Description));

                    throw new Exception("User registration failed: " + errors);
                }
            }
            catch (CustomRepositoryException ex)
            {
                _logger.LogError(ex, "Error when register user: {@RegisterDto}", registerModel);

                throw new CustomRepositoryException("Error occurred while register: " + ex.Message, ex.ErrorCode, ex.AdditionalInfo);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "Error when mapping the CustomUser: {@RegisterDto}", registerModel);

                throw new CustomRepositoryException("Error occurred during CustomUser mapping", "MAPPING_ERROR_CODE", ex.Message);
            }
        }
    }
}
