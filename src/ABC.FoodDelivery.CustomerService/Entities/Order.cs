namespace ABC.FoodDelivery.CustomerService.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; } // Foreign Key
        public Guid RestaurantId { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; } // Placed, Picked, Delivered, Cancelled
        public decimal TotalAmount { get; set; }

        public required Customer Customer { get; set; } // Navigation property
        public required ICollection<OrderItem> Items { get; set; } // One-to-Many Relationship
    }
}