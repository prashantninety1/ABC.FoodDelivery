namespace ABC.FoodDelivery.CustomerService.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }  // Unique Identifier
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; } // Unique
        public required string PhoneNumber { get; set; } // Unique
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } // Account creation timestamp
        public DateTime UpdatedAt { get; set; }
        public required ICollection<Address> Addresses { get; set; } // One-to-Many Relationship
        public required ICollection<Order> Orders { get; set; } // One-to-Many Relationship
    }
}