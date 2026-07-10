# Sending

Send transactional and broadcast emails through the Helo API.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

## Send a transactional email

`POST /send/transactional`

Sends a single transactional email. `channelId` selects the sending channel and
`idempotencyKey` (both optional) lets you safely retry a send without duplicating it.

```csharp Sending_transactional
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var response = await helo.Sending.Transactional(new SendMessageRequest
{
    From = new MailAddress { Email = "sender@example.com", Name = "Sender" },
    To = [new MailAddress { Email = "recipient@example.com", Name = "Recipient" }],
    Subject = "Hello from Helo",
    Html = "<p>Hello!</p>",
    Text = "Hello!",
    Tags = ["welcome", "onboarding"],
}, channelId: "your-channel-id");
```

## Send transactional emails in batch

`POST /send/transactional/batch`

Sends up to several transactional emails in a single request.

```csharp Sending_transactionalBatch
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var response = await helo.Sending.TransactionalBatch(new SendMessageBatchRequest
{
    Requests =
    [
        new SendMessageRequest
        {
            From = new MailAddress { Email = "sender@example.com", Name = "Sender" },
            To = [new MailAddress { Email = "first@example.com" }],
            Subject = "Hello from Helo",
            Html = "<p>Hello!</p>",
        },
        new SendMessageRequest
        {
            From = new MailAddress { Email = "sender@example.com", Name = "Sender" },
            To = [new MailAddress { Email = "second@example.com" }],
            Subject = "Hello from Helo",
            Html = "<p>Hello!</p>",
        },
    ],
}, channelId: "your-channel-id");
```

## Send a broadcast email

`POST /send/broadcast`

Sends a broadcast to multiple recipients using a shared template.

```csharp Sending_broadcast
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var response = await helo.Sending.Broadcast(new SendBroadcastRequest
{
    From = new MailAddress { Email = "sender@example.com", Name = "Sender" },
    Template = new MessageTemplate
    {
        Subject = "Product update",
        Html = "<p>Here's what's new this month…</p>",
    },
    Messages =
    [
        new BroadcastMessage { To = [new MailAddress { Email = "first@example.com" }] },
        new BroadcastMessage { To = [new MailAddress { Email = "second@example.com" }] },
    ],
}, channelId: "your-channel-id");
```

## Send a single broadcast email

`POST /send/broadcast/message`

Sends a single message as part of a broadcast.

```csharp Sending_broadcastMessage
using HeloEmail.Sdk;
using HeloEmail.Sdk.Sending;

var response = await helo.Sending.BroadcastMessage(new SendMessageRequest
{
    From = new MailAddress { Email = "sender@example.com", Name = "Sender" },
    To = [new MailAddress { Email = "recipient@example.com" }],
    Subject = "Hello from Helo",
    Html = "<p>Hello!</p>",
}, channelId: "your-channel-id");
```
