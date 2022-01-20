# .NET Framework versions estimated lifecycle and support

Below is an estimated support lifecycle for .NET Framework and above. It is compiled based on
lifecycle and support documents coming from Microsoft, and is not meant to be an official support
document.

You can use this document to roughly gauge what versions are supported at this moment, and when
support will end.

Please note that, in order to encourage upgrade, the current table will only show IX.Framework
support that is current. You are intentionally left on your own to figure out if previous
IX.Framework versions support older, out-of-support frameworks or not.

NuGet packages for frameworks that have ended support are marked obsolete. If you find one
that is not marked obsolete, please let me know by opening an issue here.

For every version of the below framework versions that become unsupported, the next release of
any library in the framework will remove support for it.

## .NET Version lifecycle

### Symbols legend

| Symbol | Meaning |
|:------:|:-------:|
| :x: | Not supported |
| :heavy_exclamation_mark: | Still supported, but use is not advised |
| :warning: | Still supported, but only for a short while |
| :heavy_check_mark: | Supported |

### .NET Framework

| Version | IX Support | MS Support |
|:-------:|:----------:|:----------:|
| <= 1.1 SP1 | :x: | :x: (14th July, 2015) |
| 2.0 SP2 | :x: | :heavy_exclamation_mark: (9th January, 2029)_1_ |
| 3.0 SP2 | :x: | :heavy_exclamation_mark: (9th January, 2029)_1_ |
| 3.5 SP1 | :x: | :heavy_check_mark: (9th January, 2029)_1_ |
| 4.0 | :x: | :x: (12th January, 2016) |
| 4.5 | :x: | :x: (12th January, 2016) |
| 4.5.1 | :x: | :x: (12th January, 2016) |
| 4.5.2 | :x: | :warning: (26th April, 2022) |
| 4.6 | :warning: | :warning: (26th April, 2022) |
| 4.6.1 | :warning: | :warning: (26th April, 2022) |
| 4.6.2 | :heavy_check_mark: | :heavy_check_mark: (12th January, 2027)_2_ |
| 4.7 | :heavy_check_mark: | :heavy_check_mark: (12th January, 2027)_2_ |
| 4.7.1 | :heavy_check_mark: | :heavy_check_mark: (12th January, 2027)_2_ |
| 4.7.2 | :heavy_check_mark: | :heavy_check_mark: (9th January, 2029)_2_ |
| 4.8 | :heavy_check_mark: | :heavy_check_mark: (14th October, 2031)_3_ |

