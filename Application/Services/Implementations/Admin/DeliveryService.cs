using Application.DTOModels.Models.Admin.Delivery;
using Application.DTOModels.Response.Admin;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.UnitOfWork;
using AutoMapper;
using WebAPIKurs;

namespace Application.Services.Implementations.Admin
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryService(IUnitOfWork unitOfWork, IMapper  mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeliveryResponseDto> CreateDeliveryAsync(DeliveryCreateDto deliveryModel)
        {
            var delivery = _mapper.Map<Delivery>(deliveryModel);

            var result = await _unitOfWork.DeliveryRepository.CreateDeliveryAsync(delivery);

            return _mapper.Map<DeliveryResponseDto>(result);
        }

        public async Task<DeliveryResponseDto> DeleteDeliveryAsync(int deliveryId)
        {
            var result = await _unitOfWork.DeliveryRepository.DeleteDeliveryAsync(deliveryId);

            return _mapper.Map<DeliveryResponseDto>(result); 
        }

        public async Task<DeliveryResponseDto> EditDeliveryAsync(int deliveryId, DeliveryEditDto deliveryModel)
        {
            var result = await _unitOfWork.DeliveryRepository.EditDeliveryAsync(deliveryId, deliveryModel);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DeliveryResponseDto>(result);
        }
    }
}
