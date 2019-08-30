// <copyright file="IDictionaryExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.StandardExtensions.Contracts;

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
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, byte> DeepClone<TKey>(this Dictionary<TKey, byte> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, sbyte> DeepClone<TKey>(this Dictionary<TKey, sbyte> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, short> DeepClone<TKey>(this Dictionary<TKey, short> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, ushort> DeepClone<TKey>(this Dictionary<TKey, ushort> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, char> DeepClone<TKey>(this Dictionary<TKey, char> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, int> DeepClone<TKey>(this Dictionary<TKey, int> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, uint> DeepClone<TKey>(this Dictionary<TKey, uint> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, long> DeepClone<TKey>(this Dictionary<TKey, long> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, ulong> DeepClone<TKey>(this Dictionary<TKey, ulong> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, float> DeepClone<TKey>(this Dictionary<TKey, float> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, double> DeepClone<TKey>(this Dictionary<TKey, double> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, decimal> DeepClone<TKey>(this Dictionary<TKey, decimal> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, DateTime> DeepClone<TKey>(this Dictionary<TKey, DateTime> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, bool> DeepClone<TKey>(this Dictionary<TKey, bool> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, TimeSpan> DeepClone<TKey>(this Dictionary<TKey, TimeSpan> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static Dictionary<TKey, string> DeepClone<TKey>(this Dictionary<TKey, string> source)
            => Extensions.IDictionaryExtensions.DeepClone(source);
    }
}