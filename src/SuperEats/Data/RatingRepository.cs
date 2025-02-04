using SuperEats.Features.Ratings.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RatingRepository : IRatingRepository
{
    private readonly ConcurrentDictionary<int, Rating> _ratings = new ConcurrentDictionary<int, Rating>();

    public RatingRepository()
    {
        // Add some sample ratings for testing purposes.  Remove in production.
        _ratings.TryAdd(1, new Rating
        {
            RatingId = 1,
            UserId = 1,
            RestaurantId = 1,
            RatingValue = 5,
            Comment = "Great pizza!"
        });
        _ratings.TryAdd(2, new Rating
        {
            RatingId = 2,
            UserId = 2,
            RestaurantId = 2,
            RatingValue = 4,
            Comment = "Good burger."
        });
    }

    public Task<IEnumerable<Rating>> GetAllAsync()
    {
        return Task.FromResult(_ratings.Values.AsEnumerable());
    }

    public Task<Rating?> GetByIdAsync(int ratingId)
    {
        return Task.FromResult(_ratings.GetValueOrDefault(ratingId));
    }
    public Task AddRatingAsync(Rating rating)
    {
        _ratings.TryAdd(rating.RatingId, rating);
        return Task.CompletedTask;
    }

    public Task UpdateRatingAsync(Rating rating)
    {
        _ratings.TryGetValue(rating.RatingId, out var oldRating);
        if (oldRating != null)
        {
            _ratings.TryUpdate(rating.RatingId, rating, oldRating);
        }
        return Task.CompletedTask;
    }

    public Task DeleteRatingAsync(int ratingId)
    {
        _ratings.TryRemove(ratingId, out Rating rating);
        return Task.CompletedTask;
    }
}

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetAllAsync();
    Task<Rating?> GetByIdAsync(int ratingId);
    Task AddRatingAsync(Rating rating);
    Task UpdateRatingAsync(Rating rating);
    Task DeleteRatingAsync(int ratingId);
}
