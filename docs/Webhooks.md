# Webhooks

Manage webhook endpoints. In the .NET SDK these live under `helo.WebhookEndpoints`.
The examples assume an `IHeloApiClient helo` — see the [README](../README.md) for how
to register and inject it.

## Create a webhook endpoint

`POST /webhooks`

```csharp Webhooks_create
using HeloEmail.Sdk.WebhookEndpoints;

var webhook = await helo.WebhookEndpoints.Create(new CreateWebhookEndpointRequest
{
    Url = "https://example.com/webhooks/helo",
    Events = [WebhookEvent.Delivered, WebhookEvent.Bounced],
    ChannelId = "channel-id",
    Enabled = true,
});
```

## List webhook endpoints

`GET /webhooks`

```csharp Webhooks_list
var webhooks = await helo.WebhookEndpoints.List(limit: 20);
```

## Retrieve a webhook endpoint

`GET /webhooks/{id}`

```csharp Webhooks_retrieve
var webhook = await helo.WebhookEndpoints.Retrieve("webhook-id");
```

## Update a webhook endpoint

`PATCH /webhooks/{id}`

```csharp Webhooks_update
using HeloEmail.Sdk.WebhookEndpoints;

var webhook = await helo.WebhookEndpoints.Update("webhook-id", new UpdateWebhookEndpointRequest
{
    Enabled = false,
});
```

## Delete a webhook endpoint

`DELETE /webhooks/{id}`

```csharp Webhooks_delete
await helo.WebhookEndpoints.Delete("webhook-id");
```

## Regenerate a webhook signing key

`POST /webhooks/{id}/regenerate-signing-key`

```csharp Webhooks_regenerateSigningKey
var webhook = await helo.WebhookEndpoints.RegenerateSigningKey("webhook-id");
```
