// <copyright file="INamedVariable{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A contract for a variable of a specific type and name.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    [PublicAPI]
    public interface INamedVariable<T> : IVariable<T>, IComparable<INamedVariable<T>>, IEquatable<INamedVariable<T>>, INamedVariable
    {
    }
}