public class Customer
{
    [Key]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid UserId { get; set; } // FK from IdentityService

    public string Address { get; set; }
    public string Phone { get; set; }

    public List<Order> Orders { get; set; } // Navigation Property
}