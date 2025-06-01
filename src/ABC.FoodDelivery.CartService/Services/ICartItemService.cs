using ABC.FoodDelivery.CartService.DTOs;
using ABC.FoodDelivery.CartService.Entities;

namespace ABC.FoodDelivery.CartService.Services
{
    public interface ICartItemService
    {
        Task<List<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
        Task<CartItem> GetCartItemByIdAsync(Guid cartItemId);
        Task<CartItem> AddCartItemAsync(Guid cartId, CartItemDto cartItemDto);
        Task UpdateCartItemAsync(Guid cartItemId, int quantity);
        Task RemoveCartItemAsync(Guid cartItemId);
    }
}