using ABC.FoodDelivery.RestaurantService.DTOs;
using ABC.FoodDelivery.RestaurantService.Entities;

namespace ABC.FoodDelivery.RestaurantService.Services
{
    public class Service(IRestaurantRepository restaurantRepository)
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

        public async Task<List<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _restaurantRepository.GetRestaurantsAsync();
        }

        public async Task<Restaurant> RegisterRestaurantAsync(RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                RestaurantId = Guid.NewGuid(),
                Name = restaurantDto.Name,
                OwnerId = restaurantDto.OwnerId,
                Address = restaurantDto.Address,
                IsActive = true
            };

            await _restaurantRepository.AddRestaurantAsync(restaurant);
            return restaurant;
        }
    }
}