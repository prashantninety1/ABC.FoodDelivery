using ABC.FoodDelivery.CustomerService.Data;
using ABC.FoodDelivery.CustomerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.CustomerService.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(Guid customerId)
        {
            return await _context.Addresses.Where(a => a.CustomerId == customerId).ToListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(Guid addressId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task UpdateAddressAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            var address = await GetAddressByIdAsync(addressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}