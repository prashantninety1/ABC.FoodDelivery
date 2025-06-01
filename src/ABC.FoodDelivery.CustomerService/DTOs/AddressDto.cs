namespace ABC.FoodDelivery.CustomerService.DTOs
{
    public class AddressDto
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public bool IsPrimary { get; set; } 
        public required string PostalCode { get; set; }
    }
}