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

- **GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null)**
- **GetBreachedSitesAsync(string domain = null)**
- **GetBreachSiteByNameAsync(string breachName)**
- **GetDataClassesAsync()**
- **GetAccountPastesAsync(string emailAddress)**

### Example

```csharp
IHibpClient client = new HibpClient(new HttpClient());

var result = await client.GetAccountBreachesAsync("johnsmith@gmail.com");

Console.WriteLine($"Number of breaches: {result.Count()}");
```

## Further Information

See the following for more general information:

- [HIBP Website](https://haveibeenpwned.com/)
- [HIBP API Documentation](https://haveibeenpwned.com/API/v3)
- [HIBP API Keys](https://haveibeenpwned.com/API/Key)
