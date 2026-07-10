using HeloEmail.Sdk.Errors;
using HeloEmail.Sdk.WebhookEndpoints;
using Meziantou.Extensions.Logging.Xunit.v3;

namespace HeloEmail.Sdk.Tests.WebhookEndpoints;

public class WebhookEndpointsTests(ITestOutputHelper outputHelper) : BaseFixture
{
    private static WebhookEndpointsClient CreateClient() =>
        new(HttpClient, XUnitLogger.CreateLogger<WebhookEndpointsClient>());

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
        WebhookEndpointResponse? created = null;
        try
        {
            created = await client.Create(new CreateWebhookEndpointRequest
            {
                Url = "https://example.com/webhook",
                Events = [WebhookEvent.Delivered, WebhookEvent.Bounced],
                Enabled = true,
            });
            Assert.NotNull(created);
            Assert.NotNull(created.Id);

            var retrieved = await client.Retrieve(created.Id);
            Assert.Equal(created.Id, retrieved.Id);

            var updated = await client.Update(created.Id, new UpdateWebhookEndpointRequest
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
