using System.ComponentModel.DataAnnotations;

namespace ABC.FoodDelivery.RestaurantService.DTOs
{
    public class MenuItemDto
    {
        [Required]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}