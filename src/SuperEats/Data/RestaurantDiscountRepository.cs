using SuperEats.Features.RestaurantDiscounts.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RestaurantDiscountRepository : IRestaurantDiscountRepository
{
    private readonly ConcurrentDictionary<int, RestaurantDiscount> _restaurantDiscounts = new ConcurrentDictionary<int, RestaurantDiscount>();

    public RestaurantDiscountRepository()
    {
        // Add some sample restaurant discounts for testing purposes.  Remove in production.
        _restaurantDiscounts.TryAdd(1, new RestaurantDiscount
        {
            RestaurantDiscountId = 1,
            RestaurantId = 1,
            Description = "10% off on all pizzas",
            DiscountPercentage = 10.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(7)
        });
        _restaurantDiscounts.TryAdd(2, new RestaurantDiscount
        {
            RestaurantDiscountId = 2,
            RestaurantId = 2,
            Description = "$5 off on orders over $30",
            DiscountPercentage = 5.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(14)
        });
    }

    public Task<IEnumerable<RestaurantDiscount>> GetAllAsync()
    {
        return Task.FromResult(_restaurantDiscounts.Values.AsEnumerable());
    }

    public Task<RestaurantDiscount?> GetByIdAsync(int restaurantDiscountId)
    {
        return Task.FromResult(_restaurantDiscounts.GetValueOrDefault(restaurantDiscountId));
    }
    public Task AddRestaurantDiscountAsync(RestaurantDiscount restaurantDiscount)
    {
        _restaurantDiscounts.TryAdd(restaurantDiscount.RestaurantDiscountId, restaurantDiscount);
        return Task.CompletedTask;
    }

    public Task UpdateRestaurantDiscountAsync(RestaurantDiscount restaurantDiscount)
    {
        _restaurantDiscounts.TryGetValue(restaurantDiscount.RestaurantDiscountId, out var oldRestaurantDiscount);
        if (oldRestaurantDiscount != null)
        {
            _restaurantDiscounts.TryUpdate(restaurantDiscount.RestaurantDiscountId, restaurantDiscount, oldRestaurantDiscount);
        }
        return Task.CompletedTask;
    }

    public Task DeleteRestaurantDiscountAsync(int restaurantDiscountId)
    {
        _restaurantDiscounts.TryRemove(restaurantDiscountId, out RestaurantDiscount restaurantDiscount);
        return Task.CompletedTask;
    }
}

public interface IRestaurantDiscountRepository
{
    Task<IEnumerable<RestaurantDiscount>> GetAllAsync();
    Task<RestaurantDiscount?> GetByIdAsync(int restaurantDiscountId);
    Task AddRestaurantDiscountAsync(RestaurantDiscount restaurantDiscount);
    Task UpdateRestaurantDiscountAsync(RestaurantDiscount restaurantDiscount);
    Task DeleteRestaurantDiscountAsync(int restaurantDiscountId);
}
