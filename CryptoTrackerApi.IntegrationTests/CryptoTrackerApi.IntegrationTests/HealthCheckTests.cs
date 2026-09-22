using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CryptoTrackerApi.IntegrationTests;

public class HealthCheckTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{

    [Fact]
    public async Task HealthCheck_ReturnsOkAndHealthy()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}
