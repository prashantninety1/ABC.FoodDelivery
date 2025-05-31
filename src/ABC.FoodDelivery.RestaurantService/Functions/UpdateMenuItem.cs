using System.Text.Json;
using ABC.FoodDelivery.RestaurantService.DTOs;
using ABC.FoodDelivery.RestaurantService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.RestaurantService.Functions
{
    public class UpdateMenuItemFunction(MenuService menuService)
    {
        private readonly MenuService _menuService = menuService;

        [Function("UpdateMenuItem")]
        public async Task<IActionResult> Run(
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
    }
}