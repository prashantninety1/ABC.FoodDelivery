namespace ABC.FoodDelivery.CustomerService.Entities
{
    public class FavoriteRestaurant
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }  // Foreign Key
        public Guid RestaurantId { get; set; }  // Reference to restaurant entity
        public DateTime AddedAt { get; set; }

        public required Customer Customer { get; set; }
    }
}