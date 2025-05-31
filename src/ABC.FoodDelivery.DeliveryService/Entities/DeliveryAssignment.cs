public class DeliveryAssignment
{
    [Key]
    public Guid AssignmentId { get; set; }

    [Required]
    public Guid OrderId { get; set; } // FK from OrderService

    [Required]
    public Guid DeliveryPartnerId { get; set; } // FK from IdentityService

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}