# IX.Retry

## Introduction

An operational retry policy framework for any operations that might fail due to recoverable circumstances.

Each failure is handled based on a specific policy which can be customized (note: future release).

## How to get

This project is primarily available through NuGet.

The current version can be accessed by using NuGet commands:

```powershell
Install-Package IX.Retry
```

Releases: [![IX.Retry NuGet](https://img.shields.io/nuget/v/IX.Retry.svg)](https://www.nuget.org/packages/IX.Retry/)

## Usage

### Retrying

Simple.

Original code:
```c#
int x = GetNumberOfHeadbangsPerMinuteFromRemoteServer(param1, param2, param3);
```
With retry:
```c#
int x = With.Retry(GetNumberOfHeadbangsPerMinuteFromRemoteServer, param1, param2, param3, Policy.TimeBased<SomeTransportException>(TimeSpan.FromSeconds(10)));
```

Asynchronous operations are also supported.

Original code:
```c#
int x = await GetNumberOfHeadbangsPerMinuteFromRemoteServer(param1, param2, param3);
```
With retry:
```c#
int x = await With.RetryAsync(GetNumberOfHeadbangsPerMinuteFromRemoteServer, param1, param2, param3, Policy.TimeBased<SomeTransportException>(TimeSpan.FromSeconds(10)));
```

Extension methods are also available for delegate parameters.

Original code:
```c#
public int SomeMethod(Func<Type1, Type2, Type3, int> getNumberOfHeadbangsPerMinuteFromRemoteServer, Type1 param1, Type2 param2, Type3 param3)
{
    return getNumberOfHeadbangsPerMinuteFromRemoteServer(param1, param2, param3);
}
```
With retry:
```c#
public int SomeMethod(Func<Type1, Type2, Type3, int> getNumberOfHeadbangsPerMinuteFromRemoteServer, Type1 param1, Type2 param2, Type3 param3)
{
    return getNumberOfHeadbangsPerMinuteFromRemoteServer.WithRetry(param1, param2, param3, Policy.TimeBased<SomeTransportException>(TimeSpan.FromSeconds(10)));
}
```

Asynchronous support as well.

Original code:
```c#
public async Task<int> SomeMethod(Func<Type1, Type2, Type3, Task<int>> getNumberOfHeadbangsPerMinuteFromRemoteServer, Type1 param1, Type2 param2, Type3 param3)
{
    return await getNumberOfHeadbangsPerMinuteFromRemoteServer(param1, param2, param3);
}
```
With retry:
```c#
public async Task<int> SomeMethod(Func<Type1, Type2, Type3, Task<int>> getNumberOfHeadbangsPerMinuteFromRemoteServer, Type1 param1, Type2 param2, Type3 param3)
{
    return await getNumberOfHeadbangsPerMinuteFromRemoteServer.WithRetryAsync(param1, param2, param3, Policy.TimeBased<SomeTransportException>(TimeSpan.FromSeconds(10)));
}
```

Or you can completely forget about explicitly specifying the policy.

One might ask "What if I want to asynchronously wait for a synchronous method?" That is possible as well, as you will notice, with the same syntax.
```c#
public async Task<int> SomeMethod(Func<Type1, Type2, Type3, int> getNumberOfHeadbangsPerMinuteFromRemoteServer, Type1 param1, Type2 param2, Type3 param3)
{
    return await getNumberOfHeadbangsPerMinuteFromRemoteServer.WithRetryAsync(param1, param2, param3, Policy.TimeBased<SomeTransportException>(TimeSpan.FromSeconds(10)));
}
```

### Synchronous vs Asynchronous

Please be advised that, due to threading and compatibility issues, and the desire to offer only the best practices available, the synchronous retry calls will retry for as many times as specified, but they will not delay.

If delays are needed, due to the exponential nature of retry policies, the thread can be blocked for a long time. It is therefore advisable to use the asynchronous methods if delay is necessary.