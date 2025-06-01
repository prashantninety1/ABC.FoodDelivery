using System.Text.Json;
using ABC.FoodDelivery.OrderService.DTOs;
using ABC.FoodDelivery.OrderService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ABC.FoodDelivery.OrderService.Functions
{
    public class OrderFunctions
    {
        private readonly IOrderService _orderService;

        public OrderFunctions(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Create Order
        [Function("CreateOrder")]
        public async Task<IActionResult> CreateOrder(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orders/{customerId}/create")] HttpRequest req,
            Guid customerId)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var orderRequest = JsonSerializer.Deserialize<OrderRequestDto>(requestBody);

            var order = await _orderService.CreateOrderAsync(customerId, orderRequest.RestaurantId, orderRequest.Items);

            return new CreatedResult($"orders/{order.Id}", order);
        }

        // Get Order by ID
        [Function("GetOrderById")]
        public async Task<IActionResult> GetOrderById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{orderId}")] HttpRequest req,
            Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return new NotFoundResult();

            return new OkObjectResult(order);
        }

        // Get Customer Order History
        [Function("GetCustomerOrders")]
        public async Task<IActionResult> GetCustomerOrders(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{customerId}/history")] HttpRequest req,
            Guid customerId)
        {
            var orders = await _orderService.GetCustomerOrdersAsync(customerId);
            if (!orders.Any()) return new NotFoundResult();

            return new OkObjectResult(orders);
        }

        // Get Orders by Restaurant ID
        [Function("GetRestaurantOrders")]
        public async Task<IActionResult> GetRestaurantOrders(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{restaurantId}/all")] HttpRequest req,
            Guid restaurantId)
        {
            var orders = await _orderService.GetRestaurantOrdersAsync(restaurantId);
            if (!orders.Any()) return new NotFoundResult();

            return new OkObjectResult(orders);
        }

        // Update Order Status
        [Function("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "orders/{orderId}/updateStatus")] HttpRequest req,
            Guid orderId)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateStatusDto = JsonSerializer.Deserialize<UpdateOrderStatusDto>(requestBody);

            await _orderService.UpdateOrderStatusAsync(orderId, updateStatusDto.Status);
            return new NoContentResult();
        }

        // Cancel Order
        [Function("CancelOrder")]
        public async Task<IActionResult> CancelOrder(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "orders/{orderId}/cancel")] HttpRequest req,
            Guid orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return new NoContentResult();
        }
    }
}