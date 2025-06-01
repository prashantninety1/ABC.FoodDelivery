using System.Text.Json;
using ABC.FoodDelivery.DeliveryService.DTOs;
using ABC.FoodDelivery.DeliveryService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

public class DeliveryFunctions
{
    private readonly IDeliveryService _deliveryService;

    public DeliveryFunctions(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    // Assign Delivery Partner to an Order
    [Function("AssignDelivery")]
    public async Task<IActionResult> AssignDelivery(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "delivery/{orderId}/assign/{driverId}")] HttpRequest req,
        Guid orderId, Guid driverId)
    {
        var delivery = await _deliveryService.AssignDeliveryAsync(orderId, driverId);
        return new CreatedResult($"delivery/{orderId}/status", delivery);
    }

    // Get Delivery Status by Order ID
    [Function("GetDeliveryByOrderId")]
    public async Task<IActionResult> GetDeliveryByOrderId(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "delivery/{orderId}/status")] HttpRequest req,
        Guid orderId)
    {
        var delivery = await _deliveryService.GetDeliveryByOrderIdAsync(orderId);
        if (delivery == null) return new NotFoundResult();

        return new OkObjectResult(delivery);
    }

    // Get Delivery History for a Driver
    [Function("GetDeliveriesByDriverId")]
    public async Task<IActionResult> GetDeliveriesByDriverId(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "delivery/partners/{driverId}/history")] HttpRequest req,
        Guid driverId)
    {
        var deliveries = await _deliveryService.GetDeliveriesByDriverIdAsync(driverId);
        if (!deliveries.Any()) return new NotFoundResult();

        return new OkObjectResult(deliveries);
    }

    // Update Delivery Status
    [Function("UpdateDeliveryStatus")]
    public async Task<IActionResult> UpdateDeliveryStatus(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "delivery/{orderId}/updateStatus")] HttpRequest req,
        Guid orderId)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var updateStatusDto = JsonSerializer.Deserialize<UpdateDeliveryStatusDto>(requestBody);

        await _deliveryService.UpdateDeliveryStatusAsync(orderId, updateStatusDto.Status);
        return new NoContentResult();
    }

    // Cancel Delivery before pickup
    [Function("CancelDelivery")]
    public async Task<IActionResult> CancelDelivery(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "delivery/{orderId}/cancel")] HttpRequest req,
        Guid orderId)
    {
        await _deliveryService.CancelDeliveryAsync(orderId);
        return new NoContentResult();
    }
}