# Sending

Send transactional and broadcast emails.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**Transactional**](#transactional) | **POST** /send/transactional | Send a transactional email |
| [**TransactionalBatch**](#transactionalbatch) | **POST** /send/transactional/batch | Send transactional emails in batch |
| [**Broadcast**](#broadcast) | **POST** /send/broadcast | Send a broadcast email |
| [**BroadcastMessage**](#broadcastmessage) | **POST** /send/broadcast/message | Send a single broadcast email |

## Transactional

`POST /send/transactional`

Sends a single transactional email such as receipts, confirmations, or notifications.

```csharp Sending_transactional
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendMessageAccepted = await helo.Sending.Transactional(new SendMessageRequest
{
    From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
    To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
    Subject = "Hello from Helo",
    Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
    Text = "This is a test message, delivered with <3 by Helo.",
    Tags = ["welcome", "onboarding"],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```

## TransactionalBatch

`POST /send/transactional/batch`

Sends multiple transactional emails in a single API request for better performance.

```csharp Sending_transactionalBatch
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendMessageBatch = await helo.Sending.TransactionalBatch(new SendMessageBatchRequest
{
    Requests = [
        new SendMessageRequest
        {
            From = new MailAddress { Email = "test@example.com", Name = "test-name" },
            To = [new MailAddress { Email = "test@example.com", Name = "test-name" }],
            Subject = "test-subject",
            Html = "test-html",
            Text = "test-text",
            Tags = ["test-tag"],
        },
    ],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```

## Broadcast

`POST /send/broadcast`

Sends a broadcast email to multiple recipients for marketing or announcement purposes.

```csharp Sending_broadcast
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendBroadcast = await helo.Sending.Broadcast(new SendBroadcastRequest
{
    From = new MailAddress { Email = "test@example.com", Name = "test-name" },
    Template = new SendBroadcastRequestTemplate
    {
        Subject = "test-subject",
        Html = "test-html",
        Text = "test-text",
        InlineStyles = true,
    },
    Tags = ["test-tag"],
    Messages = [
        new SendBroadcastRequestMessage
        {
            To = [new MailAddress { Email = "test@example.com", Name = "test-name" }],
            Tags = ["test-tag"],
        },
    ],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```

## BroadcastMessage

`POST /send/broadcast/message`

Sends a single broadcast email message.

```csharp Sending_broadcastMessage
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendMessageAccepted = await helo.Sending.BroadcastMessage(new SendMessageRequest
{
    From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
    To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
    Subject = "Hello from Helo",
    Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
    Text = "This is a test message, delivered with <3 by Helo.",
    Tags = ["welcome", "onboarding"],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```
