using ABC.FoodDelivery.RestaurantService.Data;
using ABC.FoodDelivery.RestaurantService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.RestaurantService.Repositories
{
    public class RestaurantRepository(RestaurantDbContext context) : IRestaurantRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Restaurant?> GetRestaurantByIdAsync(Guid restaurantId)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public Task<List<Restaurant>> GetRestaurantsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}