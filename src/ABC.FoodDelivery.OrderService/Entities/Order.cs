namespace ABC.FoodDelivery.OrderService.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get; set; } // Reference to the restaurant
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Preparing, PickedUp, Delivered
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}