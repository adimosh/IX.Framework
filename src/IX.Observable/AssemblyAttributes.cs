// <copyright file="AssemblyAttributes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using IX.Observable;

[assembly: InternalsVisibleTo("IX.Observable.UnitTests")]
[assembly: ContractNamespace(Constants.DataContractNamespace, ClrNamespace = nameof(IX.Observable))]