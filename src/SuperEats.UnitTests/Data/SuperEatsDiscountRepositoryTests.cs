using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.SuperEatsDiscounts.Models;

public class SuperEatsDiscountRepositoryTests
{
    private readonly SuperEatsDiscountRepository _repository;

    public SuperEatsDiscountRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new SuperEatsDiscountRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSuperEatsDiscounts()
    {
        // Arrange

        // Act
        var superEatsDiscounts = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(superEatsDiscounts);
        Assert.True(superEatsDiscounts.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectSuperEatsDiscount()
    {
        // Arrange
        int existingId = 1;

        // Act
        var superEatsDiscount = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(superEatsDiscount);
        Assert.Equal(existingId, superEatsDiscount.SuperEatsDiscountId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var superEatsDiscount = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(superEatsDiscount);
    }

    [Fact]
    public async Task AddSuperEatsDiscountAsync_AddsNewSuperEatsDiscount()
    {
        // Arrange
        var newSuperEatsDiscount = new SuperEatsDiscount
        {
            SuperEatsDiscountId = 3,
            Description = "Test Discount",
            DiscountPercentage = 25.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(7)
        };

        // Act
        await _repository.AddSuperEatsDiscountAsync(newSuperEatsDiscount);
        var addedSuperEatsDiscount = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedSuperEatsDiscount);
        Assert.Equal(newSuperEatsDiscount.Description, addedSuperEatsDiscount.Description);
        Assert.Equal(newSuperEatsDiscount.DiscountPercentage, addedSuperEatsDiscount.DiscountPercentage);
    }

    [Fact]
    public async Task UpdateSuperEatsDiscountAsync_UpdatesExistingSuperEatsDiscount()
    {
        // Arrange
        int existingId = 1;
        var updatedSuperEatsDiscount = new SuperEatsDiscount
        {
            SuperEatsDiscountId = existingId,
            Description = "Updated Discount Description",
            DiscountPercentage = 30.00m,
            StartTime = System.DateTime.Now.AddDays(1),
            EndTime = System.DateTime.Now.AddDays(60)
        };

        // Act
        await _repository.UpdateSuperEatsDiscountAsync(updatedSuperEatsDiscount);
        var retrievedSuperEatsDiscount = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedSuperEatsDiscount);
        Assert.Equal(updatedSuperEatsDiscount.Description, retrievedSuperEatsDiscount.Description);
        Assert.Equal(updatedSuperEatsDiscount.DiscountPercentage, retrievedSuperEatsDiscount.DiscountPercentage);
        Assert.Equal(updatedSuperEatsDiscount.StartTime, retrievedSuperEatsDiscount.StartTime);
        Assert.Equal(updatedSuperEatsDiscount.EndTime, retrievedSuperEatsDiscount.EndTime);
    }

    [Fact]
    public async Task UpdateSuperEatsDiscountAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedSuperEatsDiscount = new SuperEatsDiscount
        {
            SuperEatsDiscountId = nonExistingId,
            Description = "Updated Description",
            DiscountPercentage = 20.00m,
            StartTime = System.DateTime.Now.AddDays(1),
            EndTime = System.DateTime.Now.AddDays(60)
        };

        // Act
        await _repository.UpdateSuperEatsDiscountAsync(updatedSuperEatsDiscount);
        var retrievedSuperEatsDiscount = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedSuperEatsDiscount);
    }


    [Fact]
    public async Task DeleteSuperEatsDiscountAsync_DeletesExistingSuperEatsDiscount()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteSuperEatsDiscountAsync(existingId);
        var deletedSuperEatsDiscount = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedSuperEatsDiscount);
    }

    [Fact]
    public async Task DeleteSuperEatsDiscountAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteSuperEatsDiscountAsync(nonExistingId);
        var deletedSuperEatsDiscount = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedSuperEatsDiscount); // Already null, but ensures no exception
    }
}
