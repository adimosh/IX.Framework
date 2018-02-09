// <copyright file="INamedVariable.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A contract for a named variable.
    /// </summary>
    public interface INamedVariable : IVariable
    {
        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        /// <value>The name of the variable.</value>
        string Name { get; }
    }
}