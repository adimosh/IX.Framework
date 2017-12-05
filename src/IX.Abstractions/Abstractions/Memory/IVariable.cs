// <copyright file="IVariable.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A contract for a variable.
    /// </summary>
    public interface IVariable : IDisposable
    {
        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        /// <value>The name of the variable.</value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the value that is shown in and loaded from a debugger window.
        /// </summary>
        /// <value>The debugger value.</value>
        /// <remarks>
        /// <para>Implementations of this property should treat converting to/from string representations of values.</para>
        /// <para>The getter of this property should not involve state changes within the variable.</para>
        /// </remarks>
        string DebuggerValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether the value stored in this variable is the default value for this type.
        /// </summary>
        /// <value><c>true</c> if the contained value is default for the type; otherwise, <c>false</c>.</value>
        bool IsDefault { get; }

        /// <summary>
        /// Gets the discreet type of the data contained within the variable.
        /// </summary>
        /// <returns>The data type.</returns>
        Type GetDataType();
    }
}