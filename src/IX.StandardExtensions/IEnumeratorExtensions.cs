// <copyright file="IEnumeratorExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extension methods for IEnumerator.
    /// </summary>
    public static class IEnumeratorExtensions
    {
        /// <summary>
        /// Executes an action for each one of the elements of an enumerator.
        /// </summary>
        /// <typeparam name="T">The enumerator type.</typeparam>
        /// <param name="source">The enumerator source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ForEach<T>(this IEnumerator<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            while (source.MoveNext())
            {
                action(source.Current);
            }
        }

        /// <summary>
        /// Executes an action for each one of the elements of a non-generic enumerator.
        /// </summary>
        /// <param name="source">The enumerator source.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ForEach(this IEnumerator source, Action<object> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            while (source.MoveNext())
            {
                action(source.Current);
            }
        }
    }
}