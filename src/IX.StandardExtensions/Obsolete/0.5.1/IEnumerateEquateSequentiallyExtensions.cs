// <copyright file="IEnumerateEquateSequentiallyExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> and <see cref="IEnumerable"/> dealing with sequential equality.
    /// </summary>
    [PublicAPI]
    public static partial class IEnumerateEquateSequentiallyExtensions
    {
        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>An enumerable stating which item is equal to its correspondent.</returns>
        /// <remarks>
        /// <para>For a guide on how this method is used, please refer to <see cref="EquateSequentially{T}(IEnumerable{T}, IEnumerable{T}, Func{T, T, bool}, Func{T, bool})"/>
        /// and view its remarks section.</para>
        /// </remarks>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right) => Extensions.IEnumerableExtensions.EquateSequentially(
            left,
            right);

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <remarks>
        /// <para>For a guide on how this method is used, please refer to <see cref="EquateSequentially{T}(IEnumerable{T}, IEnumerable{T}, Func{T, T, bool}, Func{T, bool})"/>
        /// and view its remarks section.</para>
        /// </remarks>
        /// <returns>An enumerable stating which item is equal to its correspondent.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, bool> determineEmpty) => Extensions.IEnumerableExtensions.EquateSequentially(
            left,
            right,
            determineEmpty);

        /// <summary>
        /// Equates two enumerable collections sequentially with a custom comparer.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="comparer">A comparer function to use.</param>
        /// <returns>An enumerable stating which item is equal to its correspondent.</returns>
        /// <remarks>
        /// <para>For a guide on how this method is used, please refer to <see cref="EquateSequentially{T}(IEnumerable{T}, IEnumerable{T}, Func{T, T, bool}, Func{T, bool})"/>
        /// and view its remarks section.</para>
        /// </remarks>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, T, bool> comparer) => Extensions.IEnumerableExtensions.EquateSequentially(
            left,
            right,
            comparer);

        /// <summary>
        /// Equates two enumerable collections sequentially with a custom comparer, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="comparer">A comparer function to use.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>An enumerable stating which item is equal to its correspondent.</returns>
        /// <remarks>
        /// <para>If the <paramref name="comparer"/> is not <see langword="null"/> (<see langword="Nothing"/> in Visual Basic), it will be used regardless of the type of the elements.</para>
        /// <para>If it is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic), the method will try to infer some comparison that is possible between the items of the type
        /// specified.</para>
        /// <para>It will first check whether or not the type is <see cref="IEquatable{T}"/>. If it is, it will use its <see cref="IEquatable{T}.Equals(T)"/> method
        /// to determine equality.</para>
        /// <para>It will then check whether or not the type is <see cref="IComparable{T}"/>. If it is, it will use its <see cref="IComparable{T}.CompareTo(T)"/> method
        /// to determine equality.</para>
        /// <para>It will then check whether or not the type is <see cref="IComparable"/>. If it is, it will use its <see cref="IComparable.CompareTo(object)"/> method
        /// to determine equality.</para>
        /// <para>It will then use the default object comparison to attempt to determine equality.</para>
        /// <para>If the <paramref name="determineEmpty"/> function is not <see langword="null"/> (<see langword="Nothing"/> in Visual Basic), then any item which is considered &quot;empty&quot;
        /// is going to be skipped over when comparing. The definition of &quot;empty&quot; depends on the implementation of the function.</para>
        /// </remarks>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<bool> EquateSequentially<T>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, T, bool> comparer,
            Func<T, bool> determineEmpty) => Extensions.IEnumerableExtensions.EquateSequentially(
            left,
            right,
            comparer,
            determineEmpty);
    }
}