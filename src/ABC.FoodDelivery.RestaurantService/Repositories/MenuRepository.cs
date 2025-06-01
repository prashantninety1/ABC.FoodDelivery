using ABC.FoodDelivery.RestaurantService.Data;
using ABC.FoodDelivery.RestaurantService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.RestaurantService.Repositories
{
    public class MenuRepository(ApplicationDbContext context) : IMenuRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<MenuItem>> GetMenuByRestaurantIdAsync(Guid restaurantId)
        {
            return await _context.MenuItems.Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(Guid menuItemId)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == menuItemId);
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMenuItemAsync(Guid menuItemId)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemId);
            if (menuItem == null) return false;

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}