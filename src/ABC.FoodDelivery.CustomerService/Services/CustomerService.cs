using ABC.FoodDelivery.CustomerService.Entities;
using ABC.FoodDelivery.CustomerService.Repositories;

namespace ABC.FoodDelivery.CustomerService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return (List<Customer>)await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            customer.CreatedAt = DateTime.UtcNow;
            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            customer.UpdatedAt = DateTime.UtcNow;
            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}