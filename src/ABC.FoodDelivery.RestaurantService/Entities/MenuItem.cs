using System.ComponentModel.DataAnnotations;

namespace ABC.FoodDelivery.RestaurantService.Entities
{
    public class MenuItem
    {
        [Key]
        public Guid MenuItemId { get; set; }
        [Required]
        public Guid RestaurantId { get; set; } // FK from Restaurant
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}