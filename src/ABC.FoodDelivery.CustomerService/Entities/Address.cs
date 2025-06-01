namespace ABC.FoodDelivery.CustomerService.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }  // Foreign Key
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }
        public bool IsPrimary { get; set; } // Indicates default address
        public DateTime CreatedAt { get; set; }

        public required Customer Customer { get; set; } // Navigation property
    }
}