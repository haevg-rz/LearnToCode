# Push Example

## Motivation

We want to send a Push-Notification from our console application to our smartphone.

![Screenshot](Assets/Screenshot.jpg)

This is a very trivial example for making http requests against an API.

## Background

Your smartphone is getting a lot of push notifications for a lot of different apps. But the apps are not pulling (check every second for incoming messages), there is a central server to which your smartphone is all the time connected. This permanent connection will receive all notifications for all apps.

"Push technology, or server push, is a style of Internet-based communication where the request for a given transaction is initiated by the publisher or central server. It is contrasted with pull/get, where the request for the transmission of information is initiated by the receiver or client."
[https://en.wikipedia.org/wiki/Push_technology](https://en.wikipedia.org/wiki/Push_technology)

## Prearrangement

1. Sign in or create an account at [https://pushover.net/](https://pushover.net/).
2. Copy User Key e.g. kwvuyrsn7v7ar25gnfapjd85pj28wh
3. Create New Application/API Token [https://pushover.net/apps/build](https://pushover.net/apps/build)
4. Copy API Token e.g. r45xxu4vzks8ie9zmvj625hcr45xxu
5. Install .NET Core 3.1 SDK [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
6. Install VS Code

## Getting started

### Read the documentation

After reading the documentation [Pushing Messages](https://pushover.net/api#messages) you know a lot how to use this api.

1. You must make a `POST` request
2. The url is `https://api.pushover.net/1/messages.json`
3. `HTTPS` is required for all API calls
4. parameter `token`, `user` and `message` are required
5. parameter must be encoded as `application/x-www-form-urlencoded`

### Creating a console app

```cmd
mkdir PushSample
cd PushSample
dotnet new console
dotnet new sln
dotnet sln add PushSample.csproj
```

### Implementation

#### Creating the parameter for the api

> We are assigning the required parameters in the format that `FormUrlEncodedContent` expected.

```csharp
var nameValueCollection = new List<KeyValuePair<String, String>>
{
    KeyValuePair.Create("token", "add token here"),
    KeyValuePair.Create("user", "add token here"),
    KeyValuePair.Create("title", "My first push message"),
    KeyValuePair.Create("message","Hello!"),
};
```

#### Creating a HTTP Client

> Best practice is to reuse the same `HttpClient` instance. But be aware, no thread safety here.

```csharp
private static readonly HttpClient Client = new HttpClient();
```

#### Make a request

> We are making a `POST` request against the `url` with our parameter as `UrlEncoded`.

```csharp
const string url = "https://api.pushover.net/1/messages.json";
var response = Client.PostAsync(url, new FormUrlEncodedContent(nameValueCollection)).GetAwaiter().GetResult();
```

#### check response

> `response` hat a nice property `HttpResponseMessage.IsSuccessStatusCode` which gets a value that indicates whether the HTTP response was successful.

```csharp
if (response.IsSuccessStatusCode)
{
    Console.Out.WriteLine($"Push message was send!");
}
else
{
    Console.Out.WriteLine($"Push message wasn't send!");
}
```

#### Get the api rate limit

> To see how many requests are allowed, you can evaluate the User Header `X-Limit-App-Remaining`.

```csharp
response.Headers.FirstOrDefault(h => h.Key == "X-Limit-App-Remaining").Value?.FirstOrDefault();
```

#### Do some formatting

> You can make the request a lot prettier with some html styling.

```csharp
KeyValuePair.Create("html", "1"),
KeyValuePair.Create("message", $"Send from <b>{Environment.UserName}</b> on <b>{Environment.MachineName}</b> " +
                                $"with {Environment.ProcessorCount} CPUs at {DateTime.Now} " +
                                $"for more information go to <a href=\"https://github.com/haevg-rz/LearnToCode/tree/master/Examples\">github.com</a>"),
```

### Publish this application

> It would be very nice to create one executable file that contains every that is needed. Since .NET Core 3 this is possible with
> `/p:PublishSingleFile=true` and `/p:PublishTrimmed=true` to reduce the size. Don't use trim if you use reflection!

#### For windows

```c
dotnet publish -c Release -r win-x86 /p:PublishSingleFile=true /p:PublishTrimmed=true
```

#### For linux

```c
dotnet publish -c Release -r linux-musl-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```
