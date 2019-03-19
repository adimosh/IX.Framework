// <copyright file="HeapAllocationAnalyzerSuppressions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "HAA0504:Implicit new array creation allocation", Justification = "Explicit array creation is usually an intended phenomenon, for now let's ignore.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HAA0502:Explicit new reference type allocation", Justification = "Explicit new allocations will only be optimizable in the future.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HAA0101:Array allocation for params parameter", Justification = "So params allocates an empty array anytime. So what? It's not like I have a choice.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HAA0501:Explicit new array type allocation", Justification = "Explicit array creation will be refactored at a later time, wherever necessary.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HAA0505:Initializer reference type allocation", Justification = "Explicit collection creation by initializer is usually an intended phenomenon, for now let's ignore.", Scope = "module")]
[assembly: SuppressMessage("Performance", "HAA0503:Explicit new anonymous object allocation", Justification = "LINQ works that way, it is rare enough to be of little impact anyway.", Scope = "module")]

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1413:UseTrailingCommasInMultiLineInitializers", Justification = "Not observed.", Scope = "module")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1407:Arithmetic expressions should declare precedence", Justification = "Not observed.", Scope = "module")]