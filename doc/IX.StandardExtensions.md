# IX.StandardExtensions

## Introduction

IX.StandardExtensions is a .NET library that seeks to implement various extensions in order to standardize access to some functionality.

The motivation behind this library was introduced in .NET 4 where the [`List<T>`](https://msdn.microsoft.com/en-us/library/6sh2ey19.aspx) class introduced the ForEach method. Arrays have their own static ForEach method (which turns out to be extremely slow compared to the foreach cycle), whereas IEnumerable do not have a ForEach at all.

Then came the Task Parallel Library which introduced Parallel.ForEach, which uses IEnumerable as a parameter.

So we came up with this library that exposes extension methods which give the same ForEach approach to enumerable and to array.

Furthermore, the ICloneable interface is recommended by MSDN to not be used at all, leaving us with no baked-in way to define an object which can have shallow clones or an object which can have deep clones. We had to make our own.

This is, in a nutshell, how we came up with this library.

## How to get

There are multiple packages in this project:

| Name | NuGet |
|:----:|:-----:|
| Standard Extensions | [![IX.StandardExtensions NuGet](https://img.shields.io/nuget/v/IX.StandardExtensions.svg)](https://www.nuget.org/packages/IX.StandardExtensions/) |
| Component Model | [![IX.StandardExtensions Component Model NuGet](https://img.shields.io/nuget/v/IX.StandardExtensions.ComponentModel.svg)](https://www.nuget.org/packages/IX.StandardExtensions.ComponentModel/) |
| Test Utils | [![IX.StandardExtensions Test Utils NuGet](https://img.shields.io/nuget/v/IX.StandardExtensions.TestUtils.svg)](https://www.nuget.org/packages/IX.StandardExtensions.TestUtils/) |
| Threading | [![IX.StandardExtensions Threading NuGet](https://img.shields.io/nuget/v/IX.StandardExtensions.Threading.svg)](https://www.nuget.org/packages/IX.StandardExtensions.Threading/) |

## Usage

The library exposes a lot of methods in an attempt to standardize the approach to code, so we'll just take a few examples.

### ForEach on an IEnumerable.

Given we have:

```csharp
IEnumerable<someClass> someCollection;
```

We would call a method for each item of the collection like this:

```csharp
foreach (var item in someCollection)
{
    someMethod(item);
}
```

With the extension method, we could call it like this:

```csharp
someCollection.ForEach(someMethod);
```

The same would hold true for an array.

Although, to be fair, if you're going to have a benchmark of:

```csharp
i++;
```

...then the foreach cycle will be faster, since you will not have an extra method invocation.

As an extra bonus, you can run them using task parallel library (.NET Standard 1.1 and above only).

```csharp
someCollection.ParallelForEach(someMethod);
```

### Sequence Equals

The next example comes from the need to compare data. Comparison on arrays or enumerables (or between an array or an IEnumerable) has always been slightly burdensome. We have a helper for that:

```csharp
if (someCollection.SequenceEquals(someOtherCollection))
{
    // Do something
}
```

### Component model

The component model project features some extensions used in the .NET models for UI and for use cases when change notifications, validation errors and so on are necessary.

For example, give that we have:

```csharp
public class SomeUIViewModel : ViewModelBase
{
...
}
```

...one can, by virtue of having implemented ViewModelBase, use:

```csharp
public string SomeUIProperty
{
    get => someValue;

    set
    {
        if (someValue != value)
        {
            someValue = value;
            RaisePropertyChanged(nameof(SomeUIProperty));
        }
    }
}
```

### Synchronization lockers

The threading project features classes that help in synchronizing mutithreaded operations in the most efficient ways possible.

Suppose that one has:

```csharp
private ReaderWriterLockSlim rwls;
```

Then, within code, one can:

```csharp
using (new ReadOnlySynchronizationLocker(rwls))
{
    // Do some operation here that requires thread synchronization that only reads
}
```

... or:

```csharp
using (new WriteOnlySynchronizationLocker(rwls))
{
    // Do some operation here that requires thread synchronization and locking of data for writing
}
```

### Atomic enumerator

The _AtomicEnumerator_ class is an enumerator based on another enumerator, which synchronizes data fetching (e.g. the _Next_ and _Reset_ methods), and which will fail just like a regular enumerator if the collection is changed.