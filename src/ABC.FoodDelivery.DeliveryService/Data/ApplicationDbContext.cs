using ABC.FoodDelivery.DeliveryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.DeliveryService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSets for DeliveryService
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Delivery>()
                .HasOne<DeliveryPartner>()
                .WithMany()
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .Property(d => d.Status)
                .HasDefaultValue("Assigned");
        }
    }
}