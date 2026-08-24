using HeloEmail.Sdk.Errors;
using HeloEmail.Sdk.Webhooks;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.WebhookEndpoints;

public class WebhookEndpointsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static WebhooksClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<WebhooksClient>());

    [Fact]
    public async Task List_DoesNotThrow()
    {
        try
        {
            var result = await CreateClient().List();
            Assert.NotNull(result);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
    }

    [Fact]
    public async Task CreateRetrieveUpdateDelete_DoesNotThrow()
    {
        var client = CreateClient();
        WebhookResponse? created = null;
        try
        {
            created = await client.Create(new CreateWebhookRequest
            {
                Url = "https://example.com/webhook",
                Events = [WebhookEvent.Delivered, WebhookEvent.Bounced],
                Enabled = true,
            });
            Assert.NotNull(created);
            Assert.NotNull(created.Id);

            var retrieved = await client.Retrieve(created.Id);
            Assert.Equal(created.Id, retrieved.Id);

            var updated = await client.Update(created.Id, new UpdateWebhookRequest
            {
                Enabled = false,
            });
            Assert.Equal(created.Id, updated.Id);
        }
        catch (ApiErrorException ex)
        {
            outputHelper.WriteLine(ex.ResponseContent);
            throw;
        }
        finally
        {
            if (created != null)
                await client.Delete(created.Id);
        }
    }
}
