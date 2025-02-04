using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.Ratings.Models;

public class RatingRepositoryTests
{
    private readonly RatingRepository _repository;

    public RatingRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new RatingRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllRatings()
    {
        // Arrange

        // Act
        var ratings = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(ratings);
        Assert.True(ratings.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectRating()
    {
        // Arrange
        int existingId = 1;

        // Act
        var rating = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(rating);
        Assert.Equal(existingId, rating.RatingId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var rating = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(rating);
    }

    [Fact]
    public async Task AddRatingAsync_AddsNewRating()
    {
        // Arrange
        var newRating = new Rating
        {
            RatingId = 3,
            UserId = 3,
            RestaurantId = 1,
            RatingValue = 5,
            Comment = "Excellent service!"
        };

        // Act
        await _repository.AddRatingAsync(newRating);
        var addedRating = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedRating);
        Assert.Equal(newRating.UserId, addedRating.UserId);
        Assert.Equal(newRating.RestaurantId, addedRating.RestaurantId);
        Assert.Equal(newRating.RatingValue, addedRating.RatingValue);
        Assert.Equal(newRating.Comment, addedRating.Comment);
    }

    [Fact]
    public async Task UpdateRatingAsync_UpdatesExistingRating()
    {
        // Arrange
        int existingId = 1;
        var updatedRating = new Rating
        {
            RatingId = existingId,
            UserId = 4,
            RestaurantId = 2,
            RatingValue = 3,
            Comment = "Updated Comment."
        };

        // Act
        await _repository.UpdateRatingAsync(updatedRating);
        var retrievedRating = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedRating);
        Assert.Equal(updatedRating.UserId, retrievedRating.UserId);
        Assert.Equal(updatedRating.RestaurantId, retrievedRating.RestaurantId);
        Assert.Equal(updatedRating.RatingValue, retrievedRating.RatingValue);
        Assert.Equal(updatedRating.Comment, retrievedRating.Comment);
    }

    [Fact]
    public async Task UpdateRatingAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedRating = new Rating
        {
            RatingId = nonExistingId,
            UserId = 4,
            RestaurantId = 2,
            RatingValue = 3,
            Comment = "Updated Comment."
        };

        // Act
        await _repository.UpdateRatingAsync(updatedRating);
        var retrievedRating = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedRating);
    }


    [Fact]
    public async Task DeleteRatingAsync_DeletesExistingRating()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteRatingAsync(existingId);
        var deletedRating = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedRating);
    }

    [Fact]
    public async Task DeleteRatingAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteRatingAsync(nonExistingId);
        var deletedRating = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedRating); // Already null, but ensures no exception
    }
}
