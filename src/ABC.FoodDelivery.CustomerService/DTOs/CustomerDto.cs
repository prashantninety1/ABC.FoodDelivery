namespace ABC.FoodDelivery.CustomerService.DTOs
{
    public class CustomerDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; } // Unique
        public required string PhoneNumber { get; set; } // Unique
        public DateTime DateOfBirth { get; set; }
    }
}