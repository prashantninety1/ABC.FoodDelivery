using ABC.FoodDelivery.CustomerService.Entities;
using ABC.FoodDelivery.CustomerService.Repositories;

namespace ABC.FoodDelivery.CustomerService.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ICustomerAddressRepository _addressRepository;

        public CustomerAddressService(ICustomerAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(Guid customerId)
        {
            return await _addressRepository.GetAddressesByCustomerIdAsync(customerId);
        }

        public async Task<Address> GetAddressByIdAsync(Guid addressId)
        {
            return await _addressRepository.GetAddressByIdAsync(addressId);
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            address.Id = Guid.NewGuid();
            //address.CreatedAt = DateTime.UtcNow;
            return await _addressRepository.CreateAddressAsync(address);
        }

        public async Task UpdateAddressAsync(Address address)
        {
            await _addressRepository.UpdateAddressAsync(address);
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            await _addressRepository.DeleteAddressAsync(addressId);
        }
    }
}