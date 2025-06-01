using ABC.FoodDelivery.CartService.DTOs;
using ABC.FoodDelivery.CartService.Entities;

namespace ABC.FoodDelivery.CartService.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartByCustomerIdAsync(Guid customerId);
        Task<Cart> CreateCartAsync(Guid customerId);
        Task<CartItem> AddItemToCartAsync(Guid customerId, CartItemDto cartItemDto);
        Task UpdateCartItemAsync(Guid customerId, Guid itemId, int quantity);
        Task RemoveItemFromCartAsync(Guid customerId, Guid itemId);
        Task CheckoutCartAsync(Guid customerId);
    }
}