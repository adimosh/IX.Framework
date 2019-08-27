// <copyright file="IEnumerateEquateSequentiallyExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> and <see cref="System.Collections.IEnumerable"/> dealing with sequential equality.
    /// </summary>
    public static partial class IEnumerateEquateSequentiallyExtensions
    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<byte> left, IEnumerable<byte> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<byte> left, IEnumerable<byte> right, Func<byte, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<sbyte> left, IEnumerable<sbyte> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<sbyte> left, IEnumerable<sbyte> right, Func<sbyte, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<short> left, IEnumerable<short> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<short> left, IEnumerable<short> right, Func<short, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ushort> left, IEnumerable<ushort> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ushort> left, IEnumerable<ushort> right, Func<ushort, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<char> left, IEnumerable<char> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<char> left, IEnumerable<char> right, Func<char, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<int> left, IEnumerable<int> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<int> left, IEnumerable<int> right, Func<int, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<uint> left, IEnumerable<uint> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<uint> left, IEnumerable<uint> right, Func<uint, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<long> left, IEnumerable<long> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<long> left, IEnumerable<long> right, Func<long, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ulong> left, IEnumerable<ulong> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ulong> left, IEnumerable<ulong> right, Func<ulong, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<float> left, IEnumerable<float> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<float> left, IEnumerable<float> right, Func<float, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<double> left, IEnumerable<double> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<double> left, IEnumerable<double> right, Func<double, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<decimal> left, IEnumerable<decimal> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<decimal> left, IEnumerable<decimal> right, Func<decimal, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<DateTime> left, IEnumerable<DateTime> right, Func<DateTime, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<bool> left, IEnumerable<bool> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<bool> left, IEnumerable<bool> right, Func<bool, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right, Func<TimeSpan, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<string> left, IEnumerable<string> right)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<string> left, IEnumerable<string> right, Func<string, bool> determineEmpty)
            => Extensions.IEnumerableExtensions.EquateSequentially(left, right, determineEmpty);
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}