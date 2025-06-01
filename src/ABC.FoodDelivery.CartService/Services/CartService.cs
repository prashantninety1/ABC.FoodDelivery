using ABC.FoodDelivery.CartService.DTOs;
using ABC.FoodDelivery.CartService.Entities;
using ABC.FoodDelivery.CartService.Repositories;

namespace ABC.FoodDelivery.CartService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Cart> GetCartByCustomerIdAsync(Guid customerId)
        {
            return await _cartRepository.GetCartByCustomerIdAsync(customerId);
        }

        public async Task<Cart> CreateCartAsync(Guid customerId)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                TotalAmount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _cartRepository.CreateCartAsync(cart);
        }

        public async Task<CartItem> AddItemToCartAsync(Guid customerId, CartItemDto cartItemDto)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId) ?? await CreateCartAsync(customerId);

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                MenuItemId = cartItemDto.MenuItemId,
                Quantity = cartItemDto.Quantity,
                Price = cartItemDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _cartItemRepository.AddCartItemAsync(cartItem);

            cart.TotalAmount += cartItem.Price * cartItem.Quantity;
            await _cartRepository.UpdateCartAsync(cart);

            return cartItem;
        }

        public async Task UpdateCartItemAsync(Guid customerId, Guid itemId, int quantity)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(itemId);
            if (cartItem == null) return;

            cartItem.Quantity = quantity;
            await _cartItemRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task RemoveItemFromCartAsync(Guid customerId, Guid itemId)
        {
            await _cartItemRepository.RemoveCartItemAsync(itemId);
        }

        public async Task CheckoutCartAsync(Guid customerId)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null || !cart.Items.Any()) throw new Exception("Cart is empty!");

            // Send cart details to OrderService for order creation (Event-driven approach)
            // Delete the cart after checkout
            await _cartRepository.DeleteCartAsync(cart.Id);
        }
    }
}