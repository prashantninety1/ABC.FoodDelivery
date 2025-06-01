using ABC.FoodDelivery.CustomerService.Entities;

namespace ABC.FoodDelivery.CustomerService.Services
{
    public interface ICustomerAddressService
    {
        Task<List<Address>> GetAddressesByCustomerIdAsync(Guid customerId);
        Task<Address> GetAddressByIdAsync(Guid addressId);
        Task<Address> CreateAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(Guid addressId);
    }
}