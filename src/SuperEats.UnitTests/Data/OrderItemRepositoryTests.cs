using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.OrderItems.Models;

public class OrderItemRepositoryTests
{
    private readonly OrderItemRepository _repository;

    public OrderItemRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new OrderItemRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllOrderItems()
    {
        // Arrange

        // Act
        var orderItems = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(orderItems);
        Assert.True(orderItems.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectOrderItem()
    {
        // Arrange
        int existingId = 1;

        // Act
        var orderItem = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(orderItem);
        Assert.Equal(existingId, orderItem.OrderItemId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var orderItem = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(orderItem);
    }

    [Fact]
    public async Task AddOrderItemAsync_AddsNewOrderItem()
    {
        // Arrange
        var newOrderItem = new OrderItem
        {
            OrderItemId = 3,
            OrderId = 1,
            ItemId = 2,
            Quantity = 3,
            PriceAtOrder = 7.99m
        };

        // Act
        await _repository.AddOrderItemAsync(newOrderItem);
        var addedOrderItem = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedOrderItem);
        Assert.Equal(newOrderItem.OrderId, addedOrderItem.OrderId);
        Assert.Equal(newOrderItem.ItemId, addedOrderItem.ItemId);
        Assert.Equal(newOrderItem.Quantity, addedOrderItem.Quantity);
        Assert.Equal(newOrderItem.PriceAtOrder, addedOrderItem.PriceAtOrder);
    }

    [Fact]
    public async Task UpdateOrderItemAsync_UpdatesExistingOrderItem()
    {
        // Arrange
        int existingId = 1;
        var updatedOrderItem = new OrderItem
        {
            OrderItemId = existingId,
            OrderId = 2,
            ItemId = 3,
            Quantity = 4,
            PriceAtOrder = 9.99m
        };

        // Act
        await _repository.UpdateOrderItemAsync(updatedOrderItem);
        var retrievedOrderItem = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedOrderItem);
        Assert.Equal(updatedOrderItem.OrderId, retrievedOrderItem.OrderId);
        Assert.Equal(updatedOrderItem.ItemId, retrievedOrderItem.ItemId);
        Assert.Equal(updatedOrderItem.Quantity, retrievedOrderItem.Quantity);
        Assert.Equal(updatedOrderItem.PriceAtOrder, retrievedOrderItem.PriceAtOrder);
    }

    [Fact]
    public async Task UpdateOrderItemAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedOrderItem = new OrderItem
        {
            OrderItemId = nonExistingId,
            OrderId = 2,
            ItemId = 3,
            Quantity = 4,
            PriceAtOrder = 9.99m
        };

        // Act
        await _repository.UpdateOrderItemAsync(updatedOrderItem);
        var retrievedOrderItem = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedOrderItem);
    }


    [Fact]
    public async Task DeleteOrderItemAsync_DeletesExistingOrderItem()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteOrderItemAsync(existingId);
        var deletedOrderItem = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedOrderItem);
    }

    [Fact]
    public async Task DeleteOrderItemAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteOrderItemAsync(nonExistingId);
        var deletedOrderItem = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedOrderItem); // Already null, but ensures no exception
    }
}
