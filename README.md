[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Hibp.svg)](https://www.nuget.org/packages/ByteDev.Hibp)

# ByteDev.Hibp

Provides simple client to talk to the "Have I been Pwned?" API as hosted by Troy Hunt.

## Installation

ByteDev.Hibp has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Hibp is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Hibp`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Hibp/).

## Code

The repo can be cloned from git bash:

`git clone https://github.com/ByteDev/ByteDev.Hibp`

Integration tests are also provided in the solution.

## Usage

The `HibpClient` class currently has a single (overloaded) method:

- **GetAccountBreachesAsync(string emailAddress)**
- **GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options)**

### Example

```c#
var client = new HibpClient();

var result = await client.GetHasBeenPwnedAsync("johnsmith@gmail.com");

Console.WriteLine($"Is account pwned?: {result.IsPwned}");
Console.WriteLine($"Number of breaches: {result.Breaches.Count()}");
```


## Further Information

See the following for more general information:

- [HIBP Website](https://haveibeenpwned.com/)
- [HIBP API Documentation](https://haveibeenpwned.com/API/v2)
