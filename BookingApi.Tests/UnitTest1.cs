using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace BookingApi.Tests;

public class AvailableHomesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AvailableHomesTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ShouldReturnAvailableHomeForValidRange()
    {
        var response = await _client.GetAsync("/api/available-homes?startDate=2025-07-15&endDate=2025-07-16");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AvailableHomeResponse>();
        Assert.NotNull(result);
        Assert.Equal("OK", result.Status);
        Assert.Contains(result.Homes, h => h.HomeId == "123");
    }

    [Fact]
    public async Task ShouldReturnEmptyIfNoMatch()
    {
        var response = await _client.GetAsync("/api/available-homes?startDate=2025-07-10&endDate=2025-07-11");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AvailableHomeResponse>();
        Assert.NotNull(result);
        Assert.Empty(result.Homes);
    }

    [Fact]
    public async Task ShouldReturnBadRequestForInvalidDate()
    {
        var response = await _client.GetAsync("/api/available-homes?startDate=invalid&endDate=2025-07-16");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
