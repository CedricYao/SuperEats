using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.Orders.Models;

public class OrderRepositoryTests
{
    private readonly OrderRepository _repository;

    public OrderRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new OrderRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllOrders()
    {
        // Arrange

        // Act
        var orders = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(orders);
        Assert.True(orders.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectOrder()
    {
        // Arrange
        int existingId = 1;

        // Act
        var order = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(existingId, order.OrderId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var order = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(order);
    }

    [Fact]
    public async Task AddOrderAsync_AddsNewOrder()
    {
        // Arrange
        var newOrder = new Order
        {
            OrderId = 3,
            UserId = 1,
            RestaurantId = 2,
            TotalAmount = 35.50m,
            FinalAmount = 32.50m,
            Status = "Confirmed"
        };

        // Act
        await _repository.AddOrderAsync(newOrder);
        var addedOrder = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedOrder);
        Assert.Equal(newOrder.UserId, addedOrder.UserId);
        Assert.Equal(newOrder.RestaurantId, addedOrder.RestaurantId);
        Assert.Equal(newOrder.TotalAmount, addedOrder.TotalAmount);
        Assert.Equal(newOrder.FinalAmount, addedOrder.FinalAmount);
        Assert.Equal(newOrder.Status, addedOrder.Status);
    }

    [Fact]
    public async Task UpdateOrderAsync_UpdatesExistingOrder()
    {
        // Arrange
        int existingId = 1;
        var updatedOrder = new Order
        {
            OrderId = existingId,
            UserId = 2,
            RestaurantId = 3,
            TotalAmount = 40.00m,
            FinalAmount = 36.00m,
            Status = "Shipped"
        };

        // Act
        await _repository.UpdateOrderAsync(updatedOrder);
        var retrievedOrder = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedOrder);
        Assert.Equal(updatedOrder.UserId, retrievedOrder.UserId);
        Assert.Equal(updatedOrder.RestaurantId, retrievedOrder.RestaurantId);
        Assert.Equal(updatedOrder.TotalAmount, retrievedOrder.TotalAmount);
        Assert.Equal(updatedOrder.FinalAmount, retrievedOrder.FinalAmount);
        Assert.Equal(updatedOrder.Status, retrievedOrder.Status);
    }

    [Fact]
    public async Task UpdateOrderAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedOrder = new Order
        {
            OrderId = nonExistingId,
            UserId = 2,
            RestaurantId = 3,
            TotalAmount = 40.00m,
            FinalAmount = 36.00m,
            Status = "Shipped"
        };

        // Act
        await _repository.UpdateOrderAsync(updatedOrder);
        var retrievedOrder = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedOrder);
    }


    [Fact]
    public async Task DeleteOrderAsync_DeletesExistingOrder()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteOrderAsync(existingId);
        var deletedOrder = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedOrder);
    }

    [Fact]
    public async Task DeleteOrderAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteOrderAsync(nonExistingId);
        var deletedOrder = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedOrder); // Already null, but ensures no exception
    }
}
