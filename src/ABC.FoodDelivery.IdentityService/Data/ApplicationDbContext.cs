using ABC.FoodDelivery.IdentityService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.IdentityService.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}