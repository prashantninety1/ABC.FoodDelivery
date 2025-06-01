using System.Text.Json;
using ABC.FoodDelivery.CartService.DTOs;
using ABC.FoodDelivery.CartService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ABC.FoodDelivery.CartService.Functions
{
    public class CartItemFunctions
    {
        private readonly ICartItemService _cartItemService;

        public CartItemFunctions(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [Function("GetCartItems")]
        public async Task<IActionResult> GetCartItems(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cart/{cartId}/items")] HttpRequest req,
            Guid cartId)
        {
            var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cartId);
            if (!cartItems.Any()) return new NotFoundResult();

            var response = cartItems.Select(ci => new CartItemResponseDto
            {
                Id = ci.Id,
                MenuItemId = ci.MenuItemId,
                Quantity = ci.Quantity,
                Price = ci.Price
            }).ToList();

            return new OkObjectResult(response);
        }

        [Function("GetCartItemById")]
        public async Task<IActionResult> GetCartItemById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cart/items/{cartItemId}")] HttpRequest req,
            Guid cartItemId)
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null) return new NotFoundResult();

            var response = new CartItemResponseDto
            {
                Id = cartItem.Id,
                MenuItemId = cartItem.MenuItemId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Price
            };

            return new OkObjectResult(response);
        }

        [Function("AddCartItem")]
        public async Task<IActionResult> AddCartItem(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cart/{cartId}/items")] HttpRequest req,
            Guid cartId)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var cartItemDto = JsonSerializer.Deserialize<CartItemDto>(requestBody);

            var addedItem = await _cartItemService.AddCartItemAsync(cartId, cartItemDto);
            return new CreatedResult($"cart/{cartId}/items/{addedItem.Id}", addedItem);
        }

        [Function("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "cart/items/{cartItemId}")] HttpRequest req,
            Guid cartItemId)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateData = JsonSerializer.Deserialize<UpdateCartItemDto>(requestBody);

            await _cartItemService.UpdateCartItemAsync(cartItemId, updateData.Quantity);
            return new NoContentResult();
        }

        [Function("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "cart/items/{cartItemId}")] HttpRequest req,
            Guid cartItemId)
        {
            await _cartItemService.RemoveCartItemAsync(cartItemId);
            return new NoContentResult();
        }
    }
}