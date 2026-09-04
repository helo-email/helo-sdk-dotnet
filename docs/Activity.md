# Activity

Track and retrieve message activity, including messages, delivery and engagement events.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**ListEvents**](#listevents) | **GET** /activity/events | List activity events |
| [**ListMessages**](#listmessages) | **GET** /activity/messages | List messages |
| [**RetrieveMessage**](#retrievemessage) | **GET** /activity/messages/{id} | Retrieve message details |

## ListEvents

`GET /activity/events`

Retrieves activity events for messages, including delivery status, opens, clicks, bounces, unsubscribes and complaints.

```csharp Activity_listEvents
using HeloEmail.Sdk;
using HeloEmail.Sdk.Activity;

var paginatedEvents = await helo.Activity.ListEvents(channelId: "550e8400-e29b-41d4-a716-446655440000", messageId: "550e8400-e29b-41d4-a716-446655440000");
```

## ListMessages

`GET /activity/messages`

Retrieves a paginated list of sent messages with basic tracking information.

```csharp Activity_listMessages
using HeloEmail.Sdk;
using HeloEmail.Sdk.Activity;

var paginatedMessages = await helo.Activity.ListMessages(channelId: "550e8400-e29b-41d4-a716-446655440000", after: 10);
```

## RetrieveMessage

`GET /activity/messages/{id}`

Fetches detailed tracking information for a specific message, including all associated events.

```csharp Activity_retrieveMessage
using HeloEmail.Sdk.Activity;

var messageDetails = await helo.Activity.RetrieveMessage("550e8400-e29b-41d4-a716-446655440000");
```
