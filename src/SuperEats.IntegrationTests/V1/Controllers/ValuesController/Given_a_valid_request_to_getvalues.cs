using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SuperEats.IntegrationTests.V1.Controllers.ValuesController
{
    public partial class ValuesControllerTests
    {
        public class Given_a_valid_request_to_getvalues : IClassFixture<TestingWebApplicationFactory>
        {
            private HttpResponseMessage response;

            public Given_a_valid_request_to_getvalues(TestingWebApplicationFactory factory)
            {
                var client = factory.CreateClient();

                Task.Run(() => client.GetAsync("/api/v1/Values/12345678/123"))
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
                response.Content.ReadAsStringAsync().Result.Should().Be("{\"result\":\"{\\\"Param1\\\":\\\"12345678\\\",\\\"Param2\\\":\\\"123\\\"}\"}");
            }
        }
    }
}
