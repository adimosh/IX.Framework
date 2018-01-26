// <copyright file="IDictionaryCollectionAdapter{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;

namespace IX.Observable.Adapters
{
    /// <summary>
    /// A collection adapter contract for a dictionary collection adapter.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <seealso cref="IX.Observable.Adapters.ICollectionAdapter{T}" />
    /// <seealso cref="global::System.Collections.Generic.IDictionary{TKey, TValue}" />
    public interface IDictionaryCollectionAdapter<TKey, TValue> : ICollectionAdapter<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The index where the item has been added.</returns>
        new int Add(TKey key, TValue value);
    }
}