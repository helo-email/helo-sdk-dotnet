using HeloEmail.Sdk.Errors;
using HeloEmail.Sdk.Statistics;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.Statistics;

public class StatisticsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static StatisticsClient CreateClient() => new(HttpClient, XUnitLogger.CreateLogger<StatisticsClient>());

    [Fact]
    public async Task RetrieveHourly_DoesNotThrow()
    {
        var statsClient = CreateClient();
        try
        {
            var result = await statsClient.RetrieveHourly(DateTimeOffset.UtcNow.AddHours(-12), DateTimeOffset.UtcNow);
            Assert.NotNull(result);
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
        var statsClient = CreateClient();
        try
        {
            var result = await statsClient.RetrieveDaily(DateTimeOffset.UtcNow.AddDays(-7), DateTimeOffset.UtcNow, tz);
            Assert.NotNull(result);
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
        var statsClient = CreateClient();
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