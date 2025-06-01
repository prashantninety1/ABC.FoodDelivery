namespace ABC.FoodDelivery.DeliveryService.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public string Status { get; set; } // Assigned, PickedUp, Delivered, Canceled
        public DateTime? PickupTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
    }
}