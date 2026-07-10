# Statistics

Retrieve delivery statistics. The examples assume an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

## Hourly statistics

`GET /activity/statistics/hourly`

```csharp Statistics_retrieveHourly
using System;

var stats = await helo.Statistics.RetrieveHourly(
    from: DateTimeOffset.UtcNow.AddDays(-1),
    to: DateTimeOffset.UtcNow);
```

## Daily statistics

`GET /activity/statistics/daily`

```csharp Statistics_retrieveDaily
using System;

var stats = await helo.Statistics.RetrieveDaily(
    from: DateTimeOffset.UtcNow.AddDays(-30),
    to: DateTimeOffset.UtcNow,
    timezone: "America/New_York");
```

## Total statistics

`GET /activity/statistics/totals`

```csharp Statistics_retrieveTotals
using System;

var stats = await helo.Statistics.RetrieveTotals(
    from: DateTimeOffset.UtcNow.AddDays(-30),
    to: DateTimeOffset.UtcNow);
```
