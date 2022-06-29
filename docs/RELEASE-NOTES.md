# Release Notes

## 4.0.1 - 29 June 2022

Breaking changes:
- (None)

New features:
- (None)

Bug fixes / internal changes:
- Updated package dependencies:
  - ByteDev.ResourceIdentifier 2.2.0
  - Newtonsoft.Json 13.0.1

## 4.0.0 - 15 July 2021

Breaking changes:
- Fixed `TruncateResponse` option so inline with API default (now true by default).
- Fixed `IncludeUnverified` option so inline with API default (now true by default).
- Renamed namespace `ByteDev.Hibp.Request` to `ByteDev.Hibp.Contract.Request`.
- Renamed namespace `ByteDev.Hibp.Response` to `ByteDev.Hibp.Contract.Response`.
- Removed API key string param from `HibpClient` constructor (now provided through `HibpClientOptions` param).

New features:
- `HibpClient` constructor takes optional `HibpClientOptions` which:
  - Allows consumer to set optional API key.
  - Allows consumer to instruct client to automatically handle `429 Too many requests` style responses from the API if desired.

Bug fixes / internal changes:
- Removed package dependency `ByteDev.Common`.
- Added package dependency `ByteDev.ResourceIdentifier`.

## 3.0.0 - 24 July 2020

Breaking changes:
- Changes for v3 of the API.

New features:
- (None)

Bug fixes / internal changes:
- (None)
