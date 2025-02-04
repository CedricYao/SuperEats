using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.DeliveryDrivers.Models;

public class DeliveryDriverRepositoryTests
{
    private readonly DeliveryDriverRepository _repository;

    public DeliveryDriverRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new DeliveryDriverRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllDeliveryDrivers()
    {
        // Arrange

        // Act
        var deliveryDrivers = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(deliveryDrivers);
        Assert.True(deliveryDrivers.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectDeliveryDriver()
    {
        // Arrange
        int existingId = 1;

        // Act
        var deliveryDriver = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(deliveryDriver);
        Assert.Equal(existingId, deliveryDriver.DriverId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var deliveryDriver = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deliveryDriver);
    }

    [Fact]
    public async Task AddDeliveryDriverAsync_AddsNewDeliveryDriver()
    {
        // Arrange
        var newDeliveryDriver = new DeliveryDriver
        {
            DriverId = 3,
            Name = "Driver 3",
            PhoneNumber = "111-222-3333",
            Availability = true
        };

        // Act
        await _repository.AddDeliveryDriverAsync(newDeliveryDriver);
        var addedDeliveryDriver = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedDeliveryDriver);
        Assert.Equal(newDeliveryDriver.Name, addedDeliveryDriver.Name);
    }

    [Fact]
    public async Task UpdateDeliveryDriverAsync_UpdatesExistingDeliveryDriver()
    {
        // Arrange
        int existingId = 1;
        var updatedDeliveryDriver = new DeliveryDriver
        {
            DriverId = existingId,
            Name = "Updated Driver Name",
            PhoneNumber = "555-555-5555",
            Availability = false
        };

        // Act
        await _repository.UpdateDeliveryDriverAsync(updatedDeliveryDriver);
        var retrievedDeliveryDriver = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedDeliveryDriver);
        Assert.Equal(updatedDeliveryDriver.Name, retrievedDeliveryDriver.Name);
        Assert.Equal(updatedDeliveryDriver.PhoneNumber, retrievedDeliveryDriver.PhoneNumber);
        Assert.Equal(updatedDeliveryDriver.Availability, retrievedDeliveryDriver.Availability);
    }

    [Fact]
    public async Task UpdateDeliveryDriverAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedDeliveryDriver = new DeliveryDriver
        {
            DriverId = nonExistingId,
            Name = "Updated Driver Name",
            PhoneNumber = "555-555-5555",
            Availability = false
        };

        // Act
        await _repository.UpdateDeliveryDriverAsync(updatedDeliveryDriver);
        var retrievedDeliveryDriver = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedDeliveryDriver);
    }


    [Fact]
    public async Task DeleteDeliveryDriverAsync_DeletesExistingDeliveryDriver()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteDeliveryDriverAsync(existingId);
        var deletedDeliveryDriver = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedDeliveryDriver);
    }

    [Fact]
    public async Task DeleteDeliveryDriverAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteDeliveryDriverAsync(nonExistingId);
        var deletedDeliveryDriver = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedDeliveryDriver); // Already null, but ensures no exception
    }
}
