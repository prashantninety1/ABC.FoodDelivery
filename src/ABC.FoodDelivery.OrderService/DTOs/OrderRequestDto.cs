namespace ABC.FoodDelivery.OrderService.DTOs
{
    public class OrderRequestDto
    {
        public Guid RestaurantId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}