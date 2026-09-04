# Webhooks

Create and manage webhooks for event notifications.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /webhooks | List all webhooks |
| [**Create**](#create) | **POST** /webhooks | Create a webhook |
| [**Retrieve**](#retrieve) | **GET** /webhooks/{id} | Retrieve a webhook |
| [**Update**](#update) | **PATCH** /webhooks/{id} | Update a webhook |
| [**Delete**](#delete) | **DELETE** /webhooks/{id} | Delete a webhook |
| [**RegenerateSigningKey**](#regeneratesigningkey) | **POST** /webhooks/{id}/regenerate-signing-key | Regenerate webhook signing key |

## List

`GET /webhooks`

Retrieves all webhooks configured for the account.

```csharp Webhooks_list
using HeloEmail.Sdk.Webhooks;

var paginationResultOfWebhook = await helo.Webhooks.List(limit: 10, offset: 10);
```

## Create

`POST /webhooks`

Registers a new webhook to receive event notifications.

```csharp Webhooks_create
using HeloEmail.Sdk.Webhooks;

var webhook = await helo.Webhooks.Create(new CreateWebhookRequest
{
    Url = "test-url",
    Events = [WebhookEvent.MessageAccepted],
    ChannelId = "550e8400-e29b-41d4-a716-446655440000",
    Enabled = true,
});
```

## Retrieve

`GET /webhooks/{id}`

Fetches the details and configuration of a specific webhook.

```csharp Webhooks_retrieve
using HeloEmail.Sdk.Webhooks;

var webhook = await helo.Webhooks.Retrieve("550e8400-e29b-41d4-a716-446655440000");
```

## Update

`PATCH /webhooks/{id}`

Modifies an existing webhook by ID.

```csharp Webhooks_update
using HeloEmail.Sdk.Webhooks;

var webhook = await helo.Webhooks.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateWebhookRequest
{
    Url = "test-url",
    Events = [WebhookEvent.MessageAccepted],
    ChannelId = "550e8400-e29b-41d4-a716-446655440000",
    Enabled = true,
});
```

## Delete

`DELETE /webhooks/{id}`

Permanently removes a webhook.

```csharp Webhooks_delete
using HeloEmail.Sdk.Webhooks;

await helo.Webhooks.Delete("550e8400-e29b-41d4-a716-446655440000");
```

## RegenerateSigningKey

`POST /webhooks/{id}/regenerate-signing-key`

Regenerate the signing key used for the webhook signature. This operation replaces the old key.

```csharp Webhooks_regenerateSigningKey
using HeloEmail.Sdk.Webhooks;

var webhook = await helo.Webhooks.RegenerateSigningKey("550e8400-e29b-41d4-a716-446655440000");
```
