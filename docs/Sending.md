# Sending

Send transactional and broadcast emails.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**SendTransactional**](#sendtransactional) | **POST** /send/transactional | Send a transactional email |
| [**SendTransactionalBatch**](#sendtransactionalbatch) | **POST** /send/transactional/batch | Send transactional emails in batch |
| [**SendBroadcast**](#sendbroadcast) | **POST** /send/broadcast |  |
| [**SendBroadcastMessage**](#sendbroadcastmessage) | **POST** /send/broadcast/message | Send a single broadcast email |

## SendTransactional

`POST /send/transactional`

Sends a single transactional email such as receipts, confirmations, or notifications.

```csharp Sending_sendTransactional
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendMessageAccepted = await helo.Sending.SendTransactional(new SendMessageRequest
{
    From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
    To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
    Subject = "Hello from Helo",
    Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
    Text = "This is a test message, delivered with <3 by Helo.",
    Tags = ["welcome", "onboarding"],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```

## SendTransactionalBatch

`POST /send/transactional/batch`

Sends multiple transactional emails in a single API request for better performance.

```csharp Sending_sendTransactionalBatch
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendMessageBatch = await helo.Sending.SendTransactionalBatch(new SendMessageBatchRequest
{
    Requests = [
        new SendMessageRequest
        {
            From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
            To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
            Subject = "Hello from Helo",
            Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
            Text = "This is a test message, delivered with <3 by Helo.",
            Tags = ["welcome", "onboarding"],
        },
    ],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```

## SendBroadcast

`POST /send/broadcast`

```csharp Sending_sendBroadcast
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendBroadcast = await helo.Sending.SendBroadcast(new SendBroadcastRequest
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

## SendBroadcastMessage

`POST /send/broadcast/message`

Sends a single broadcast email message.

```csharp Sending_sendBroadcastMessage
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var sendMessageAccepted = await helo.Sending.SendBroadcastMessage(new SendMessageRequest
{
    From = new MailAddress { Email = "from@yourdomain.com", Name = "From name" },
    To = [new MailAddress { Email = "to@example.com", Name = "To name" }],
    Subject = "Hello from Helo",
    Html = "<html><body><h1>Hi there, new friend.</h1><p>This is a test message, delivered with <3 by Helo. </p></body></html>",
    Text = "This is a test message, delivered with <3 by Helo.",
    Tags = ["welcome", "onboarding"],
}, channelId: "550e8400-e29b-41d4-a716-446655440000", idempotencyKey: "test-idempotencyKey");
```
