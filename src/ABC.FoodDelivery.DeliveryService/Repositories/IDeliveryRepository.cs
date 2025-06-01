using ABC.FoodDelivery.DeliveryService.Entities;

namespace ABC.FoodDelivery.DeliveryService.Repositories
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetDeliveryByOrderIdAsync(Guid orderId);
        Task<List<Delivery>> GetDeliveriesByDriverIdAsync(Guid driverId);
        Task<Delivery> AssignDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryStatusAsync(Guid orderId, string status, DateTime? deliveryTime);
        Task CancelDeliveryAsync(Guid orderId);
    }
}