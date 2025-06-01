namespace ABC.FoodDelivery.DeliveryService.DTOs
{
    public class DeliveryResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public string Status { get; set; } // Assigned, PickedUp, Delivered, Canceled
        public DateTime? PickupTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
    }
}