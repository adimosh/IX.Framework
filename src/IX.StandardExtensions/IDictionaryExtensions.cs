// <copyright file="IDictionaryExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     Extensions for IDictionary.
    /// </summary>
    [PublicAPI]

    // ReSharper disable once InconsistentNaming - We are doing extensions for IDictionary :)
    public static partial class IDictionaryExtensions
    {
        /// <summary>
        ///     Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, TValue> DeepClone<TKey, TValue>(this Dictionary<TKey, TValue> source)
            where TValue : IDeepCloneable<TValue> => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        ///     Creates a clone of a dictionary, with shallow clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A cloned dictionary with shallow clones.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, TValue> CopyWithShallowClones<TKey, TValue>(this Dictionary<TKey, TValue> source)
            where TValue : IShallowCloneable<TValue> => Extensions.IDictionaryExtensions.CopyWithShallowClones(source);
    }
}