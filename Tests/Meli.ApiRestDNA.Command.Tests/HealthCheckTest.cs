using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Meli.ApiRestDNA.Tests
{
    public class HealthCheckTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HealthCheckTest(CustomWebApplicationFactory<Startup> factory) =>
            _client = factory.CreateClient();

        [Fact]
        public async Task GetStatus_Default_Returns200Ok()
        {
            var response = await this._client.GetAsync("/");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
