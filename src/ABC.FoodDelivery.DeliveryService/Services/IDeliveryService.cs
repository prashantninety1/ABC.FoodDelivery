using ABC.FoodDelivery.DeliveryService.Entities;

namespace ABC.FoodDelivery.DeliveryService.Services
{
    public interface IDeliveryService
    {
        Task<Delivery> AssignDeliveryAsync(Guid orderId, Guid driverId);
        Task<Delivery> GetDeliveryByOrderIdAsync(Guid orderId);
        Task<List<Delivery>> GetDeliveriesByDriverIdAsync(Guid driverId);
        Task UpdateDeliveryStatusAsync(Guid orderId, string status);
        Task CancelDeliveryAsync(Guid orderId);
    }
}