# Channels

Manage sending channels. The examples assume an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

## Create a channel

`POST /channels`

```csharp Channels_create
using HeloEmail.Sdk;
using HeloEmail.Sdk.Channels;

var channel = await helo.Channels.Create(new CreateChannelRequest
{
    Name = "Transactional",
    DeliveryType = DeliveryType.Live,
    Tracking = new ChannelTracking { Links = true, Opens = true },
});
```

## List channels

`GET /channels`

```csharp Channels_list
using HeloEmail.Sdk;

var channels = await helo.Channels.List(limit: 20, deliveryType: DeliveryType.Live);
```

## Retrieve a channel

`GET /channels/{id}`

```csharp Channels_retrieve
var channel = await helo.Channels.Retrieve("channel-id");
```

## Update a channel

`PATCH /channels/{id}`

```csharp Channels_update
using HeloEmail.Sdk.Channels;

var channel = await helo.Channels.Update("channel-id", new UpdateChannelRequest
{
    Name = "Marketing",
});
```

## Delete a channel

`DELETE /channels/{id}`

```csharp Channels_delete
await helo.Channels.Delete("channel-id");
```
