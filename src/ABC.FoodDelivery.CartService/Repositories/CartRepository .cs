using ABC.FoodDelivery.CartService.Data;
using ABC.FoodDelivery.CartService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.CartService.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByIdAsync(Guid cartId)
        {
            return await _context.Carts.Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }


        public async Task<Cart> GetCartByCustomerIdAsync(Guid customerId)
        {
            return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}