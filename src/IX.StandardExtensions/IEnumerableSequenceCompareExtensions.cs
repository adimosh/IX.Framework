// <copyright file="IEnumerableSequenceCompareExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     SequenceEquals extensions for IEnumerable.
    /// </summary>
    [PublicAPI]

    // ReSharper disable once InconsistentNaming - we're doing extension methods for IEnumerable
    public static partial class IEnumerableSequenceCompareExtensions
    {
        /// <summary>
        ///     Compares two enumerable sequences to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare<T>(
            this IEnumerable<IComparable<T>> left,
            IEnumerable<T> right) => Extensions.IEnumerableExtensions.SequenceCompare(
            left,
            right);

        /// <summary>
        ///     Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare(
            this IEnumerable<IComparable> left,
            IEnumerable<object> right) => Extensions.IEnumerableExtensions.SequenceCompare(
            left,
            right);

        /// <summary>
        ///     Compares two enumerable sequences to one another with the aid of a comparer.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            IComparer<T> comparer) => Extensions.IEnumerableExtensions.SequenceCompare(
            left,
            right,
            comparer);

        /// <summary>
        ///     Compares two enumerable sequences to one another with the aid of a comparer function.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, T, int> comparer) => Extensions.IEnumerableExtensions.SequenceCompare(
            left,
            right,
            comparer);

        /// <summary>
        ///     Compares two enumerable sequences to one another, by object comparison.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompareByObjectComparison(
            this IEnumerable<object> left,
            IEnumerable<object> right) => Extensions.IEnumerableExtensions.SequenceCompareByObjectComparison(
            left,
            right);

        /// <summary>
        ///     Compares two enumerable sequences to one another, by reference.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompareByReference(
            this IEnumerable<object> left,
            IEnumerable<object> right) => Extensions.IEnumerableExtensions.SequenceCompareByReference(
            left,
            right);
    }
}