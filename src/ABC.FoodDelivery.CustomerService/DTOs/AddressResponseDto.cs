namespace ABC.FoodDelivery.CustomerService.DTOs
{
    public class AddressResponseDto
    {
        public Guid Id { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}