public class AdminUser
{
    [Key]
    public Guid AdminId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public string Role { get; set; } // SuperAdmin, Manager, Auditor
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}