// <copyright file="IVariable.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A contract for a variable.
    /// </summary>
    [PublicAPI]
    public interface IVariable : IDisposable
    {
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
        /// Gets or sets the boxed value contained within the variable.
        /// </summary>
        /// <value>The boxed value.</value>
        /// <remarks>
        /// <para>This property will box/unbox values as required by the caller, based on the abilities of the implementing class.</para>
        /// <para>For this reason, please refrain from using this property if you don't absolutely have to.</para>
        /// </remarks>
        object BoxedValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether the value stored in this variable is the default value for this type.
        /// </summary>
        /// <value><see langword="true"/> if the contained value is default for the type; otherwise, <see langword="false"/>.</value>
        bool IsDefault { get; }

        /// <summary>
        /// Gets the discreet type of the data contained within the variable.
        /// </summary>
        /// <returns>The data type.</returns>
        Type GetDataType();
    }
}