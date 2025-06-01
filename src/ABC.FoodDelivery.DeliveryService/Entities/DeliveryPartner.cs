namespace ABC.FoodDelivery.DeliveryService.Entities
{
    public class DeliveryPartner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvailabilityStatus { get; set; } // Available, Busy, Offline
    }
}