namespace ABC.FoodDelivery.RestaurantService.DTOs
{
    public class UpdateMenuItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsAvailable { get; set; }
    }
}