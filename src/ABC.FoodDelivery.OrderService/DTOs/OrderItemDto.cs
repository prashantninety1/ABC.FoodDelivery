namespace ABC.FoodDelivery.OrderService.DTOs
{
    public class OrderItemDto
    {
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}