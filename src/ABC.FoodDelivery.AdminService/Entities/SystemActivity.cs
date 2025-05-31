public class SystemActivity
{
    [Key]
    public Guid ActivityId { get; set; }

    [Required]
    public string Action { get; set; } // "UserDeleted", "OrderApproved"
    
    public string PerformedBy { get; set; } // Admin Email
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}