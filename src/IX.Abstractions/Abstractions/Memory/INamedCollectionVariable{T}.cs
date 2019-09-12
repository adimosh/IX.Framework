// <copyright file="INamedCollectionVariable{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.Abstractions.Memory
{
    /// <summary>
    /// A named collection variable.
    /// </summary>
    /// <typeparam name="T">The type of item in the collection.</typeparam>
    /// <seealso cref="IX.Abstractions.Memory.INamedVariable{T}" />
    [PublicAPI]
    public interface INamedCollectionVariable<T> : INamedVariable<IEnumerable<T>>, ICollection<T>, ICollection<IVariable<T>>
    {
    }
}