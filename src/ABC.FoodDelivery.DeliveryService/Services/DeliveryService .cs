using ABC.FoodDelivery.DeliveryService.Entities;
using ABC.FoodDelivery.DeliveryService.Repositories;

namespace ABC.FoodDelivery.DeliveryService.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<Delivery> AssignDeliveryAsync(Guid orderId, Guid driverId)
        {
            var delivery = new Delivery
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                DriverId = driverId,
                Status = "Assigned",
                PickupTime = DateTime.UtcNow
            };

            return await _deliveryRepository.AssignDeliveryAsync(delivery);
        }

        public async Task<Delivery> GetDeliveryByOrderIdAsync(Guid orderId)
        {
            return await _deliveryRepository.GetDeliveryByOrderIdAsync(orderId);
        }

        public async Task<List<Delivery>> GetDeliveriesByDriverIdAsync(Guid driverId)
        {
            return await _deliveryRepository.GetDeliveriesByDriverIdAsync(driverId);
        }

        public async Task UpdateDeliveryStatusAsync(Guid orderId, string status)
        {
            await _deliveryRepository.UpdateDeliveryStatusAsync(orderId, status, status == "Delivered" ? DateTime.UtcNow : null);
        }

        public async Task CancelDeliveryAsync(Guid orderId)
        {
            await _deliveryRepository.CancelDeliveryAsync(orderId);
        }
    }
}