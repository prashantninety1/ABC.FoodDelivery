using ABC.FoodDelivery.RestaurantService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.RestaurantService.Data
{
    public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                .HasOne<Restaurant>()
                .WithMany()
                .HasForeignKey(m => m.RestaurantId);
        }
    }
}