using System.Net;
using System.Net.Http.Json;
using CryptoTrackerApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CryptoTrackerApi.IntegrationTests;

public class BlockchainControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task FetchAndStoreBlockchainData_ReturnsOk_OrServiceUnavailableIfApiFails()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.PostAsync("/api/Blockchain/fetch-store", null);

        // Assert
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            Assert.NotNull(result);
            Assert.Contains("successfully", result["message"]);
        }
        else
        {
            // If BlockCypher API fails, the Middleware returns 503
            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }
    }

    [Fact]
    public async Task GetBlockchainHistory_ReturnsOkWithList()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Blockchain/history");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var records = await response.Content.ReadFromJsonAsync<List<BaseBlockchainDataDto>>();
        Assert.NotNull(records);
    }

    [Theory]
    [InlineData("BtcMain")]
    [InlineData("EthMain")]
    public async Task GetBlockchainHistoryByNetwork_WithValidNetwork_ReturnsOk(string networkName)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/Blockchain/history/{networkName}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var records = await response.Content.ReadFromJsonAsync<List<BaseBlockchainDataDto>>();
        Assert.NotNull(records);
    }

    [Fact]
    public async Task GetBlockchainHistoryByNetwork_WithInvalidNetwork_ReturnsBadRequestDueToEnumValidation()
    {
        // Arrange
        var client = factory.CreateClient();
        string invalidNetwork = "InvalidNetworkName";

        // Act
        var response = await client.GetAsync($"/api/Blockchain/history/{invalidNetwork}");

        // Assert
        // .NET automatically validates the Enum and returns 400 Bad Request
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
