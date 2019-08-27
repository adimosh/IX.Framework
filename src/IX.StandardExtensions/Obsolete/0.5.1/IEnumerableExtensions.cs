// <copyright file="IEnumerableExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for IEnumerable.
    /// </summary>
    [PublicAPI]

    // ReSharper disable once InconsistentNaming - We're doing extensions for IEnumerable
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Executes an action for each one of the elements of an enumerable.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void ForEach<T>(
            this IEnumerable<T> source,
            Action<T> action) => Extensions.IEnumerableExtensions.ForEach(
            source,
            action);

        /// <summary>
        /// Executes an action for each one of the elements of an enumerable.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void ForEach(
            this IEnumerable source,
            Action<object> action) => Extensions.IEnumerableExtensions.ForEach(
            source,
            action);

#if !STANDARD
        /// <summary>
        /// Executes an independent action for each one of the elements of an enumerable, in parallel.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void ParallelForEach<T>(
            this IEnumerable<T> source,
            Action<T> action) => Extensions.IEnumerableExtensions.ParallelForEach(
            source,
            action);
#endif

        /// <summary>
        /// Executes an action in sequence with an iterator.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void For<T>(
            this IEnumerable<T> source,
            Action<int, T> action) => Extensions.IEnumerableExtensions.For(
            source,
            action);

        /// <summary>
        /// Executes an action in sequence with an iterator.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void For(
            this IEnumerable source,
            Action<int, object> action) => Extensions.IEnumerableExtensions.For(
            source,
            action);

#if !STANDARD
        /// <summary>
        /// Executes an independent action in parallel, with an iterator that respects the original sequence.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void ParallelFor<T>(
            this IEnumerable<T> source,
            Action<int, T> action) => Extensions.IEnumerableExtensions.ParallelFor(
            source,
            action);
#endif
    }
}