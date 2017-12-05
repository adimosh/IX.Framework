// <copyright file="IEnumerableExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for IEnumerable.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Executes an action for each one of the elements of an enumerable.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Executes an action for each one of the elements of an enumerable.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static void ForEach(this IEnumerable source, Action<object> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

#if !NETSTANDARD1_0
        /// <summary>
        /// Executes an independent action for each one of the elements of an enumerable, in parallel.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static void ParallelForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Parallel.ForEach(source, action);
        }
#endif

        /// <summary>
        /// Executes an action in sequence with an iterator.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static void For<T>(this IEnumerable<T> source, Action<int, T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var i = 0;
            foreach (T item in source)
            {
                action(i, item);
                i++;
            }
        }

        /// <summary>
        /// Executes an action in sequence with an iterator.
        /// </summary>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static void For(this IEnumerable source, Action<int, object> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var i = 0;
            foreach (var item in source)
            {
                action(i, item);
                i++;
            }
        }

#if !NETSTANDARD1_0
        /// <summary>
        /// Executes an independent action in parallel, with an iterator that respects the original sequence.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static void ParallelFor<T>(this IEnumerable<T> source, Action<int, T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Parallel.ForEach(EnumerateWithIndex(source), (state) => action(state.Item1, state.Item2));

            IEnumerable<Tuple<int, T>> EnumerateWithIndex(IEnumerable<T> sourceEnumerable)
            {
                var i = 0;
                foreach (T item in sourceEnumerable)
                {
                    yield return new Tuple<int, T>(i, item);
                    i++;
                }

                yield break;
            }
        }
#endif
    }
}