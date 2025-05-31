public class DeliveryDbContext : DbContext
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) {}

    public DbSet<DeliveryAssignment> DeliveryAssignments { get; set; }
}