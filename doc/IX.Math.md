# IX.Math

## Introduction

IX.Math is a .NET library that reads mathematical formulas as text and creates delegates and delegate placeholders for solving them at runtime.

The library is capable of interpreting any mathematical expression that makes sense from a logical perspective, complete with a few mathematical functions. It supports integer numbers (as [long](https://msdn.microsoft.com/en-us/library/system.int64.aspx) in standard numeric formats), floating-point numbers (as [double](https://msdn.microsoft.com/en-us/library/system.double.aspx), strings (as [string](https://msdn.microsoft.com/en-us/library/system.string.aspx)) and boolean (as [bool](https://msdn.microsoft.com/en-us/library/system.boolean.aspx)) values, and can compute the most common mathematic operations, as well as certain mathematics functions.

## How to get

This project is primarily available through NuGet.

The current version can be accessed by using NuGet commands:

```powershell
Install-Package IX.Math
```

Releases: [![IX.Math NuGet](https://img.shields.io/nuget/v/IX.Math.svg)](https://www.nuget.org/packages/IX.Math/)

## Usage

### Standard function set

This library is based on two implementations of the interface IExpressionParsingService:
- ExpressionParsingService - a parsing service that just spits out delegates on demand
- CachedExpressionParsingService - a parsing service that also caches its expressions

There is one method that is implemented in both: Interpret. This method takes in one string (and an optional [CancellationToken](https://msdn.microsoft.com/en-us/library/system.threading.cancellationtoken.aspx)) and generates a ComputedExpression object which can afterwards be used to calculate the result of the expression.

A computed expression can be parameterless (for instance 5+6) or parametered (for instance 2+x).

The ComputedExpression features two overloads of the Compute method:
- The first overload takes parameters as objects
- The second overload takes an IDataFinder, which is an interface that the library user is supposed to implement and that will fetch items by name

Each of these methods results in a possible result, or, if the expression doesn't make sense from a mathematics perspective, or if the parameters fed to it are of the wrong type, will return the original expression as a string.

### Extensibility

In order to extend the set of functions that the IX.Math library supports, a new class should be created for each function that can be invoked. For now, only unary, binary and nonary (no parameters) functions can be created, but a generalized implementation will be created soon.

Each such class should inherit from BinaryFunctionNodeBase, UnaryFunctionNodeBase and NonaryFunctionNodeBase, must be decorated with the CallableMathematicsFunctionAttribute, and their containing assembly must be registered with the IExpressionParsingService's RegisterFunctionsAssembly method.