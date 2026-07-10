# Activity

Query message activity and events. The examples assume an `IHeloApiClient helo` —
see the [README](../README.md) for how to register and inject it.

## List events

`GET /activity/events`

```csharp Activity_listEvents
using System;

var events = await helo.Activity.ListEvents(
    startDate: DateTimeOffset.UtcNow.AddDays(-7),
    limit: 50);
```

## List messages

`GET /activity/messages`

```csharp Activity_listMessages
var messages = await helo.Activity.ListMessages(
    recipient: "customer@example.com",
    limit: 50);
```

## Retrieve a message

`GET /activity/messages/{id}`

```csharp Activity_retrieveMessage
var message = await helo.Activity.RetrieveMessage("message-id");
```
