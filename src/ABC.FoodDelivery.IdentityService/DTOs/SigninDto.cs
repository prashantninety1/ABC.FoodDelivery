namespace ABC.FoodDelivery.IdentityService.DTOs
{
    public class SigninDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}