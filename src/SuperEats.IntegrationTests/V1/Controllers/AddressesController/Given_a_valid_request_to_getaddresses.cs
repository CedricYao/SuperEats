using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Newtonsoft.Json;
using SuperEats.Features.Addresses;
using System.Collections.Generic;
using SuperEats.IntegrationTests;

namespace SuperEats.IntegrationTests.V1.Controllers.AddressesController
{
    public partial class AddressesControllerTests
    {
        public class Given_a_valid_request_to_getaddresses : IClassFixture<TestingWebApplicationFactory>
        {
            private HttpResponseMessage response;

            public Given_a_valid_request_to_getaddresses(TestingWebApplicationFactory factory)
            {
                var client = factory.CreateClient();

                Task.Run(() => client.GetAsync("/api/v1/Addresses"))
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
                var expectedResponse = new GetAddresses.Response
                {
                    AddressDetails = new List<GetAddresses.AddressDetails>
                    {
                        new GetAddresses.AddressDetails
                        {
                            AddressId = 1,
                            StreetAddress = "123 Main St",
                            City = "Anytown",
                            State = "CA",
                            ZipCode = "12345",
                            Latitude = 34.052235m,
                            Longitude = -118.243683m
                        },
                        new GetAddresses.AddressDetails
                        {
                            AddressId = 2,
                            StreetAddress = "456 Oak Ave",
                            City = "Springfield",
                            State = "IL",
                            ZipCode = "67890",
                            Latitude = 39.783730m,
                            Longitude = -89.667475m
                        }
                    }
                };

                var content = response.Content.ReadAsStringAsync().Result;
                var actualResponse = JsonConvert.DeserializeObject<GetAddresses.Response>(content);

                actualResponse.Should().BeEquivalentTo(expectedResponse);

            }
        }
    }
}
