using Application.DTOModels.Models.User;
using WebAPIKurs;

namespace Application.Services.Interfaces.IRepository.User
{
    public interface IProfileRepository
    {
        Task<CustomUser> EditProfileAsync(EditProfileDto model);

        Task<CustomUser> GetAllInfoAsync();
    }
}
