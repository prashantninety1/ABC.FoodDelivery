using ABC.FoodDelivery.CartService.DTOs;
using ABC.FoodDelivery.CartService.Entities;
using ABC.FoodDelivery.CartService.Repositories;

namespace ABC.FoodDelivery.CartService.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;

        public CartItemService(ICartItemRepository cartItemRepository, ICartRepository cartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(Guid cartId)
        {
            return await _cartItemRepository.GetCartItemsByCartIdAsync(cartId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(Guid cartItemId)
        {
            return await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
        }

        public async Task<CartItem> AddCartItemAsync(Guid cartId, CartItemDto cartItemDto)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            if (cart == null) throw new Exception("Cart not found!");

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cartId,
                MenuItemId = cartItemDto.MenuItemId,
                Quantity = cartItemDto.Quantity,
                Price = cartItemDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _cartItemRepository.AddCartItemAsync(cartItem);

            // Update cart total amount
            cart.TotalAmount += cartItem.Price * cartItem.Quantity;
            await _cartRepository.UpdateCartAsync(cart);

            return cartItem;
        }

        public async Task UpdateCartItemAsync(Guid cartItemId, int quantity)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null) throw new Exception("CartItem not found!");

            cartItem.Quantity = quantity;
            await _cartItemRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null) throw new Exception("CartItem not found!");

            await _cartItemRepository.RemoveCartItemAsync(cartItemId);

            var cart = await _cartRepository.GetCartByIdAsync(cartItem.CartId);
            if (cart != null)
            {
                cart.TotalAmount -= cartItem.Price * cartItem.Quantity;
                await _cartRepository.UpdateCartAsync(cart);
            }
        }
    }
}