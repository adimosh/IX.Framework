// <copyright file="ICollectionAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;

namespace IX.Observable.Adapters
{
    /// <summary>
    /// An adapter interface for non-standard collection types.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="global::System.Collections.Generic.IReadOnlyCollection{T}" />
    /// <seealso cref="global::System.Collections.Generic.ICollection{T}" />
    /// <seealso cref="ICollection" />
    public interface ICollectionAdapter<T> : IReadOnlyCollection<T>, ICollection<T>, ICollection
    {
        /// <summary>
        /// Occurs when the owner of this list adapter must reset.
        /// </summary>
        event EventHandler MustReset;
    }
}