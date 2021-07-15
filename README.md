[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Hibp?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Hibp/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Hibp.svg)](https://www.nuget.org/packages/ByteDev.Hibp)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Hibp/blob/master/LICENSE)

# ByteDev.Hibp

Provides simple client to talk to the "Have I been Pwned?" API as hosted by Troy Hunt.

## Installation

ByteDev.Hibp has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Hibp is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Hibp`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Hibp/).

## Usage

The `HibpClient` class currently has a number of public methods:

- `GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null)`
- `GetBreachedSitesAsync(string domain = null)`
- `GetBreachSiteByNameAsync(string breachName)`
- `GetDataClassesAsync()`
- `GetAccountPastesAsync(string emailAddress)`

**Note:** Methods `GetAccountBreachesAsync` and `GetAccountPastesAsync` require a valid API key be provided (via the `HibpClient` constructor).

### Example

```csharp
var options = new HibpClientOptions
{
    ApiKey = "someApiKey",
    RetryOnRateLimitExceeded = true
};

IHibpClient client = new HibpClient(new HttpClient(), options);

var result = await client.GetAccountBreachesAsync("johnsmith@gmail.com");

Console.WriteLine($"Number of breaches: {result.Count()}");
```

## Glossary

**Account** = Represented by an email address.

**Breach** = An instance of a system having been compromised by an attacker and the data disclosed.

**Data Class** = An attribute of a record compromised in a breach.
For example, many breaches expose data classes such as "Email addresses" and "Passwords".
A complete list of all data classes can be obtained by calling the `GetDataClassesAsync` method.
		
**Pastes** = Text pasted onto a website whereupon it receives its own unique URL so that it can then be shared with others who may want to view the paste.

## Further Information

See the following for more general information:

- [HIBP Website](https://haveibeenpwned.com/)
- [HIBP API Documentation](https://haveibeenpwned.com/API/v3)
- [HIBP API Keys](https://haveibeenpwned.com/API/Key)
