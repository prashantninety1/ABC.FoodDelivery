using ABC.FoodDelivery.RestaurantService.Entities;

namespace ABC.FoodDelivery.RestaurantService.Repositories
{
    public interface IMenuRepository
    {
        Task<List<MenuItem>> GetMenuByRestaurantIdAsync(Guid restaurantId);
        Task<MenuItem?> GetMenuItemByIdAsync(Guid menuItemId);
        Task AddMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(Guid menuItemId);
    }
}