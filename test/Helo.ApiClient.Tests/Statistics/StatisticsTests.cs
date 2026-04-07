using Helo.ApiClient.Errors;
using Helo.ApiClient.Statistics;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace Helo.ApiClient.Tests.Statistics;

public class StatisticsTests(ITestOutputHelper outputHelper)
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
        try
        {
            var result = await statsClient.RetrieveHourly(DateTimeOffset.UtcNow.AddHours(-12), DateTimeOffset.UtcNow);
            Assert.NotNull(result);
            Assert.Empty(result.Results);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Theory]
    [InlineData("UTC")]
    [InlineData("utc")]
    public async Task RetrieveDaily_DoesNotThrow(string tz)
    {
        var statsClient = new StatisticsClient(HttpClient, XUnitLogger.CreateLogger<StatisticsClient>());
        try
        {
            var result = await statsClient.RetrieveDaily(DateTimeOffset.UtcNow.AddDays(-7), DateTimeOffset.UtcNow, tz);
            Assert.NotNull(result);
            Assert.Empty(result.Results);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task RetrieveTotals_DoesNotThrow()
    {
        var statsClient = new StatisticsClient(HttpClient, XUnitLogger.CreateLogger<StatisticsClient>());
        try
        {
            var result = await statsClient.RetrieveTotals(DateTimeOffset.UtcNow.AddDays(-7), DateTimeOffset.UtcNow);
            Assert.NotNull(result);
            Assert.NotNull(result.Transactional);
            Assert.NotNull(result.Broadcast);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }
}