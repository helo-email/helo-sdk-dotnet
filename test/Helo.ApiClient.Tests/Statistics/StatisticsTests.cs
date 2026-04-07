using Helo.ApiClient.Statistics;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace Helo.ApiClient.Tests.Statistics;

public class StatisticsTests
{
    private static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri("http://localhost:8000"),
        DefaultRequestHeaders = { { "Authorization", $"Bearer {Settings.DevApiKey}" } }
    };

    [Fact]
    public async Task RetrieveHourly_DoesNotThrow()
    {
        var statsClient = new StatisticsClient(HttpClient, XUnitLogger.CreateLogger<StatisticsClient>());
        var result = await statsClient.RetrieveHourly(DateTimeOffset.UtcNow.AddHours(-12), DateTimeOffset.UtcNow);
        Assert.NotNull(result);
        Assert.Empty(result.Results);
    }
}