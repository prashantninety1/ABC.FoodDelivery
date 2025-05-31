using ABC.FoodDelivery.RestaurantService.DTOs;
using ABC.FoodDelivery.RestaurantService.Entities;
using ABC.FoodDelivery.RestaurantService.Repositories;

namespace ABC.FoodDelivery.RestaurantService.Services
{
    public class MenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuItem>> GetMenuByRestaurantIdAsync(Guid restaurantId)
        {
            return await _menuRepository.GetMenuByRestaurantIdAsync(restaurantId);
        }

        public async Task<MenuItem> AddMenuItemAsync(Guid restaurantId, MenuItemDto menuItemDto)
        {
            var menuItem = new MenuItem
            {
                MenuItemId = Guid.NewGuid(),
                RestaurantId = restaurantId,
                Name = menuItemDto.Name,
                Description = menuItemDto.Description,
                Price = menuItemDto.Price,
                IsAvailable = menuItemDto.IsAvailable
            };

            await _menuRepository.AddMenuItemAsync(menuItem);
            return menuItem;
        }

        public async Task<MenuItem?> UpdateMenuItemAsync(Guid restaurantId, Guid menuItemId, UpdateMenuItemDto updateDto)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null || menuItem.RestaurantId != restaurantId) return null;

            if (!string.IsNullOrEmpty(updateDto.Name)) menuItem.Name = updateDto.Name;
            if (!string.IsNullOrEmpty(updateDto.Description)) menuItem.Description = updateDto.Description;
            if (updateDto.Price.HasValue) menuItem.Price = updateDto.Price.Value;
            if (updateDto.IsAvailable.HasValue) menuItem.IsAvailable = updateDto.IsAvailable.Value;

            await _menuRepository.UpdateMenuItemAsync(menuItem);
            return menuItem;
        }

        public async Task<bool> DeleteMenuItemAsync(Guid restaurantId, Guid menuItemId)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null || menuItem.RestaurantId != restaurantId) return false;

            return await _menuRepository.DeleteMenuItemAsync(menuItemId);
        }
    }
}