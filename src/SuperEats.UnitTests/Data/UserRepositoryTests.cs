using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.Users.Models;

public class UserRepositoryTests
{
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new UserRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllUsers()
    {
        // Arrange

        // Act
        var users = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(users);
        Assert.True(users.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectUser()
    {
        // Arrange
        int existingId = 1;

        // Act
        var user = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(existingId, user.UserId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var user = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task AddUserAsync_AddsNewUser()
    {
        // Arrange
        var newUser = new User
        {
            UserId = 3,
            Username = "testuser3",
            Email = "test3@example.com",
            Password = "password789"
        };

        // Act
        await _repository.AddUserAsync(newUser);
        var addedUser = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedUser);
        Assert.Equal(newUser.Username, addedUser.Username);
        Assert.Equal(newUser.Email, addedUser.Email);
    }

    [Fact]
    public async Task UpdateUserAsync_UpdatesExistingUser()
    {
        // Arrange
        int existingId = 1;
        var updatedUser = new User
        {
            UserId = existingId,
            Username = "updateduser",
            Email = "updated@example.com",
            Password = "newpassword"
        };

        // Act
        await _repository.UpdateUserAsync(updatedUser);
        var retrievedUser = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(updatedUser.Username, retrievedUser.Username);
        Assert.Equal(updatedUser.Email, retrievedUser.Email);
    }

    [Fact]
    public async Task UpdateUserAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedUser = new User
        {
            UserId = nonExistingId,
            Username = "updateduser",
            Email = "updated@example.com",
            Password = "newpassword"
        };

        // Act
        await _repository.UpdateUserAsync(updatedUser);
        var retrievedUser = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedUser);
    }


    [Fact]
    public async Task DeleteUserAsync_DeletesExistingUser()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeleteUserAsync(existingId);
        var deletedUser = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task DeleteUserAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteUserAsync(nonExistingId);
        var deletedUser = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedUser); // Already null, but ensures no exception
    }
}
