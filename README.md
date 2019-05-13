# IX Framework

## Introduction

The IX Framework is a collections of assemblies delivered as .NET Standard NuGet packages.

Their aim is to provide extensions, abstractions and functionality, in order to make working with various components of the .NET Framework in
a better way across devices, across platforms and across versions.

All libraries are compatible with .NET Framework 4.5 or above, as well as with the .NET Standard 2.0, and all libraries have at least one other .NET
Standard below 2.0 as a minimal supported version.

## License and copyright

Until further notice, all versions of this repository will be distributed under the MIT License, which is available [here](LICENSE.md).

## Components and how to get them

| Library | Description | Location |
|:-------:|:-----------:|:--------:|
| IX.StandardExtensions | Extensions aiming at standardizing various operations in the .NET Framework. | [Project page](doc/IX.StandardExtensions.md) |
| IX.Abstractions | A set of libraries for abstracting away things that are not abstracted by default (e.g. file storage) | - |
| IX.Math | A library that interprets mathematical expressions into executable .NET code | [Project page](https://github.com/adimosh/IX.Math) (separate project) |
| IX.Retry | A library that allows error recovery by retrying a method call according to set rules | [Project page](doc/IX.Retry.md) |
| IX.Guaranteed | A library dealing with operations that are guaranteed to have completed when successful | - |
| IX.Undoable | A library providing a framework for general undo and redo operations | (part of IX.IObservable below) |
| IX.Observable | A library providing various collection types in a manner that is observable and that can be databound to various controls or simply to provide a way to observe changes. | [Project page](https://github.com/adimosh/IX.Observable) (separate project) |
| IX.Sandbox | A set of libraries for providing a sandbox environment. | - |

## Contributing

### Guidelines

Contributing can be done by anyone, at any time and in any form, as long as the contributor
has read the [contributing guidelines](https://adimosh.github.io/contributingguidelines)
beforehand and tries their best to abide by them.

### Localization

Localization is important, and we strive to have resources localized in an effective way across languages.

Currently, main languages (first component of a language group) are supported, with plans to add later.

Targeted languages:
- English (also neutral language)
- Romanian - developer's native language
- French - translator available
- German - contributions welcome
- Spanish - contributions welcome
- Japanese - contributions welcome
- A proper variant for China - contributions and ideas welcome
- A proper variant for India - contributions and ideas welcome
- Italian - contributions welcome
- Swedish - contributions welcome
- Russian - contributions welcome

### Code health checks

| Build | Status |
|:-----:|:------:|
| Master branch | [![Build Status](https://ixiancorp.visualstudio.com/IX.Framework/_apis/build/status/Master%20CI/IX.Framework%20master%20CI?branchName=master)](https://ixiancorp.visualstudio.com/IX.Framework/_build/latest?definitionId=4&branchName=master) |
| Continuous integration | [![Build Status](https://ixiancorp.visualstudio.com/IX.Framework/_apis/build/status/Development%20CI/IX.Framework%20continuous%20integration?branchName=dev)](https://ixiancorp.visualstudio.com/IX.Framework/_build/latest?definitionId=2&branchName=dev) |

### Developer guidelines

All projects in this repository build in Visual Studio 2017 at least 15.4 and use some of the language enhancements that it brought. The project
structure also follows the .NET Core CSPROJ standard.

Since the tooling is very difficult to work with and mostly unavailable, Visual Studio 2017 has been chosen as a suitable IDE with a good-enough
project structure for the purposes of this project. There are no plans to port this to earlier editions of Visual Studio.

There is also the matter of text template files, which are a must in order to avoid dreary development tasks, and they are currently only supported
as a tool in the full edition of Visual Studio.

Visual Studio Code should, to the extent of the author's knowledge, also work (at least for vanilla code changes), but that IDE is not going to
be supported as the de-facto tooling, instead focusing on development with the familiar IDE that is used in commercial development. OF course,
being an open-source project, working with and on this repository is fully legally compatible with Visual Studio Community Edition.

Should any special build, platform or standard be specifically required in the future, please point it out, as well as giving a reason/scenario
in which things did not work out with the current targets. Such input is always welcome, since the author cannot commit to developing on all
available platforms and operating systems at the same time.

## Acknowledgments

This project uses the following libraries:

- .NET Framework Core, available from the [.NET Foundation](https://github.com/dotnet)
- StyleCop analyzer, available from [its GitHub page](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
- xunit.net, available from [its GitHub page](http://xunit.github.io/)

This project uses the following tools:

- [Visual Studio](https://visualstudio.microsoft.com/) Community Edition [2019](https://visualstudio.microsoft.com/vs/), from [Microsoft](https://www.microsoft.com)
- [GhostDoc](http://submain.com/products/ghostdoc.aspx), from [SubMain](http://submain.com)
- [ReSharper](https://www.jetbrains.com/resharper/), from [JetBrains](https://www.jetbrains.com)
- Mads Kristensen's fabulous and numerous tools and extensions, which are too many to name and are available at
[his GitHub page](https://github.com/madskristensen/)
- [ErrorProne.NET](https://github.com/SergeyTeplyakov/ErrorProne.NET), by [Sergey Teplyakov](https://blogs.msdn.microsoft.com/seteplia/)

There is also [EditorConfig](http://editorconfig.org/) support and an .editorconfig file
included that works with Visual Studio 2017's baked-in support.

The project is hosted by [GitHub](https://github.com) and its build server is powered by
[Azure DevOps](https://dev.azure.com). We used to use [AppVeyor](https://www.appveyor.com/), however, it turned
out that Azure DevOps was a better fit for what we were trying to achieve.