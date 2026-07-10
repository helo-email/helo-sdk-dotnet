# Broadcasts

Query broadcast campaigns. The examples assume an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

## List broadcasts

`GET /broadcasts`

```csharp Broadcasts_list
using HeloEmail.Sdk.Broadcasts;

var broadcasts = await helo.Broadcasts.List("channel-id", status: BroadcastStatus.Completed);
```

## Retrieve a broadcast

`GET /broadcasts/{id}`

```csharp Broadcasts_retrieve
var broadcast = await helo.Broadcasts.Retrieve("broadcast-id");
```

## List a broadcast's failures

`GET /broadcasts/{id}/failures`

```csharp Broadcasts_listFailures
var failures = await helo.Broadcasts.ListFailures("broadcast-id");
```

## List a broadcast's suppressions

`GET /broadcasts/{id}/suppressions`

```csharp Broadcasts_listSuppressions
var suppressions = await helo.Broadcasts.ListSuppressions("broadcast-id");
```
