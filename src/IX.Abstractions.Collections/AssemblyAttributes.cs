// <copyright file="AssemblyAttributes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using IX.Abstractions.Collections;

[assembly: InternalsVisibleTo("IX.Abstractions.UnitTests")]
[assembly: ContractNamespace(Constants.DataContractNamespace, ClrNamespace = nameof(IX.Abstractions.Collections))]