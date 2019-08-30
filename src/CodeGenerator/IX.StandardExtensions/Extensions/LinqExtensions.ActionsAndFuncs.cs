// <copyright file="LinqExtensions.ActionsAndFuncs.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.StandardExtensions.Contracts;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    /// Extension methods for LINQ.
    /// </summary>
    public static partial class LinqExtensions
    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1>(this IEnumerable<TItem> source, Func<TItem, TParam1, bool> action, TParam1 param1)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1>(this IEnumerable<TItem> source, Func<TItem, TParam1, bool> action, TParam1 param1)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1>(this IEnumerable<TItem> source, Func<TItem, TParam1, bool> action, TParam1 param1)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, bool> action, TParam1 param1, TParam2 param2)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, bool> action, TParam1 param1, TParam2 param2)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, bool> action, TParam1 param1, TParam2 param2)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2, TParam3>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, bool> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2, TParam3>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, bool> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2, TParam3>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, bool> action, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2, TParam3, TParam4>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2, TParam3, TParam4>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2, TParam3, TParam4>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2, TParam3, TParam4, TParam5>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2, TParam3, TParam4, TParam5>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2, TParam3, TParam4, TParam5>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked predicate at index 6.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked predicate at index 6.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6, param7))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked predicate at index 6.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked predicate at index 6.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6, param7))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked predicate at index 6.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked predicate at index 6.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6, param7))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements that match a specific predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked predicate at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked predicate at index 7.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check for emptiness.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked predicate at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked predicate at index 7.</param>
        /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static bool Any<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6, param7, param8))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked predicate at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked predicate at index 7.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to filter.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked predicate at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked predicate at index 7.</param>
        /// <returns>The filtered enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static IEnumerable<TItem> Where<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6, param7, param8))
                {
                    yield return item;
                }
            }

            yield break;
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TItem">The enumerable item type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked predicate at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked predicate at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked predicate at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked predicate at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked predicate at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked predicate at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked predicate at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked predicate at index 7.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{T}" /> to check.</param>
        /// <param name="action">The predicate to check items with.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked predicate at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked predicate at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked predicate at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked predicate at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked predicate at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked predicate at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked predicate at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked predicate at index 7.</param>
        /// <returns>The first filtered item, or a default value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="action" /> are <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static TItem FirstOrDefault<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(this IEnumerable<TItem> source, Func<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, bool> action, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            foreach (TItem item in source)
            {
                if (action(item, param1, param2, param3, param4, param5, param6, param7, param8))
                {
                    return item;
                }
            }

            return default;
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}