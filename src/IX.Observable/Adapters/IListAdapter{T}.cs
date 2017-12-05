// <copyright file="IListAdapter{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved.
// </copyright>

using System.Collections;
using System.Collections.Generic;

namespace IX.Observable.Adapters
{
    /// <summary>
    /// An adapter interface for non-standard collection types.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    /// <seealso cref="ICollectionAdapter{T}" />
    /// <seealso cref="IList" />
    /// <seealso cref="global::System.Collections.Generic.IList{T}" />
    /// <seealso cref="global::System.Collections.Generic.IReadOnlyList{T}" />
    public interface IListAdapter<T> : ICollectionAdapter<T>, IList<T>, IReadOnlyList<T>, IList
    {
    }
}