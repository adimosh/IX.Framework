// <copyright file="IEnumerableSequenceCompareExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.StandardExtensions
{
    /// <summary>
    /// SequenceEquals extensions for IEnumerable.
    /// </summary>
    public static partial class IEnumerableSequenceCompareExtensions
    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<byte> left, IEnumerable<byte> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<sbyte> left, IEnumerable<sbyte> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<short> left, IEnumerable<short> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<ushort> left, IEnumerable<ushort> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<char> left, IEnumerable<char> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<int> left, IEnumerable<int> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<uint> left, IEnumerable<uint> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<long> left, IEnumerable<long> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<ulong> left, IEnumerable<ulong> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<float> left, IEnumerable<float> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<double> left, IEnumerable<double> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<decimal> left, IEnumerable<decimal> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<bool> left, IEnumerable<bool> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(this IEnumerable<string> left, IEnumerable<string> right)
            => Extensions.IEnumerableExtensions.SequenceCompare(left, right);
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}