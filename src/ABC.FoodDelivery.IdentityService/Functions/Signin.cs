using System.Text.Json;
using ABC.FoodDelivery.IdentityService.DTOs;
using ABC.FoodDelivery.IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.IdentityService.Functions
{
    public class Signin(UserService userService, ITokenService tokenService)
    {
        private readonly UserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        [Function("Signin")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/signin")] HttpRequest req,
            ILogger log)
        {
            // log.LogInformation("Processing user login...");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var signinDto = JsonSerializer.Deserialize<SigninDto>(requestBody);

            if (signinDto == null)
            {
                return new BadRequestObjectResult("Invalid request payload.");
            }

            try
            {
                var user = await _userService.AuthenticateUserAsync(signinDto.Email, signinDto.Password);
                if (user == null)
                {
                    return new UnauthorizedObjectResult(new { message = "Invalid email or password." });
                }

                // Generate JWT token
                var token = _tokenService.GenerateToken(user);

                return new OkObjectResult(new { token, message = "Login successful." });
            }
            catch (Exception ex)
            {
                // log.LogError($"Error during login: {ex.Message}");
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }
    }
}