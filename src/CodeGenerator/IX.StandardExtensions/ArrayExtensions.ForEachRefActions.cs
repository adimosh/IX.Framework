// <copyright file="ArrayExtensions.ForEachRefActions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.Contracts;
using IX.StandardExtensions.Efficiency;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     Extensions for array types.
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1> action,
            ref TParam1 param1)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2> action,
            ref TParam1 param1,
            ref TParam2 param2)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2, TParam3>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2, TParam3> action,
            ref TParam1 param1,
            ref TParam2 param2,
            ref TParam3 param3)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2, ref param3);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2. This parameter is passed by reference.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2, TParam3, TParam4>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2, TParam3, TParam4> action,
            ref TParam1 param1,
            ref TParam2 param2,
            ref TParam3 param3,
            ref TParam4 param4)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2, ref param3, ref param4);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2. This parameter is passed by reference.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3. This parameter is passed by reference.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2, TParam3, TParam4, TParam5>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2, TParam3, TParam4, TParam5> action,
            ref TParam1 param1,
            ref TParam2 param2,
            ref TParam3 param3,
            ref TParam4 param4,
            ref TParam5 param5)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2, ref param3, ref param4, ref param5);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2. This parameter is passed by reference.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3. This parameter is passed by reference.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4. This parameter is passed by reference.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action,
            ref TParam1 param1,
            ref TParam2 param2,
            ref TParam3 param3,
            ref TParam4 param4,
            ref TParam5 param5,
            ref TParam6 param6)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2, ref param3, ref param4, ref param5, ref param6);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2. This parameter is passed by reference.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3. This parameter is passed by reference.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4. This parameter is passed by reference.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5. This parameter is passed by reference.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> action,
            ref TParam1 param1,
            ref TParam2 param2,
            ref TParam3 param3,
            ref TParam4 param4,
            ref TParam5 param5,
            ref TParam6 param6,
            ref TParam7 param7)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2, ref param3, ref param4, ref param5, ref param6, ref param7);
            }
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="TItem">The array type.</typeparam>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0. This parameter is passed by reference.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1. This parameter is passed by reference.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2. This parameter is passed by reference.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3. This parameter is passed by reference.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4. This parameter is passed by reference.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5. This parameter is passed by reference.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6. This parameter is passed by reference.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7. This parameter is passed by reference.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        public static void ForEach<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(
            this TItem[] source,
            RefIteratorAction<TItem, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> action,
            ref TParam1 param1,
            ref TParam2 param2,
            ref TParam3 param3,
            ref TParam4 param4,
            ref TParam5 param5,
            ref TParam6 param6,
            ref TParam7 param7,
            ref TParam8 param8)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i], ref param1, ref param2, ref param3, ref param4, ref param5, ref param6, ref param7, ref param8);
            }
        }
    }
}