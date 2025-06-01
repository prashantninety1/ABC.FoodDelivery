using ABC.FoodDelivery.CartService.Entities;

namespace ABC.FoodDelivery.CartService.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(Guid cartId);
        Task<Cart> GetCartByCustomerIdAsync(Guid customerId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(Guid cartId);
    }
}