using System.Text.Json;
using ABC.FoodDelivery.IdentityService.DTOs;
using ABC.FoodDelivery.IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.IdentityService.Functions
{
    public class Signup(UserService userService)
    {
        private readonly UserService _userService = userService;

        [Function("Signup")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/signup")]  HttpRequest req,
            ILogger log)
        {
            //log.LogInformation("Processing user registration...");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var userDto = JsonSerializer.Deserialize<UserDto>(requestBody);

        if (userDto == null)
        {
            return new BadRequestObjectResult("Invalid request payload.");
        }


            try
            {
                var result = await _userService.RegisterUserAsync(userDto);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                //log.LogError($"Error registering user: {ex.Message}");
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }
    }
}