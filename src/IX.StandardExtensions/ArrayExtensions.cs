// <copyright file="ArrayExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     Extensions for array types.
    /// </summary>
    [PublicAPI]
    public static partial class ArrayExtensions
    {
        /// <summary>
        ///     Deep clones an array.
        /// </summary>
        /// <typeparam name="T">The type of the items of the array to clone.</typeparam>
        /// <param name="source">The source array.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public static T[] DeepClone<T>(this T[] source)
            where T : IDeepCloneable<T>
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));

            var length = source.Length;

            var destination = new T[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i].DeepClone();
            }

            return destination;
        }

        /// <summary>
        ///     Copies an array with shallow clones of its items.
        /// </summary>
        /// <typeparam name="T">The type of the items of the array to clone.</typeparam>
        /// <param name="source">The source array.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public static T[] CopyWithShallowClones<T>(this T[] source)
            where T : IShallowCloneable<T>
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));

            var length = source.Length;

            var destination = new T[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i].ShallowClone();
            }

            return destination;
        }

        /// <summary>
        ///     Copies all items in the specified source array to a new array.
        /// </summary>
        /// <typeparam name="T">The type of the array items.</typeparam>
        /// <param name="source">The source array.</param>
        /// <returns>A new array with items copied.</returns>
        /// <remarks>
        ///     <para>
        ///         This method copies all items by reference for reference types. Their instance will be the same as the
        ///         original source array.
        ///     </para>
        ///     <para>If deep cloning is required, please use the <see cref="DeepClone{T}(T[])" /> instead of this method.</para>
        ///     <para>Value types are value-copied.</para>
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public static T[] Copy<T>(this T[] source)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));

            var length = source.Length;

            var destination = new T[length];

            Array.Copy(
                source,
                destination,
                length);

            return destination;
        }

        /// <summary>
        ///     Copies all items in the specified source array to a new array.
        /// </summary>
        /// <typeparam name="T">The type of the array items.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="length">The length of the sub-array to copy.</param>
        /// <returns>A new array with items copied.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="length" /> is greater than 0
        ///     or
        ///     the length exceeds the bounds of the source array.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         This method copies all items by reference for reference types. Their instance will be the same as the
        ///         original source array.
        ///     </para>
        ///     <para>If deep cloning is required, please use the <see cref="DeepClone{T}(T[])" /> instead of this method.</para>
        ///     <para>Value types are value-copied.</para>
        /// </remarks>
        public static T[] Copy<T>(
            this T[] source,
            int length)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresValidArrayLength(
                in length,
                in source,
                nameof(length));

            var destination = new T[length];

            Array.Copy(
                source,
                destination,
                length);

            return destination;
        }

        /// <summary>
        ///     Copies all items in the specified source array to a new array.
        /// </summary>
        /// <typeparam name="T">The type of the array items.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="sourceIndex">The index at which to start.</param>
        /// <param name="length">The length of the sub-array to copy.</param>
        /// <returns>A new array with items copied.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="sourceIndex" /> is less than 0 or greater than the size of the array
        ///     or
        ///     <paramref name="length" /> is greater than 0
        ///     or
        ///     the source index and length exceed the bounds of the source array.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         This method copies all items by reference for reference types. Their instance will be the same as the
        ///         original source array.
        ///     </para>
        ///     <para>If deep cloning is required, please use the <see cref="DeepClone{T}(T[])" /> instead of this method.</para>
        ///     <para>Value types are value-copied.</para>
        /// </remarks>
        public static T[] Copy<T>(
            this T[] source,
            int sourceIndex,
            int length)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresValidArrayRange(
                in sourceIndex,
                in length,
                in source,
                nameof(length));

            var destination = new T[length];

            Array.Copy(
                source,
                sourceIndex,
                destination,
                0,
                length);

            return destination;
        }

        /// <summary>
        ///     Executes an action for each one of the elements of an array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="source">The array to run on.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="source" /> or <paramref name="action" /> is
        ///     <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public static void ForEach<T>(
            this T[] source,
            Action<T> action)
        {
            Contract.RequiresNotNull(
                in source,
                nameof(source));
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            for (var i = 0; i < source.Length; i++)
            {
                action(source[i]);
            }
        }
    }
}