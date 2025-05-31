public class AdminDbContext : DbContext
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) {}

    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<SystemActivity> SystemActivities { get; set; }
}