using ABC.FoodDelivery.CustomerService.Data;
using ABC.FoodDelivery.CustomerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.FoodDelivery.CustomerService.Repositories
{
    public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId) =>
            await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == customerId);

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
            await _context.Customers.ToListAsync();

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }


        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}