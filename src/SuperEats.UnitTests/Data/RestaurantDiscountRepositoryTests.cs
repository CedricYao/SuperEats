using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.RestaurantDiscounts.Models;

public class RestaurantDiscountRepositoryTests
{
    private readonly RestaurantDiscountRepository _repository;

    public RestaurantDiscountRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new RestaurantDiscountRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllRestaurantDiscounts()
    {
        // Arrange

        // Act
        var restaurantDiscounts = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(restaurantDiscounts);
        Assert.True(restaurantDiscounts.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectRestaurantDiscount()
    {
        // Arrange
        int existingId = 1;

        // Act
        var restaurantDiscount = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(restaurantDiscount);
        Assert.Equal(existingId, restaurantDiscount.RestaurantDiscountId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var restaurantDiscount = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(restaurantDiscount);
    }

    [Fact]
    public async Task AddRestaurantDiscountAsync_AddsNewRestaurantDiscount()
    {
        // Arrange
        var newRestaurantDiscount = new RestaurantDiscount
        {
            RestaurantDiscountId = 3,
            RestaurantId = 1,
            Description = "New Discount",
            DiscountPercentage = 15.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(30)
        };

        // Act
        await _repository.AddRestaurantDiscountAsync(newRestaurantDiscount);
        var addedRestaurantDiscount = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedRestaurantDiscount);
        Assert.Equal(newRestaurantDiscount.RestaurantId, addedRestaurantDiscount.RestaurantId);
        Assert.Equal(newRestaurantDiscount.Description, addedRestaurantDiscount.Description);
        Assert.Equal(newRestaurantDiscount.DiscountPercentage, addedRestaurantDiscount.DiscountPercentage);
    }

    [Fact]
    public async Task UpdateRestaurantDiscountAsync_UpdatesExistingRestaurantDiscount()
    {
        // Arrange
        int existingId = 1;
        var updatedRestaurantDiscount = new RestaurantDiscount
        {
            RestaurantDiscountId = existingId,
            RestaurantId = 2,
            Description = "Updated Description",
            DiscountPercentage = 20.00m,
            StartTime = System.DateTime.Now.AddDays(1),
            EndTime = System.DateTime.Now.AddDays(60)
        };

        // Act
        await _repository.UpdateRestaurantDiscountAsync(updatedRestaurantDiscount);
        var retrievedRestaurantDiscount = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedRestaurantDiscount);
        Assert.Equal(updatedRestaurantDiscount.RestaurantId, retrievedRestaurantDiscount.RestaurantId);
        Assert.Equal(updatedRestaurantDiscount.Description, retrievedRestaurantDiscount.Description);
        Assert.Equal(updatedRestaurantDiscount.DiscountPercentage, retrievedRestaurantDiscount.DiscountPercentage);
        Assert.Equal(updatedRestaurantDiscount.StartTime, retrievedRestaurantDiscount.StartTime);
        Assert.Equal(updatedRestaurantDiscount.EndTime, retrievedRestaurantDiscount.EndTime);
    }

    [Fact]
    public async Task UpdateRestaurantDiscountAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedRestaurantDiscount = new RestaurantDiscount
        {
            RestaurantDiscountId = nonExistingId,
            RestaurantId = 2,
            Description = "Updated Description",
            DiscountPercentage = 20.00m,
            StartTime = System.DateTime.Now.AddDays(1),
            EndTime = System.DateTime.Now.AddDays(60)
        };

        // Act
        await _repository.UpdateRestaurantDiscountAsync(updatedRestaurantDiscount);
        var retrievedRestaurantDiscount = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedRestaurantDiscount);
    }


    [Fact]
    public async Task DeleteRestaurantDiscountAsync_DeletesExistingRestaurantDiscount()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteRestaurantDiscountAsync(existingId);
        var deletedRestaurantDiscount = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedRestaurantDiscount);
    }

    [Fact]
    public async Task DeleteRestaurantDiscountAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteRestaurantDiscountAsync(nonExistingId);
        var deletedRestaurantDiscount = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedRestaurantDiscount); // Already null, but ensures no exception
    }
}
