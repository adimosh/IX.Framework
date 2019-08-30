// <copyright file="ArrayExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static T[] DeepClone<T>(this T[] source)
            where T : IDeepCloneable<T> => Extensions.ArrayExtensions.DeepClone(source);

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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static T[] CopyWithShallowClones<T>(this T[] source)
            where T : IShallowCloneable<T> => Extensions.ArrayExtensions.CopyWithShallowClones(source);

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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static T[] Copy<T>(this T[] source) => Extensions.ArrayExtensions.Copy(source);

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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static T[] Copy<T>(
            this T[] source,
            int length) => Extensions.ArrayExtensions.Copy(
            source,
            length);

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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static T[] Copy<T>(
            this T[] source,
            int sourceIndex,
            int length) => Extensions.ArrayExtensions.Copy(
            source,
            sourceIndex,
            length);

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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static void ForEach<T>(
            this T[] source,
            Action<T> action) => Extensions.ArrayExtensions.ForEach(
            source,
            action);
    }
}