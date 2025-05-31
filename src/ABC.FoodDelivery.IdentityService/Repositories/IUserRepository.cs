using ABC.FoodDelivery.IdentityService.Entities;

namespace ABC.FoodDelivery.IdentityService.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}