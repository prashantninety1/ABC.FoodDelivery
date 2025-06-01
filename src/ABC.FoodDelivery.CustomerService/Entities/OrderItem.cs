namespace ABC.FoodDelivery.CustomerService.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; } // Foreign Key
        public Guid MenuItemId { get; set; } // Reference to restaurant's menu item
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public required Order Order { get; set; } // Navigation property
    }
}