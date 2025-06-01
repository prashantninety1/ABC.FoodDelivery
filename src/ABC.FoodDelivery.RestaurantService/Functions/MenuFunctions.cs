using System.Text.Json;
using ABC.FoodDelivery.RestaurantService.DTOs;
using ABC.FoodDelivery.RestaurantService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.RestaurantService.Functions
{
    public class MenuFunctions(MenuService menuService)
    {
        private readonly MenuService _menuService = menuService;

        [Function("AddMenuItem")]
        public async Task<IActionResult> AddMenuItem(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "restaurants/{restaurantId}/menu")] HttpRequest req,
            Guid restaurantId,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var menuItemDto = JsonSerializer.Deserialize<MenuItemDto>(requestBody);

            if (menuItemDto == null)
                return new BadRequestObjectResult("Invalid menu item details.");

            var menuItem = await _menuService.AddMenuItemAsync(restaurantId, menuItemDto);
            return new OkObjectResult(new { menuItem.MenuItemId, message = "Menu item added successfully." });
        }

        [Function("UpdateMenuItem")]
        public async Task<IActionResult> UpdateMenuItem(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "restaurants/{restaurantId}/menu/{menuItemId}")] HttpRequest req,
            Guid restaurantId,
            Guid menuItemId,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateDto = JsonSerializer.Deserialize<UpdateMenuItemDto>(requestBody);

            var updatedItem = await _menuService.UpdateMenuItemAsync(restaurantId, menuItemId, updateDto);
            if (updatedItem == null)
                return new NotFoundObjectResult("Menu item not found.");

            return new OkObjectResult("Menu item updated successfully.");
        }

        [Function("DeleteMenuItem")]
        public async Task<IActionResult> DeleteMenuItem(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "restaurants/{restaurantId}/menu/{menuItemId}")] HttpRequest req,
            Guid restaurantId,
            Guid menuItemId,
            ILogger log)
        {
            var success = await _menuService.DeleteMenuItemAsync(restaurantId, menuItemId);
            if (!success)
                return new NotFoundObjectResult("Menu item not found.");

            return new OkObjectResult("Menu item deleted successfully.");
        }
    }
}