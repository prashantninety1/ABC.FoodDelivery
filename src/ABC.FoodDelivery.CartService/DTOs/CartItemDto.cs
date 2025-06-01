namespace ABC.FoodDelivery.CartService.DTOs
{
    public class CartItemDto
    {
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}