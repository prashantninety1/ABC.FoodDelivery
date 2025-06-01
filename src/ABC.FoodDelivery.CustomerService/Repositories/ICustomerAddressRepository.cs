using ABC.FoodDelivery.CustomerService.Entities;

namespace ABC.FoodDelivery.CustomerService.Repositories
{
    public interface ICustomerAddressRepository
    {
        Task<List<Address>> GetAddressesByCustomerIdAsync(Guid customerId);
        Task<Address> GetAddressByIdAsync(Guid addressId);
        Task<Address> CreateAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(Guid addressId);
    }
}