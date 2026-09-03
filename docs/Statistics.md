# Statistics

Access activity statistics.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**RetrieveHourly**](#retrievehourly) | **GET** /statistics/hourly | Retrieve hourly statistics |
| [**RetrieveDaily**](#retrievedaily) | **GET** /statistics/daily | Retrieve daily statistics |
| [**RetrieveTotals**](#retrievetotals) | **GET** /statistics/totals | Retrieve all time statistics |

## RetrieveHourly

`GET /statistics/hourly`

Fetches hourly aggregated statistics.

```csharp Statistics_retrieveHourly
using System;
using HeloEmail.Sdk.Statistics;

var statisticsHourly = await helo.Statistics.RetrieveHourly(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, channelId: "550e8400-e29b-41d4-a716-446655440000");
```

## RetrieveDaily

`GET /statistics/daily`

Fetches daily aggregated statistics.

```csharp Statistics_retrieveDaily
using System;
using HeloEmail.Sdk.Statistics;

var statisticsDaily = await helo.Statistics.RetrieveDaily(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "America/New_York", channelId: "550e8400-e29b-41d4-a716-446655440000");
```

## RetrieveTotals

`GET /statistics/totals`

Fetches cumulative statistics.

```csharp Statistics_retrieveTotals
using System;
using HeloEmail.Sdk.Statistics;

var statisticsTotals = await helo.Statistics.RetrieveTotals(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, channelId: "550e8400-e29b-41d4-a716-446655440000");
```
