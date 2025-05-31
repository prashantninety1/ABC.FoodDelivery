using System.ComponentModel.DataAnnotations;

namespace ABC.FoodDelivery.RestaurantService.DTOs
{
    public class RestaurantDto
    {
        [Required]
    public required string Name { get; set; }

    [Required]
    public Guid OwnerId { get; set; } // Linking to IdentityService

    public required string Address { get; set; }
    }
}