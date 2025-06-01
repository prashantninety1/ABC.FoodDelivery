using System.Text.Json;
using ABC.FoodDelivery.CustomerService.DTOs;
using ABC.FoodDelivery.CustomerService.Entities;
using ABC.FoodDelivery.CustomerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ABC.FoodDelivery.CustomerService.Functions
{
    public class CustomerAddressFunctions
    {
        private readonly ICustomerAddressService _customerAddressService;

        public CustomerAddressFunctions(ICustomerAddressService customerAddressService)
        {
            _customerAddressService = customerAddressService;
        }

        [Function("GetCustomerAddresses")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{customerId}/addresses")] HttpRequest req,
            Guid customerId)
        {
            var addresses = await _customerAddressService.GetAddressesByCustomerIdAsync(customerId);
            if (!addresses.Any()) return new NotFoundResult();

            var response = addresses.Select(a => new AddressResponseDto
            {
                Id = a.Id,
                Street = a.Street,
                City = a.City,
                State = a.State,
                PostalCode = a.PostalCode,
            }).ToList();

            return new OkObjectResult(response);
        }

        [Function("GetAddressById")]
        public async Task<IActionResult> GetAddressById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/addresses/{addressId}")] HttpRequest req,
        Guid addressId)
        {
            var address = await _customerAddressService.GetAddressByIdAsync(addressId);
            if (address == null) return new NotFoundResult();

            var response = new AddressResponseDto
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            };

            return new OkObjectResult(response);
        }

        [Function("CreateCustomerAddress")]
    public async Task<IActionResult> CreateCustomerAddress(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers/{customerId}/addresses")] HttpRequest req,
        Guid customerId)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var addressDto = JsonSerializer.Deserialize<AddressDto>(requestBody);

        var address = new Address
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Street = addressDto.Street,
            City = addressDto.City,
            State = addressDto.State,
            Country = addressDto.Country,
            PostalCode = addressDto.PostalCode,
            IsPrimary = addressDto.IsPrimary,
            Customer = null,
            CreatedAt = DateTime.UtcNow
        };

        var createdAddress = await _customerAddressService.CreateAddressAsync(address);

        return new CreatedResult($"customers/{customerId}/addresses/{createdAddress.Id}", new AddressResponseDto
        {
            Id = createdAddress.Id,
            Street = createdAddress.Street,
            City = createdAddress.City,
            State = createdAddress.State,
            PostalCode = createdAddress.PostalCode
        });
    }


    }
}