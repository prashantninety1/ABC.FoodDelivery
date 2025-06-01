using ABC.FoodDelivery.OrderService.Entities;

namespace ABC.FoodDelivery.OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<List<Order>> GetCustomerOrdersAsync(Guid customerId);
        Task<List<Order>> GetRestaurantOrdersAsync(Guid restaurantId);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderStatusAsync(Guid orderId, string status);
        Task DeleteOrderAsync(Guid orderId);
    }
}