// <copyright file="GlobalSuppressions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0301:Closure Allocation Source", Justification = "We're not really interested in closures in unit tests.", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0302:Display class allocation to capture closure", Justification = "We're not really interested in closures in unit tests.", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0603:Delegate allocation from a method group", Justification = "Not a concern in unit tests.", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("CodeSmell", "ERP023:Only ex.Message property was observed in exception block!", Justification = "Not a concern in unit tests.", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0601:Value type to reference type conversion causing boxing allocation", Justification = "Not a concern in unit tests.", Scope = "module")]