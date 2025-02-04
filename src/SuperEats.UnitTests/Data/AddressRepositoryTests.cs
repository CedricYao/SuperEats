using SuperEats.Features.Addresses.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class AddressRepositoryTests
{
    private readonly AddressRepository _repository;

    public AddressRepositoryTests()
    {
        _repository = new AddressRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllAddresses()
    {
        // Act
        var addresses = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(addresses);
        Assert.Equal(2, addresses.Count()); // Assuming the sample data is loaded
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsAddress()
    {
        // Arrange
        int existingId = 1;

        // Act
        var address = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(address);
        Assert.Equal(existingId, address.AddressId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var address = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(address);
    }

    [Fact]
    public async Task AddAddressAsync_AddsNewAddress()
    {
        // Arrange
        var newAddress = new Address
        {
            AddressId = 3,
            StreetAddress = "789 Pine Ln",
            City = "Hill Valley",
            State = "UT",
            ZipCode = "54321",
            Latitude = 40.7608m,
            Longitude = -111.8910m
        };

        // Act
        await _repository.AddAddressAsync(newAddress);
        var addedAddress = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedAddress);
        Assert.Equal(newAddress.StreetAddress, addedAddress.StreetAddress);
    }

    [Fact]
    public async Task UpdateAddressAsync_ExistingAddress_UpdatesAddress()
    {
        // Arrange
        var updatedAddress = new Address
        {
            AddressId = 1,
            StreetAddress = "123 Updated St",
            City = "Newtown",
            State = "GA",
            ZipCode = "56789",
            Latitude = 33.7490m,
            Longitude = -84.3880m
        };

        // Act
        await _repository.UpdateAddressAsync(updatedAddress);
        var retrievedAddress = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(retrievedAddress);
        Assert.Equal(updatedAddress.StreetAddress, retrievedAddress.StreetAddress);
        Assert.Equal(updatedAddress.City, retrievedAddress.City);
        Assert.Equal(updatedAddress.State, retrievedAddress.State);
        Assert.Equal(updatedAddress.ZipCode, retrievedAddress.ZipCode);
        Assert.Equal(updatedAddress.Latitude, retrievedAddress.Latitude);
        Assert.Equal(updatedAddress.Longitude, retrievedAddress.Longitude);
    }

    [Fact]
    public async Task UpdateAddressAsync_NonExistingAddress_DoesNotUpdate()
    {
        // Arrange
        var updatedAddress = new Address
        {
            AddressId = 999, // Non-existing ID
            StreetAddress = "Updated Address",
            City = "Updated City",
            State = "XX",
            ZipCode = "00000",
            Latitude = 0.00m,
            Longitude = 0.00m
        };

        // Act
        await _repository.UpdateAddressAsync(updatedAddress);
        var retrievedAddress = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(retrievedAddress); //Should still be null since it didn't exist in the first place.
    }

    [Fact]
    public async Task DeleteAddressAsync_ExistingId_DeletesAddress()
    {
        // Arrange
        int existingId = 2;

        // Act
        await _repository.DeleteAddressAsync(existingId);
        var deletedAddress = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedAddress);
    }

    [Fact]
    public async Task DeleteAddressAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeleteAddressAsync(nonExistingId);

        // Assert
        // No exception should be thrown
        Assert.True(true); //dummy assert to ensure the test runs and doesn't error.
    }
}
