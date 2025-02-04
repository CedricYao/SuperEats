using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Newtonsoft.Json;
using SuperEats.Features.Restaurants;
using System.Collections.Generic;

namespace SuperEats.IntegrationTests.V1.Controllers.RestaurantController
{
    public partial class RestaurantControllerTests
    {
        public class Given_a_valid_request_to_getrestaurants : IClassFixture<TestingWebApplicationFactory>
        {
            private HttpResponseMessage response;
            
            public Given_a_valid_request_to_getrestaurants(TestingWebApplicationFactory factory)
            {
                var client = factory.CreateClient();

                Task.Run(() => client.GetAsync("/api/v1/Restaurants"))
                    .ContinueWith(task => response = task.Result)
                    .Wait();
            }

            [Fact]
            public void Should_be_success()
            {
                response.EnsureSuccessStatusCode();
            }

            [Fact]
            public Task Should_have_correct_headers()
            {
                response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
                return Task.CompletedTask;
            }

            [Fact]
            public void Should_have_correct_response()
            {
                 var expectedResponse = new GetRestaurants.Response
                {
                    RestaurantDetails = new List<GetRestaurants.RestaurantDetails>
                    {
                        new GetRestaurants.RestaurantDetails
                        {
                            RestaurantId = 1,
                            Name = "Pizza Place",
                            Cuisine = "Italian",
                            OperatingHours = "11:00 AM - 10:00 PM"
                        },
                         new GetRestaurants.RestaurantDetails
                        {
                            RestaurantId = 2,
                            Name = "Burger Joint",
                            Cuisine = "American",
                            OperatingHours = "10:00 AM - 9:00 PM"
                         }
                    }
                };
                
                var content = response.Content.ReadAsStringAsync().Result;
                var actualResponse = JsonConvert.DeserializeObject<GetRestaurants.Response>(content);

                actualResponse.Should().BeEquivalentTo(expectedResponse);
                
            }
        }
    }
}
