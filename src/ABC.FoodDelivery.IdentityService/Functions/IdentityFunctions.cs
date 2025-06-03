using ABC.FoodDelivery.IdentityService.DTOs;
using ABC.FoodDelivery.IdentityService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ABC.FoodDelivery.IdentityService.Functions
{
    public class IdentityFunctions(UserService userService, ITokenService tokenService)
    {
        private readonly UserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        [Function("Signin")]
        public async Task<IActionResult> Signin(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/signin")] SigninDto req,
            ILogger log)
        {
            // log.LogInformation("Processing user login...");

            if (req == null)
            {
                return new BadRequestObjectResult("Invalid request payload.");
            }
            try
            {
                var user = await _userService.AuthenticateUserAsync(req.Email, req.Password);
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

        [Function("Signup")]
        public async Task<IActionResult> Signup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/signup")] SignupDto req,
            ILogger log)
        {
            //log.LogInformation("Processing user registration...");
            if (req == null)
            {
                return new BadRequestObjectResult("Invalid request payload.");
            }

            try
            {
                var result = await _userService.RegisterUserAsync(req);
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