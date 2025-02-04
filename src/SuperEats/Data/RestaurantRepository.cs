using SuperEats.Restaurants.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ConcurrentDictionary<int, Restaurant> _restaurants = new ConcurrentDictionary<int, Restaurant>();

    public RestaurantRepository()
    {
        // Add some sample restaurants for testing purposes.  Remove in production.
        _restaurants.TryAdd(1, new Restaurant
        {
            RestaurantId = 1,
            Name = "Pizza Place",
            Cuisine = "Italian",
            OperatingHours = "11:00 AM - 10:00 PM"
        });
        _restaurants.TryAdd(2, new Restaurant
        {
            RestaurantId = 2,
            Name = "Burger Joint",
            Cuisine = "American",
            OperatingHours = "10:00 AM - 9:00 PM"
        });

    }

    public Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return Task.FromResult(_restaurants.Values.AsEnumerable());
    }

    public Task<Restaurant?> GetByIdAsync(int restaurantId)
    {
        return Task.FromResult(_restaurants.GetValueOrDefault(restaurantId));
    }

    public Task AddRestaurantAsync(Restaurant restaurant)
    {
        _restaurants.TryAdd(restaurant.RestaurantId, restaurant);
        return Task.CompletedTask;
    }

    public Task UpdateRestaurantAsync(Restaurant restaurant)
    {
        _restaurants.TryGetValue(restaurant.RestaurantId, out var oldRestaurant);
        if (oldRestaurant != null)
        {
            _restaurants.TryUpdate(restaurant.RestaurantId, restaurant, oldRestaurant);
        }
            
         return Task.CompletedTask;
    }

    public Task DeleteRestaurantAsync(int restaurantId)
    {
        _restaurants.TryRemove(restaurantId, out Restaurant restaurant);
        return Task.CompletedTask;
    }
}

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int restaurantId);
    Task AddRestaurantAsync(Restaurant restaurant);
    Task UpdateRestaurantAsync(Restaurant restaurant);
    Task DeleteRestaurantAsync(int restaurantId);
}
