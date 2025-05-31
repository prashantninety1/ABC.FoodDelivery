using ABC.FoodDelivery.RestaurantService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.RestaurantService.Functions
{
    public class DeleteMenuItemFunction
    {
        private readonly MenuService _menuService;

        public DeleteMenuItemFunction(MenuService menuService)
        {
            _menuService = menuService;
        }

        [Function("DeleteMenuItem")]
        public async Task<IActionResult> Run(
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