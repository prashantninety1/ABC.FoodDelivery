using ABC.FoodDelivery.OrderService.Data;
using ABC.FoodDelivery.OrderService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders.Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(Guid customerId)
        {
            return await _context.Orders.Include(o => o.Items)
                .Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, string status)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null) return;

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Order>> GetRestaurantOrdersAsync(Guid restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}