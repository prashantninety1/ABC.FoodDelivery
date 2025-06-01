namespace ABC.FoodDelivery.DeliveryService.DTOs
{
    public class AssignDeliveryDto
    {
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
    }
}