using ABC.FoodDelivery.OrderService.DTOs;
using ABC.FoodDelivery.OrderService.Entities;
using ABC.FoodDelivery.OrderService.Repositories;

namespace ABC.FoodDelivery.OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(Guid customerId)
        {
            return await _orderRepository.GetCustomerOrdersAsync(customerId);
        }

        public async Task<List<Order>> GetRestaurantOrdersAsync(Guid restaurantId)
        {
            return await _orderRepository.GetRestaurantOrdersAsync(restaurantId);
        }

        public async Task<Order> CreateOrderAsync(Guid customerId, Guid restaurantId, List<OrderItemDto> items)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                RestaurantId = restaurantId,
                TotalAmount = items.Sum(i => i.Price * i.Quantity),
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Items = items.Select(i => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = Guid.NewGuid(), // Assigned after order creation
                    MenuItemId = i.MenuItemId,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    CreatedAt = DateTime.UtcNow
                }).ToList()
            };

            return await _orderRepository.CreateOrderAsync(order);
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, string status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
        }

        public async Task CancelOrderAsync(Guid orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}