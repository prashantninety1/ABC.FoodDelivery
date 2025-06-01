using System.Text.Json;
using ABC.FoodDelivery.CartService.DTOs;
using ABC.FoodDelivery.CartService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ABC.FoodDelivery.CartService.Functions
{
    public class CartFunctions
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;

        public CartFunctions(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
        }

        // Get Cart by Customer ID
        [Function("GetCartByCustomerId")]
        public async Task<IActionResult> GetCartByCustomerId(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cart/{customerId}")] HttpRequest req,
            Guid customerId)
        {
            var cart = await _cartService.GetCartByCustomerIdAsync(customerId);
            if (cart == null) return new NotFoundResult();

            return new OkObjectResult(new CartResponseDto
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
                TotalAmount = cart.TotalAmount,
                Items = cart.Items.Select(ci => new CartItemResponseDto
                {
                    Id = ci.Id,
                    MenuItemId = ci.MenuItemId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            });
        }

        // // Add Item to Cart
        // [Function("AddCartItem")]
        // public async Task<IActionResult> AddCartItem(
        //     [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cart/{cartId}/items")] HttpRequest req,
        //     Guid cartId)
        // {
        //     var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //     var cartItemDto = JsonSerializer.Deserialize<CartItemDto>(requestBody);

        //     var addedItem = await _cartItemService.AddCartItemAsync(cartId, cartItemDto);
        //     return new CreatedResult($"cart/{cartId}/items/{addedItem.Id}", addedItem);
        // }

        // // Update Cart Item Quantity
        // [Function("UpdateCartItem")]
        // public async Task<IActionResult> UpdateCartItem(
        //     [HttpTrigger(AuthorizationLevel.Function, "put", Route = "cart/items/{cartItemId}")] HttpRequest req,
        //     Guid cartItemId)
        // {
        //     var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //     var updateData = JsonSerializer.Deserialize<UpdateCartItemDto>(requestBody);

        //     await _cartItemService.UpdateCartItemAsync(cartItemId, updateData.Quantity);
        //     return new NoContentResult();
        // }

        // // Remove Item from Cart
        // [Function("RemoveCartItem")]
        // public async Task<IActionResult> RemoveCartItem(
        //     [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "cart/items/{cartItemId}")] HttpRequest req,
        //     Guid cartItemId)
        // {
        //     await _cartItemService.RemoveCartItemAsync(cartItemId);
        //     return new NoContentResult();
        // }

        // Checkout Cart
        [Function("CheckoutCart")]
        public async Task<IActionResult> CheckoutCart(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cart/{customerId}/checkout")] HttpRequest req,
            Guid customerId)
        {
            await _cartService.CheckoutCartAsync(customerId);
            return new OkObjectResult($"Checkout successful for customer: {customerId}");
        }
    }
}