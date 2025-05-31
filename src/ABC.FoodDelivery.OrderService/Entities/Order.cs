public class Order
{
    [Key]
    public Guid OrderId { get; set; }

    [Required]
    public Guid CustomerId { get; set; } // FK from CustomerService

    [Required]
    public Guid RestaurantId { get; set; } // FK from RestaurantService

    public OrderStatus Status { get; set; } // Enum

    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}