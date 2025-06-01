using ABC.FoodDelivery.DeliveryService.Data;
using ABC.FoodDelivery.DeliveryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.DeliveryService.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Delivery> GetDeliveryByOrderIdAsync(Guid orderId)
        {
            return await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == orderId);
        }

        public async Task<List<Delivery>> GetDeliveriesByDriverIdAsync(Guid driverId)
        {
            return await _context.Deliveries.Where(d => d.DriverId == driverId).ToListAsync();
        }

        public async Task<Delivery> AssignDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }

        public async Task UpdateDeliveryStatusAsync(Guid orderId, string status, DateTime? deliveryTime)
        {
            var delivery = await GetDeliveryByOrderIdAsync(orderId);
            if (delivery == null) return;

            delivery.Status = status;
            delivery.DeliveryTime = deliveryTime;
            await _context.SaveChangesAsync();
        }

        public async Task CancelDeliveryAsync(Guid orderId)
        {
            var delivery = await GetDeliveryByOrderIdAsync(orderId);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
    }
}