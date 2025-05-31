using System.Text.Json;
using ABC.FoodDelivery.RestaurantService.DTOs;
using ABC.FoodDelivery.RestaurantService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.RestaurantService.Functions
{
    public class AddMenuItemFunction(MenuService menuService)
    {
        private readonly MenuService _menuService = menuService;

        [Function("AddMenuItem")]
        public async Task<IActionResult> Run(
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
    }
}