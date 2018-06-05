// <copyright file="GlobalSuppressions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "HeapAnalyzerExplicitNewObjectRule:Explicit new reference type allocation", Justification = "Explicit new allocations will only be optimizable in the future.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HeapAnalyzerImplicitParamsRule:Array allocation for params parameter", Justification = "So params allocates an empty array anytime. So what? It's not like I have a choice.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HeapAnalyzerExplicitNewArrayRule:Explicit new array type allocation", Justification = "Explicit array creation will be refactored at a later time, wherever necessary.", Scope = "module")]