_1_ - .NET Framework versions 2.0 and 3.0 are noted to be an integral part of .NET 3.5, and are supported
under a single lifecycle policy, and Microsoft stated that their components will continue to be supported
for as long as .NET 3. SP1 remains in support; full statement can be found
[on this page](https://docs.microsoft.com/en-us/lifecycle/faq/dotnet-framework).

_2_ - Estimated date based on available information at the time of last references update

_3_ - Support for .NET Framework 4.8 doesn't have a definitive ending date yet, so the ending date of
Windows Server 2022, which is the farthest in time, is presumed.

### .NET Standard

The .NET Standard, as a stepping stone for truly unifying the .NET platform, was intended as a way
to standardize the common features guaranteed to be available on supported platforms. As such, the
concept of "support" is not really applicable, but can be a good indicator to whether or not the
standard in question may or may not have already lost traction, and shuld be abandoned.

| Version | IX Support | MS Support |
|:-------:|:----------:|:----------:|
| 1.0 | :x: | :x: (12th January, 2016) |
| 1.1 | :x: | :x: (12th January, 2016) |
| 1.2 | :x: | :x: (12th January, 2016) |
| 1.3 | :x: | :heavy_exclamation_mark: (26th April, 2022)_1, 2_ |
| 1.4 | :x: | :heavy_exclamation_mark: (26th April, 2022)_1, 2_ |
| 1.5 | :x: | :heavy_exclamation_mark: (26th April, 2022)_1, 2_ |
| 1.6 | :x: | :heavy_exclamation_mark: (26th April, 2022)_1, 2_ |
| 2.0 | :warning: | :warning: (26th April, 2022)_1_ |
| 2.1 | :heavy_check_mark: | :heavy_check_mark: (3rd December, 2022)_3_ |

_1_ - Estimated date based on available information on framework compatibility with .NET Standard

_2_ - Official practice is to no longer target .NET Standard below 2.0 if avoidable
([source here](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/cross-platform-targeting))

_3_ - .NET Standard 2.1 is still supported in .NET Core 3.1 LTS

### .NET Core

| Version | IX Support | MS Support |
|:-------:|:----------:|:----------:|
| 1.0 | :x: | :x: (27th June, 2019) |
| 1.1 | :x: | :x: (27th June, 2019) |
| 2.0 | :x: | :x: (1st October, 2018) |
| 2.1 | :x: | :x: (21st August, 2021) |
| 2.2 | :x: | :x: (23rd December, 2019) |
| 3.0 | :x: | :x: (3rd March, 2020) |
| 3.1 | :heavy_check_mark: | :heavy_check_mark: (3rd December, 2022) |

### .NET

| Version | IX Support | MS Support |
|:-------:|:----------:|:----------:|
| 5 | :warning: | :warning: (8th May, 2022) |
| 6 | :heavy_check_mark: | :heavy_check_mark: (8th November, 2024) |

## Source Windows OS lifecycle table

This table shows estimated Microsoft Windows estimated support dates, onto which .NET versions are
based.

Like the previous tables, this is compiled as support for IX.Framework users and is not meant to be
an official Microsoft support document.

| Version | Support | EHS |
|:-------:|:-------:|:---:|
| Windows 7 SP1 | :x: 2020.01.14 | :heavy_exclamation_mark: 2023.01.10 |
| Windows Server 2008 R2 | :x: 2020.01.14 | :heavy_exclamation_mark: 2024.01.09 |
| Windows 8.1 | :heavy_exclamation_mark: 2023.01.10 | - |
| Windows Server 2012 | :heavy_check_mark: 2023.10.10 | :heavy_check_mark: 2026.10.13 |
| Windows Server 2012 R2 | :heavy_check_mark: 2023.10.10 | :heavy_check_mark: 2026.10.13 |
| Windows 10 1507 | :x: 2017.05.09 | - |
| Windows 10 1151 | :x: 2017.10.10 | - |
| Windows 10 1607 | :x: 2019.04.09 | - |
| Windows 10 1703 | :x: 2019.10.08 | - |
| Windows 10 1709 | :x: 2020.10.13 | - |
| Windows 10 1803 | :x: 2021.05.11 | - |
| Windows 10 1809 | :x: 2021.05.11 | - |
| Windows 10 1903 | :x: 2020.12.08 | - |
| Windows 10 1909 | :heavy_exclamation_mark: 2022.05.10 | - |
| Windows 10 2004 | :x: 2021.12.04 | - |
| Windows 10 20H2 | :heavy_exclamation_mark: 2023.05.09 | - |
| Windows 10 21H1 | :heavy_exclamation_mark: 2022.12.13 | - |
| Windows 10 21H2 | :heavy_check_mark: 2025.10.14 | - |
| Windows 10 2015 LTSB | :heavy_check_mark: 2025.10.14 | - |
| Windows 10 2016 LTSB | :heavy_check_mark: 2026.10.13 | - |
| Windows 10 2019 LTSC | :heavy_check_mark: 2029.01.09 | - |
| Windows Server 2016 | :heavy_check_mark: 2027.01.12 | - |
| Windows Server Version 1709 | :x: 2019.04.09 | - |
| Windows Server Version 1803 | :x: 2019.11.12 | - |
| Windows Server 2019 | :heavy_check_mark: 2029.01.09 | - |
| Windows Server Version 1809 | :x: 2020.11.10 | - |
| Windows Server Version 1903 | :x: 2020.12.08 | - |
| Windows Server Version 1909 | :x: 2021.05.11 | - |
| Windows Server Version 2004 | :x: 2021.12.14 | - |
| Windows Server Version 20H2 | :warning: 2022.08.11 | - |
| Windows Server 2022 | :heavy_check_mark: 2031.10.14 | - |
| Windows 11 21H2 | :heavy_check_mark: 2024.10.08 | - |

## References

Last update: 13th November, 2021

- [Lifecycle FAQ - .NET Framework](https://docs.microsoft.com/en-us/lifecycle/faq/dotnet-framework)
- [Windows 11 Enterprise and Education (Version 21H2)](https://docs.microsoft.com/en-us/lifecycle/products/windows-11-enterprise-and-education-version-21h2)
- [Windows 11 Home and Pro (Version 21H2)](https://docs.microsoft.com/en-us/lifecycle/products/windows-11-home-and-pro-version-21h2)
- [Windows 10 Enterprise and Education](https://docs.microsoft.com/en-us/lifecycle/products/windows-10-enterprise-and-education)
- [Windows 10 Home and Pro](https://docs.microsoft.com/en-us/lifecycle/products/windows-10-home-and-pro)
- [Windows 10 2015 LTSB](https://docs.microsoft.com/en-us/lifecycle/products/windows-10-2015-ltsb)
- [Windows 10 2016 LTSB](https://docs.microsoft.com/en-us/lifecycle/products/windows-10-2016-ltsb)
- [Windows 10 LTSC 2019](https://docs.microsoft.com/en-us/lifecycle/products/windows-10-ltsc-2019)
- [Windows Server 2022](https://docs.microsoft.com/en-us/lifecycle/products/windows-server-2022)
- [Windows Server 2019](https://docs.microsoft.com/en-us/lifecycle/products/windows-server-2019)
- [Windows Server 2016](https://docs.microsoft.com/en-us/lifecycle/products/windows-server-2016)
- [Windows Server](https://docs.microsoft.com/en-us/lifecycle/products/windows-server)
- [Windows Server 2012 R2](https://docs.microsoft.com/en-us/lifecycle/products/windows-server-2012-r2)
- [Windows Server 2012](https://docs.microsoft.com/en-us/lifecycle/products/windows-server-2012)
- [Windows Server 2008 R2](https://docs.microsoft.com/en-us/lifecycle/products/windows-server-2008-r2)
- [Windows 8.1](https://docs.microsoft.com/en-us/lifecycle/products/windows-81)
- [Windows 7](https://docs.microsoft.com/en-us/lifecycle/products/windows-7)
- [Microsoft .NET Framework](https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-framework)
- [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
- [.NET and .NET Core Support Policy](https://dotnet.microsoft.com/platform/support/policy/dotnet-core)
- [Cross-platform targeting](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/cross-platform-targeting)