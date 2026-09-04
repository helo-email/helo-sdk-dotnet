using HeloEmail.Sdk.Statistics;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Statistics;

public class StatisticsTests : BaseFixture
{
    private static (StatisticsClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new StatisticsClient(httpClient, NullLogger<StatisticsClient>.Instance), handler);
    }

    [Fact]
    public async Task RetrieveHourly_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RetrieveHourly(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/statistics/hourly", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task RetrieveDaily_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RetrieveDaily(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "America/New_York");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/statistics/daily", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task RetrieveTotals_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RetrieveTotals(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/statistics/totals", handler.Request!.RequestUri!.AbsolutePath);
    }
}
