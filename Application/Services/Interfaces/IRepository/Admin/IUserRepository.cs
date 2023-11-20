using Application.DtoModels.Models.Admin;
using Application.DtoModels.Response.Admin;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository.Admin
{
    public interface IUserRepository
    {
        Task<CustomUser> EditUserAsync(UserDto model);

        Task<CustomUser> DeleteUserAsync(string userId);
    }
}
