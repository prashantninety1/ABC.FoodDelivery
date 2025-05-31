using System.ComponentModel.DataAnnotations;

namespace ABC.FoodDelivery.RestaurantService.Entities
{
    public class Restaurant
    {
        [Key]
        public Guid RestaurantId { get; set; }
        [Required]
        public Guid OwnerId { get; set; } // FK from IdentityService
        [Required]
        public required string Name { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}