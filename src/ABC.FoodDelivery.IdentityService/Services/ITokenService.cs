using ABC.FoodDelivery.IdentityService.Entities;

namespace ABC.FoodDelivery.IdentityService.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}