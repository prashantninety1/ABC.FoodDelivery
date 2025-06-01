using ABC.FoodDelivery.CustomerService.Entities;

namespace ABC.FoodDelivery.CustomerService.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}