# Channels

Create and manage communication channels for organizing messages.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /channels | List all channels |
| [**Create**](#create) | **POST** /channels | Create a channel |
| [**Retrieve**](#retrieve) | **GET** /channels/{id} | Retrieve a channel |
| [**Update**](#update) | **PATCH** /channels/{id} | Update a channel |
| [**Delete**](#delete) | **DELETE** /channels/{id} | Delete a channel |

## List

`GET /channels`

Retrieves a list of all channels accessible to the current user.

```csharp Channels_list
using HeloEmail.Sdk;
using HeloEmail.Sdk.Channels;

var paginationResultOfChannelBasic = await helo.Channels.List(limit: 10, offset: 10);
```

## Create

`POST /channels`

Creates a new communication channel for organizing and routing messages.

```csharp Channels_create
using HeloEmail.Sdk;
using HeloEmail.Sdk.Channels;

var channelDetails = await helo.Channels.Create(new CreateChannelRequest
{
    Name = "test-name",
    DeliveryType = DeliveryType.Live,
});
```

## Retrieve

`GET /channels/{id}`

Fetches the details and configuration of a specific channel.

```csharp Channels_retrieve
using HeloEmail.Sdk.Channels;

var channelDetails = await helo.Channels.Retrieve("550e8400-e29b-41d4-a716-446655440000");
```

## Update

`PATCH /channels/{id}`

Modifies an existing channel by ID.

```csharp Channels_update
using HeloEmail.Sdk;
using HeloEmail.Sdk.Channels;

var channelDetails = await helo.Channels.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateChannelRequest
{
    Name = "test-name",
    DeliveryType = DeliveryType.Live,
});
```

## Delete

`DELETE /channels/{id}`

Permanently removes a channel and all associated data.

```csharp Channels_delete
using HeloEmail.Sdk.Channels;

await helo.Channels.Delete("550e8400-e29b-41d4-a716-446655440000");
```
