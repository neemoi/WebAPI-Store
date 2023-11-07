using Application.DTOModels.Models.Admin.Delivery;
using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs;

namespace Persistance.Repository.Admin
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly WebsellContext _websellContext;
        private readonly IMapper _mapper;

        public DeliveryRepository(WebsellContext websellContext, IMapper mapper)
        {
            _websellContext = websellContext;
            _mapper = mapper;
        }

        public async Task<Delivery> CreateDeliveryAsync(Delivery deliveryModel)
        {
            var result = await _websellContext.Deliveries.AddAsync(deliveryModel);

            if (result != null)
            {
                await _websellContext.SaveChangesAsync();

                return deliveryModel;
            }
            else
            {
                throw new Exception("Error create delivery");
            }
        }

        public async Task<Delivery> DeleteDeliveryAsync(int deliveryId)
        {
            var result = await _websellContext.Deliveries.FirstOrDefaultAsync(p => p.Id == deliveryId);

            if (result != null)
            {
                _websellContext.Deliveries.Remove(result);

                await _websellContext.SaveChangesAsync();

                return result;
            }
            else
            {
                throw new Exception("Error delete delivery");
            }
        }

        public async Task<Delivery> EditDeliveryAsync(int deliveryId, DeliveryEditDto deliveryModel)
        {
            var delivery = await _websellContext.Deliveries.FirstOrDefaultAsync(p => p.Id == deliveryId);

            if (delivery != null)
            {
                _mapper.Map(deliveryModel, delivery);

                await _websellContext.SaveChangesAsync();

                return delivery;
            }
            else
            {
                throw new Exception("Error update delivery");
            }
        }
    }
}
