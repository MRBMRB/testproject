using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestProject.Tests;

public sealed class ApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private sealed record Health(string Status);

    [Fact]
    public async Task The_root_says_what_this_is()
    {
        using var client = factory.CreateClient();
        var text = await client.GetStringAsync("/", TestContext.Current.CancellationToken);
        Assert.StartsWith("testproject:", text, StringComparison.Ordinal);
    }

    [Fact]
    public async Task Health_answers_ok()
    {
        using var client = factory.CreateClient();
        using var response = await client.GetAsync("/health", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var health = await response.Content.ReadFromJsonAsync<Health>(TestContext.Current.CancellationToken);
        Assert.Equal("ok", health?.Status);
    }

    [Fact]
    public async Task An_unknown_path_is_not_found()
    {
        using var client = factory.CreateClient();
        using var response = await client.GetAsync("/missing", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
