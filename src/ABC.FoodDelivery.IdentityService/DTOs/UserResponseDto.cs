namespace ABC.FoodDelivery.IdentityService.DTOs
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public required string Message { get; set; }
    }
}