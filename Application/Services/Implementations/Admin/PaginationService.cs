using Application.DtoModels.Models.Pagination;
using Application.DtoModels.Response.Admin;
using Application.Services.Interfaces.IServices;
using Application.Services.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations
{
    public class PaginationService : IPaginationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUserWithPaginationAsync(UserQueryParametersDto parametersModel)
        {
            try
            {
                var result = await _unitOfWork.PaginationRepository.GetUserWithPaginationAsync(parametersModel);

                var userResponseDto = result.Select(user =>
                {
                    var userResponseDto = _mapper.Map<UserResponseDto>(user);

                    return userResponseDto;

                }).ToList();

                return userResponseDto;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error fetching users with pagination")
                {
                    throw new Exception("Error fetching users", ex);
                }
                else
                {
                    throw new Exception("Internal Server Error", ex);
                }
            }
        }

        public async Task<IEnumerable<RoleResponseDto>> GetRoleWithPaginationAsync(RoleQueryParametersDto parametersModel)
        {
            try
            {
                var result = await _unitOfWork.PaginationRepository.GetRoleWithPaginationAsync(parametersModel);

                var roleResponseDto = result.Select(role =>
                {
                    var roleResponseDto = _mapper.Map<RoleResponseDto>(role);

                    return roleResponseDto;

                }).ToList();

                return roleResponseDto;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error fetching role with pagination")
                {
                    throw new Exception("Error fetching roles", ex);
                }
                else
                {
                    throw new Exception("Internal Server Error", ex);
                }
            }
        }
    }
}
