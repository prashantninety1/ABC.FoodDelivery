using System.Text.Json;
using ABC.FoodDelivery.CustomerService.DTOs;
using ABC.FoodDelivery.CustomerService.Entities;
using ABC.FoodDelivery.CustomerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ABC.FoodDelivery.CustomerService.Functions
{
    public class CustomerFunctions
    {
        private readonly ICustomerService _customerService;

        public CustomerFunctions(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Function("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{id}")] HttpRequest req,
            Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return new NotFoundResult();

            return new OkObjectResult(new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DateOfBirth = customer.DateOfBirth
            });
        }

        [Function("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var customerDto = JsonSerializer.Deserialize<CustomerDto>(requestBody);

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                DateOfBirth = customerDto.DateOfBirth,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Addresses = new List<Address>(), // Assuming Address is a complex type, initialize as needed
            };

            var createdCustomer = await _customerService.CreateCustomerAsync(customer);

            return new CreatedResult($"customers/{createdCustomer.Id}", new CustomerResponseDto
            {
                Id = createdCustomer.Id,
                FirstName = createdCustomer.FirstName,
                LastName = createdCustomer.LastName,
                Email = createdCustomer.Email,
                PhoneNumber = createdCustomer.PhoneNumber,
                DateOfBirth = createdCustomer.DateOfBirth
            });
        }

        [Function("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "customers/{id}")] HttpRequest req,
            Guid id)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var customerDto = JsonSerializer.Deserialize<CustomerDto>(requestBody);

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return new NotFoundResult();

            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.DateOfBirth = customerDto.DateOfBirth;
            customer.UpdatedAt = DateTime.UtcNow;

            await _customerService.UpdateCustomerAsync(customer);

            return new NoContentResult();
        }

        [Function("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "customers/{id}")] HttpRequest req,
            Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return new NotFoundResult();

            await _customerService.DeleteCustomerAsync(id);
            return new NoContentResult();
        }

    }
}