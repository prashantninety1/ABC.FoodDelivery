namespace ABC.FoodDelivery.OrderService.DTOs
{
    public class UpdateOrderStatusDto
    {
        public string Status { get; set; } // Pending, Confirmed, Preparing, PickedUp, Delivered
    }
}