// <copyright file="ArraySequenceCompareExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.StandardExtensions.Efficiency;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     Extension methods for comparison in array.
    /// </summary>
    [PublicAPI]
    public static partial class ArraySequenceCompareExtensions
    {
        /// <summary>
        ///     Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare<T>(
            this T[] left,
            T[] right) => Extensions.ArraySequenceCompareExtensions.SequenceCompare(
            left,
            right);

        /// <summary>
        ///     Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare<T>(
            this T[] left,
            T[] right,
            IComparer<T> comparer) => Extensions.ArraySequenceCompareExtensions.SequenceCompare(
            left,
            right,
            comparer);

        /// <summary>
        ///     Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompare<T>(
            this T[] left,
            T[] right,
            InFunc<T, T, int> comparer) => Extensions.ArraySequenceCompareExtensions.SequenceCompare(
            left,
            right,
            comparer);
    }
}