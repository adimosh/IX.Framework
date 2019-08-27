// <copyright file="IEnumerableSequenceEqualsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// SequenceEquals extensions for IEnumerable.
    /// </summary>
    [PublicAPI]

    // ReSharper disable once InconsistentNaming - IEnumerable extensions :)
    public static partial class IEnumerableSequenceEqualsExtensions
    {
        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals<T>(
            this IEnumerable<IEquatable<T>> left,
            IEnumerable<T> right) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals<T>(
            this IEnumerable<IComparable<T>> left,
            IEnumerable<T> right) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals(
            this IEnumerable<IComparable> left,
            IEnumerable<object> right) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            IEqualityComparer<T> comparer) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right,
            comparer);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            IComparer<T> comparer) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right,
            comparer);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, T, bool> comparer) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right,
            comparer);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEquals<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, T, int> comparer) => Extensions.IEnumerableExtensions.SequenceEquals(
            left,
            right,
            comparer);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another by object comparison.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEqualsByObjectComparison<T>(
            this IEnumerable<object> left,
            IEnumerable<object> right) => Extensions.IEnumerableExtensions.SequenceEqualsByObjectComparison<T>(
            left,
            right);

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another by reference.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEqualsByReference<T>(
            this IEnumerable<object> left,
            IEnumerable<object> right) => Extensions.IEnumerableExtensions.SequenceEqualsByReference<T>(
            left,
            right);
    }
}