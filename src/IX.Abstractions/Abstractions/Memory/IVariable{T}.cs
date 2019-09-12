// <copyright file="IVariable{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A contract for a variable of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    [PublicAPI]
    public interface IVariable<T> : IEquatable<IVariable<T>>, IComparable<IVariable<T>>, IVariable
    {
        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        /// <value>The value.</value>
        T Value { get; set; }

        /// <summary>
        /// Gets or sets the raw value of the variable for a debugger window.
        /// </summary>
        /// <value>The raw value for the debugger.</value>
        /// <remarks>
        /// <para>The getter of this property should not involve state changes within the variable.</para>
        /// </remarks>
        T RawDebuggerValue { get; set; }
    }
}