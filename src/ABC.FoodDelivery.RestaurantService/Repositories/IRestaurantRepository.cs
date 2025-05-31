using ABC.FoodDelivery.RestaurantService.Entities;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetRestaurantsAsync();
    Task<Restaurant> GetRestaurantByIdAsync(Guid restaurantId);
    Task AddRestaurantAsync(Restaurant restaurant);
    Task UpdateRestaurantAsync(Restaurant restaurant);
}