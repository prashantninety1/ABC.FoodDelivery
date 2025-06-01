using ABC.FoodDelivery.OrderService.DTOs;
using ABC.FoodDelivery.OrderService.Entities;

namespace ABC.FoodDelivery.OrderService.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<List<Order>> GetCustomerOrdersAsync(Guid customerId);
        Task<List<Order>> GetRestaurantOrdersAsync(Guid restaurantId);
        Task<Order> CreateOrderAsync(Guid customerId, Guid restaurantId, List<OrderItemDto> items);
        Task UpdateOrderStatusAsync(Guid orderId, string status);
        Task CancelOrderAsync(Guid orderId);
    }
}