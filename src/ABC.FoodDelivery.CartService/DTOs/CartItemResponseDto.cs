namespace ABC.FoodDelivery.CartService.DTOs
{
    public class CartItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}