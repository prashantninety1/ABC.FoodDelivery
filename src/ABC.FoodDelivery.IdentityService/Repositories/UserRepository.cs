using ABC.FoodDelivery.IdentityService.Data;
using ABC.FoodDelivery.IdentityService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.IdentityService.Repositories
{
    public class UserRepository(IdentityDbContext context) : IUserRepository
    {
        private readonly IdentityDbContext _context = context;

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}