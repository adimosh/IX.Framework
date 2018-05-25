// <copyright file="ArraySequenceCompareExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extension methods for comparison in array.
    /// </summary>
    public static partial class ArraySequenceCompareExtensions
    {
        /// <summary>
        /// Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this T[] left, T[] right)
        {
            Func<T, T, int> comparer;
            if (typeof(IComparable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = SequenceCompareWithIComparableOfT;

                int SequenceCompareWithIComparableOfT(T c1, T c2)
                    => ((IComparable<T>)c1).CompareTo(c2);
            }
            else if (typeof(IComparable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = SequenceComparerWithIComparable;

                int SequenceComparerWithIComparable(T c1, T c2)
                    => ((IComparable)c1).CompareTo(c2);
            }
            else
            {
                comparer = SequenceCompareWithObjectEquality;

                int SequenceCompareWithObjectEquality(T c1, T c2)
                    => c1.Equals(c2) ? 0 : -1;
            }

            return SequenceCompare(
                left,
                right,
                comparer);
        }

        /// <summary>
        /// Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this T[] left, T[] right, IComparer<T> comparer)
        {
            return SequenceCompare(
                left,
                right,
#pragma warning disable HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group - This is acceptable, as we need a closure here anyway
                CompareUsingComparer);
#pragma warning restore HeapAnalyzerMethodGroupAllocationRule // Delegate allocation from a method group

            int CompareUsingComparer(T c1, T c2)
                => comparer.Compare(c1, c2);
        }

        /// <summary>
        /// Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this T[] left, T[] right, Func<T, T, int> comparer)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

            if (right == null)
            {
                return 1;
            }

            var i = 0;
            while (true)
            {
                var b1 = i < left.Length;
                var b2 = i < right.Length;

                if (!b1 && !b2)
                {
                    return 0;
                }

                T c1 = b1 ? left[i] : default;
                T c2 = b2 ? right[i] : default;

                var cr = comparer(c1, c2);
                if (cr != 0)
                {
                    return cr;
                }
            }
        }
    }
}