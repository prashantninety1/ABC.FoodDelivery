using System.Text.Json;
using ABC.FoodDelivery.RestaurantService.DTOs;
using ABC.FoodDelivery.RestaurantService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.RestaurantService.Functions
{
    public class RestaurantFunctions(Service restaurantService)
    {
        private readonly Service _restaurantService = restaurantService;

        [Function("RegisterRestaurant")]
        public async Task<IActionResult> RegisterRestaurant(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "restaurants/register")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var restaurantDto = JsonSerializer.Deserialize<RestaurantDto>(requestBody);

            if (restaurantDto == null)
                return new BadRequestObjectResult("Invalid request payload.");

            var restaurant = await _restaurantService.RegisterRestaurantAsync(restaurantDto);
            return new OkObjectResult(new { restaurant.RestaurantId, message = "Restaurant registered successfully." });
        }
    }
}