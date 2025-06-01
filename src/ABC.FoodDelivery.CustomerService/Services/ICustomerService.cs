using ABC.FoodDelivery.CustomerService.Entities;

namespace ABC.FoodDelivery.CustomerService.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
    }
}