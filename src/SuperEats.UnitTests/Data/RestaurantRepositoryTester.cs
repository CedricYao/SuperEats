using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SuperEats.Restaurants.Models;
using Xunit;

namespace SuperEats.UnitTests.Data
{
    public class RestaurantRepositoryTests
    {
         [Fact]
        public async Task GetAllAsync_ReturnsAllRestaurants()
        {
            // Arrange
            var repository = new RestaurantRepository();

            // Act
            var restaurants = await repository.GetAllAsync();

            // Assert
            restaurants.Should().NotBeNull();
            restaurants.Count().Should().Be(2);
            restaurants.Should().Contain(r => r.Name == "Pizza Place");
            restaurants.Should().Contain(r => r.Name == "Burger Joint");
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsCorrectRestaurant()
        {
            // Arrange
            var repository = new RestaurantRepository();
            int existingRestaurantId = 1;

            // Act
            var restaurant = await repository.GetByIdAsync(existingRestaurantId);

            // Assert
            restaurant.Should().NotBeNull();
            restaurant.RestaurantId.Should().Be(existingRestaurantId);
            restaurant.Name.Should().Be("Pizza Place");
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            var repository = new RestaurantRepository();
            int nonExistingRestaurantId = 99;

            // Act
            var restaurant = await repository.GetByIdAsync(nonExistingRestaurantId);

            // Assert
            restaurant.Should().BeNull();
        }

         [Fact]
        public async Task AddRestaurantAsync_AddsNewRestaurant()
        {
            // Arrange
            var repository = new RestaurantRepository();
            var newRestaurant = new Restaurant
            {
                RestaurantId = 3,
                Name = "Taco Truck",
                Cuisine = "Mexican",
                OperatingHours = "12:00 PM - 8:00 PM"
            };

            // Act
            await repository.AddRestaurantAsync(newRestaurant);
            var addedRestaurant = await repository.GetByIdAsync(newRestaurant.RestaurantId);
            var allRestaurants = await repository.GetAllAsync();

            // Assert
            addedRestaurant.Should().NotBeNull();
            addedRestaurant.RestaurantId.Should().Be(newRestaurant.RestaurantId);
            addedRestaurant.Name.Should().Be(newRestaurant.Name);
            allRestaurants.Count().Should().Be(3);
            allRestaurants.Should().Contain(r => r.RestaurantId == newRestaurant.RestaurantId);
        }

         [Fact]
        public async Task UpdateRestaurantAsync_ExistingRestaurant_UpdatesRestaurant()
        {
            // Arrange
            var repository = new RestaurantRepository();
            int existingRestaurantId = 1;
            var updatedRestaurant = new Restaurant
            {
                RestaurantId = existingRestaurantId,
                Name = "Updated Pizza Place",
                Cuisine = "Italian",
                OperatingHours = "12:00 PM - 11:00 PM",
                UpdatedAt = DateTime.Now
            };

            // Act
            await repository.UpdateRestaurantAsync(updatedRestaurant);
            var restaurant = await repository.GetByIdAsync(existingRestaurantId);
            var allRestaurants = await repository.GetAllAsync();

            // Assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(updatedRestaurant.Name);
            restaurant.OperatingHours.Should().Be(updatedRestaurant.OperatingHours);
            allRestaurants.Count().Should().Be(2);
        }

         [Fact]
         public async Task UpdateRestaurantAsync_NonExistingRestaurant_DoesNotUpdateRestaurant()
         {
            // Arrange
            var repository = new RestaurantRepository();
            int nonExistingRestaurantId = 99;
            var updatedRestaurant = new Restaurant
            {
                RestaurantId = nonExistingRestaurantId,
                Name = "Updated Test Restaurant",
                Cuisine = "Italian",
                OperatingHours = "12:00 PM - 11:00 PM",
                UpdatedAt = DateTime.Now
            };
           
             //Act
             await repository.UpdateRestaurantAsync(updatedRestaurant);
            var restaurant = await repository.GetByIdAsync(nonExistingRestaurantId);

             // Assert
            restaurant.Should().BeNull();
        }

         [Fact]
        public async Task DeleteRestaurantAsync_ExistingRestaurant_RemovesRestaurant()
        {
            // Arrange
            var repository = new RestaurantRepository();
            int existingRestaurantId = 1;

            // Act
            await repository.DeleteRestaurantAsync(existingRestaurantId);
            var deletedRestaurant = await repository.GetByIdAsync(existingRestaurantId);
            var allRestaurants = await repository.GetAllAsync();

            // Assert
            deletedRestaurant.Should().BeNull();
            allRestaurants.Count().Should().Be(1);
            allRestaurants.Should().NotContain(r => r.RestaurantId == existingRestaurantId);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_NonExistingRestaurant_DoesNotThrowException()
        {
            // Arrange
            var repository = new RestaurantRepository();
            int nonExistingRestaurantId = 99;

            // Act
            await repository.DeleteRestaurantAsync(nonExistingRestaurantId);
            var allRestaurants = await repository.GetAllAsync();

            // Assert
             allRestaurants.Count().Should().Be(2);
        }

         [Fact]
        public async Task UpdateRestaurantLastHourDeal_ExistingRestaurant_UpdatesIsLastHourDeal()
        {
            // Arrange
            var repository = new RestaurantRepository();
            int existingRestaurantId = 1;
            bool isLastHourDeal = true;
           
             //Act
             await repository.UpdateRestaurantLastHourDeal(existingRestaurantId, isLastHourDeal);
            var restaurant = await repository.GetByIdAsync(existingRestaurantId);

             // Assert
            restaurant.Should().NotBeNull();
            restaurant.IsLastHourDeal.Should().Be(isLastHourDeal);
        }

        [Fact]
         public async Task UpdateRestaurantLastHourDeal_NonExistingRestaurant_DoesNotUpdateIsLastHourDeal()
         {
            // Arrange
            var repository = new RestaurantRepository();
            int nonExistingRestaurantId = 99;
            bool isLastHourDeal = true;
           
             //Act
             await repository.UpdateRestaurantLastHourDeal(nonExistingRestaurantId, isLastHourDeal);
            var restaurant = await repository.GetByIdAsync(nonExistingRestaurantId);

             // Assert
            restaurant.Should().BeNull();
        }
    }
}
