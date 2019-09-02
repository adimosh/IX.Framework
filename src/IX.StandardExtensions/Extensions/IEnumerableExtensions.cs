// <copyright file="IEnumerableExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    /// Extensions for IEnumerable.
    /// </summary>
    [PublicAPI]

    // ReSharper disable once InconsistentNaming - We're doing extensions for IEnumerable
    public static partial class IEnumerableExtensions
    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Makes sense, as this is IEnumerable extensions
        /// <summary>
        /// Executes an action for each one of the elements of an enumerable.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (T item in source)
            {
                action(item);
            }
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

        /// <summary>
        /// Executes an action for each one of the elements of an enumerable.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ForEach(this IEnumerable source, Action<object> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (var item in source)
            {
                action(item);
            }
        }

#if !STANDARD
        /// <summary>
        /// Executes an independent action for each one of the elements of an enumerable, in parallel.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ParallelForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            Parallel.ForEach(source, action);
        }
#endif

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Makes sense, as this is IEnumerable extensions
        /// <summary>
        /// Executes an action in sequence with an iterator.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void For<T>(this IEnumerable<T> source, Action<int, T> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            var i = 0;
            foreach (T item in source)
            {
                action(i, item);
                i++;
            }
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator

        /// <summary>
        /// Executes an action in sequence with an iterator.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void For(this IEnumerable source, Action<int, object> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            var i = 0;
            foreach (var item in source)
            {
                action(i, item);
                i++;
            }
        }

#if !STANDARD
#pragma warning disable HAA0603 // Delegate allocation from a method group - Unavoidable
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This makes sense as it is IEnumerable extensions
        /// <summary>
        /// Executes an independent action in parallel, with an iterator that respects the original sequence.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ParallelFor<T>(this IEnumerable<T> source, Action<int, T> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            Parallel.ForEach(EnumerateWithIndex(source, action), PerformParallelAction);

            IEnumerable<Tuple<int, T, Action<int, T>>> EnumerateWithIndex(IEnumerable<T> sourceEnumerable, Action<int, T> actionToPerform)
            {
                var i = 0;
                foreach (T item in sourceEnumerable)
                {
                    yield return new Tuple<int, T, Action<int, T>>(i, item, actionToPerform);
                    i++;
                }
            }

            void PerformParallelAction(Tuple<int, T, Action<int, T>> state) => state.Item3(state.Item1, state.Item2);
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
#pragma warning restore HAA0603 // Delegate allocation from a method group
#endif
    }
}