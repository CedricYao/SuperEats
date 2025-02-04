using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.MenuItems.Models;

public class MenuItemRepositoryTests
{
    private readonly MenuItemRepository _repository;

    public MenuItemRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new MenuItemRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllMenuItems()
    {
        // Arrange

        // Act
        var menuItems = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(menuItems);
        Assert.True(menuItems.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectMenuItem()
    {
        // Arrange
        int existingId = 1;

        // Act
        var menuItem = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(menuItem);
        Assert.Equal(existingId, menuItem.ItemId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var menuItem = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(menuItem);
    }

    [Fact]
    public async Task AddMenuItemAsync_AddsNewMenuItem()
    {
        // Arrange
        var newMenuItem = new MenuItem
        {
            ItemId = 3,
            RestaurantId = 1,
            Name = "New Item",
            Price = 5.99m
        };

        // Act
        await _repository.AddMenuItemAsync(newMenuItem);
        var addedMenuItem = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedMenuItem);
        Assert.Equal(newMenuItem.Name, addedMenuItem.Name);
        Assert.Equal(newMenuItem.Price, addedMenuItem.Price);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_UpdatesExistingMenuItem()
    {
        // Arrange
        int existingId = 1;
        var updatedMenuItem = new MenuItem
        {
            ItemId = existingId,
            RestaurantId = 2,
            Name = "Updated Item",
            Price = 9.99m
        };

        // Act
        await _repository.UpdateMenuItemAsync(updatedMenuItem);
        var retrievedMenuItem = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedMenuItem);
        Assert.Equal(updatedMenuItem.RestaurantId, retrievedMenuItem.RestaurantId);
        Assert.Equal(updatedMenuItem.Name, retrievedMenuItem.Name);
        Assert.Equal(updatedMenuItem.Price, retrievedMenuItem.Price);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedMenuItem = new MenuItem
        {
            ItemId = nonExistingId,
            RestaurantId = 2,
            Name = "Updated Item",
            Price = 9.99m
        };

        // Act
        await _repository.UpdateMenuItemAsync(updatedMenuItem);
        var retrievedMenuItem = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedMenuItem);
    }


    [Fact]
    public async Task DeleteMenuItemAsync_DeletesExistingMenuItem()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteMenuItemAsync(existingId);
        var deletedMenuItem = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedMenuItem);
    }

    [Fact]
    public async Task DeleteMenuItemAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteMenuItemAsync(nonExistingId);
        var deletedMenuItem = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedMenuItem); // Already null, but ensures no exception
    }
}
