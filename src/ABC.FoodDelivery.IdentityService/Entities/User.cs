using System.ComponentModel.DataAnnotations;

namespace ABC.FoodDelivery.IdentityService.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public required string Role { get; set; } // ADMIN, CUSTOMER, etc.

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}