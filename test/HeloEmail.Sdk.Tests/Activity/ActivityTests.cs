using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Errors;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.Activity;

public class ActivityTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static ActivityClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<ActivityClient>());

    [Fact]
    public async Task ListEvents_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().ListEvents();
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task ListEvents_WithFilters_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().ListEvents(
                startDate: DateTimeOffset.UtcNow.AddDays(-7),
                endDate: DateTimeOffset.UtcNow,
                limit: 10,
                mailType: MailType.Transactional,
                eventTypes: [EventType.Delivered, EventType.Bounced]);
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task ListMessages_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().ListMessages();
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task ListMessages_WithFilters_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().ListMessages(
                startDate: DateTimeOffset.UtcNow.AddDays(-7),
                endDate: DateTimeOffset.UtcNow,
                limit: 10,
                mailType: MailType.Transactional,
                status: MessageStatus.Sent);
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }
}
