namespace ABC.FoodDelivery.OrderService.DTOs
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Preparing, PickedUp, Delivered
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new List<OrderItemResponseDto>();
    }
}