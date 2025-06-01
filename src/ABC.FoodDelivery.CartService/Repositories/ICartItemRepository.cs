using ABC.FoodDelivery.CartService.Entities;

namespace ABC.FoodDelivery.CartService.Repositories
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(Guid cartItemId);
        Task<CartItem> GetCartItemByIdAsync(Guid itemId);
    }
}