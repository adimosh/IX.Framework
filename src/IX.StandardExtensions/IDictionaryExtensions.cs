// <copyright file="IDictionaryExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for IDictionary.
    /// </summary>
    public static partial class IDictionaryExtensions
    {
        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, TValue> DeepClone<TKey, TValue>(this Dictionary<TKey, TValue> source)
            where TValue : IDeepCloneable<TValue>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, TValue>();

            source.ForEach(p => destination.Add(p.Key, p.Value.DeepClone()));

            return destination;
        }

        /// <summary>
        /// Creates a clone of a dictionary, with shallow clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A cloned dictionary with shallow clones.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, TValue> CopyWithShallowClones<TKey, TValue>(this Dictionary<TKey, TValue> source)
            where TValue : IShallowCloneable<TValue>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, TValue>();

            source.ForEach(p => destination.Add(p.Key, p.Value.ShallowClone()));

            return destination;
        }
    }
}