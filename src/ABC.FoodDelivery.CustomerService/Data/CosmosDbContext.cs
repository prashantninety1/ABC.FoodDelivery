using ABC.FoodDelivery.CustomerService.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace ABC.FoodDelivery.CustomerService.Data
{
    public class CosmosDbContext
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _customerPreferencesContainer;

        public CosmosDbContext(IConfiguration configuration)
        {
            _cosmosClient = new CosmosClient(configuration["CosmosDb:ConnectionString"]);
            var database = _cosmosClient.GetDatabase("FoodDeliveryDB");
            _customerPreferencesContainer = database.GetContainer("CustomerPreferences");
        }

        public async Task AddCustomerPreferenceAsync(FavoriteRestaurant favoriteRestaurant)
        {
            await _customerPreferencesContainer.CreateItemAsync(favoriteRestaurant, new PartitionKey(favoriteRestaurant.CustomerId.ToString()));
        }

        public async Task<IEnumerable<FavoriteRestaurant>> GetCustomerPreferencesAsync(Guid customerId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.CustomerId = @customerId")
                .WithParameter("@customerId", customerId.ToString());

            var iterator = _customerPreferencesContainer.GetItemQueryIterator<FavoriteRestaurant>(query);
            var results = new List<FavoriteRestaurant>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }
    }
}