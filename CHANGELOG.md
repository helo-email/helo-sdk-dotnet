# Changelog

## 1.3.0

- Added automatic retries through `RetryHandler`, which `AddHelo` adds to the client it
  registers. A request that fails with a network error, a timeout, or a `408`, `429` or `5xx`
  response is retried up to 2 times with backoff, but only when sending it again can't do
  anything twice. Pass `maxRetries` to `AddHelo` to change how many, or `0` to disable them. If
  you register your own `HttpClient`, add `.AddHttpMessageHandler(() => new RetryHandler())`
  and set its `Timeout` to `Timeout.InfiniteTimeSpan`.
- Each attempt now times out after 30 seconds and is retried; pass `timeout` to `AddHelo` to
  change it. `AddHelo` turns off `HttpClient.Timeout`, which covered all attempts together
  (100 seconds). A final timeout still throws `TaskCanceledException`.
- `SendTransactional`, `SendTransactionalBatch`, `SendBroadcast` and `SendBroadcastMessage` now
  generate an idempotency key when you don't pass `idempotencyKey`, and send the same key on
  every retry, so a send whose response was lost is never sent twice.
- Added `NullFields` to `UpdateTemplateRequest` and `UpdateWebhookRequest`, to clear a field:
  `request.NullFields.Add("Text")`.
- An enum value added to the API after this release now reads as the enum's new `Unknown`
  member instead of failing to deserialize. `WebhookParser.Parse` returns `null` for an event
  type it doesn't know (or a payload without one) instead of throwing, so you can acknowledge
  events you don't handle.

## 1.2.0

- Added sending template management
- Added support for custom tracking domains

## 1.1.0

- Added `id` to the `template` of transactional and broadcast send requests, to send a stored template
  instead of an inline `subject`, `html` and `text`.

## 1.0.2

- Release pipeline updates

## 1.0.1

- Initial release
