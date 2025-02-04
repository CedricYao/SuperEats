using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.Deliveries.Models;

public class DeliveryRepositoryTests
{
    private readonly DeliveryRepository _repository;

    public DeliveryRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new DeliveryRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllDeliveries()
    {
        // Arrange

        // Act
        var deliveries = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(deliveries);
        Assert.True(deliveries.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectDelivery()
    {
        // Arrange
        int existingId = 1;

        // Act
        var delivery = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(delivery);
        Assert.Equal(existingId, delivery.DeliveryId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var delivery = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(delivery);
    }

    [Fact]
    public async Task AddDeliveryAsync_AddsNewDelivery()
    {
        // Arrange
        var newDelivery = new Delivery
        {
            DeliveryId = 3,
            OrderId = 3,
            DriverId = 3,
            AssignedTime = System.DateTime.Now,
            PickupTime = System.DateTime.Now.AddMinutes(10),
            DeliveryTime = System.DateTime.Now.AddMinutes(30)
        };

        // Act
        await _repository.AddDeliveryAsync(newDelivery);
        var addedDelivery = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedDelivery);
        Assert.Equal(newDelivery.OrderId, addedDelivery.OrderId);
        Assert.Equal(newDelivery.DriverId, addedDelivery.DriverId);
    }

    [Fact]
    public async Task UpdateDeliveryAsync_UpdatesExistingDelivery()
    {
        // Arrange
        int existingId = 1;
        var updatedDelivery = new Delivery
        {
            DeliveryId = existingId,
            OrderId = 4,
            DriverId = 4,
            AssignedTime = System.DateTime.Now.AddDays(1),
            PickupTime = System.DateTime.Now.AddMinutes(15),
            DeliveryTime = System.DateTime.Now.AddMinutes(45)
        };

        // Act
        await _repository.UpdateDeliveryAsync(updatedDelivery);
        var retrievedDelivery = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedDelivery);
        Assert.Equal(updatedDelivery.OrderId, retrievedDelivery.OrderId);
        Assert.Equal(updatedDelivery.DriverId, retrievedDelivery.DriverId);
        Assert.Equal(updatedDelivery.AssignedTime, retrievedDelivery.AssignedTime);
        Assert.Equal(updatedDelivery.PickupTime, retrievedDelivery.PickupTime);
        Assert.Equal(updatedDelivery.DeliveryTime, retrievedDelivery.DeliveryTime);
    }

    [Fact]
    public async Task UpdateDeliveryAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedDelivery = new Delivery
        {
            DeliveryId = nonExistingId,
            OrderId = 4,
            DriverId = 4,
            AssignedTime = System.DateTime.Now.AddDays(1),
            PickupTime = System.DateTime.Now.AddMinutes(15),
            DeliveryTime = System.DateTime.Now.AddMinutes(45)
        };

        // Act
        await _repository.UpdateDeliveryAsync(updatedDelivery);
        var retrievedDelivery = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedDelivery);
    }


    [Fact]
    public async Task DeleteDeliveryAsync_DeletesExistingDelivery()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteDeliveryAsync(existingId);
        var deletedDelivery = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedDelivery);
    }

    [Fact]
    public async Task DeleteDeliveryAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteDeliveryAsync(nonExistingId);
        var deletedDelivery = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedDelivery); // Already null, but ensures no exception
    }
}
