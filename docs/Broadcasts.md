# Broadcasts

Manage and track broadcast email campaigns.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /broadcasts | List broadcasts |
| [**Retrieve**](#retrieve) | **GET** /broadcasts/{id} | Retrieve a broadcast |
| [**ListFailures**](#listfailures) | **GET** /broadcasts/{id}/failures | List failed broadcast messages |
| [**ListSuppressions**](#listsuppressions) | **GET** /broadcasts/{id}/suppressions | List broadcast suppressed recipients |

## List

`GET /broadcasts`

Retrieves a paginated list of sent broadcasts with summary statistics.

```csharp Broadcasts_list
using HeloEmail.Sdk.Broadcasts;

var paginatedResponseOfBroadcast = await helo.Broadcasts.List("550e8400-e29b-41d4-a716-446655440000", status: BroadcastStatus.Accepted, subject: "test-subject");
```

## Retrieve

`GET /broadcasts/{id}`

Fetches details and statistics for a specific broadcast.

```csharp Broadcasts_retrieve
using HeloEmail.Sdk.Broadcasts;

var broadcastDetails = await helo.Broadcasts.Retrieve("550e8400-e29b-41d4-a716-446655440000");
```

## ListFailures

`GET /broadcasts/{id}/failures`

Returns messages that could not be delivered due to permanent errors (e.g. invalid addresses, domain issues). Transient errors that were retried successfully do not appear here.

```csharp Broadcasts_listFailures
using HeloEmail.Sdk.Broadcasts;

var paginatedResponseOfBroadcastFailure = await helo.Broadcasts.ListFailures("550e8400-e29b-41d4-a716-446655440000", limit: 10, offset: 10);
```

## ListSuppressions

`GET /broadcasts/{id}/suppressions`

Returns recipients that were skipped because they appear on a suppression list (e.g. previous bounces or unsubscribes).

```csharp Broadcasts_listSuppressions
using HeloEmail.Sdk.Broadcasts;

var paginatedResponseOfBroadcastSuppression = await helo.Broadcasts.ListSuppressions("550e8400-e29b-41d4-a716-446655440000", limit: 10, offset: 10);
```
