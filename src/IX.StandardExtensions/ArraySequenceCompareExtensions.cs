// <copyright file="ArraySequenceCompareExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
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
        public static int SequenceCompare<T>(
            this T[] left,
            T[] right)
        {
            InFunc<T, T, int> comparer;

            if (typeof(IComparable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = SequenceCompareWithIComparableOfT;

                int SequenceCompareWithIComparableOfT(
                    in T c1,
                    in T c2)
                {
                    return ((IComparable<T>)c1).CompareTo(c2);
                }
            }
            else if (typeof(IComparable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = SequenceComparerWithIComparable;

                int SequenceComparerWithIComparable(
                    in T c1,
                    in T c2)
                {
                    return ((IComparable)c1).CompareTo(c2);
                }
            }
            else
            {
                comparer = SequenceCompareWithObjectEquality;

                int SequenceCompareWithObjectEquality(
                    in T c1,
                    in T c2)
                {
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Unavoidable due to interface implementation
                    return c1.Equals(c2) ? 0 : -1;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
                }
            }

            return SequenceCompare(
                left,
                right,
                comparer);
        }

        /// <summary>
        ///     Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(
            this T[] left,
            T[] right,
            IComparer<T> comparer)
        {
            return SequenceCompare(
                left,
                right,
#pragma warning disable HAA0603 // Delegate allocation from a method group - We know, how could this possibly be avoided?
                CompareUsingComparer);
#pragma warning restore HAA0603 // Delegate allocation from a method group

            int CompareUsingComparer(
                in T c1,
                in T c2)
            {
                return comparer.Compare(
                    c1,
                    c2);
            }
        }

        /// <summary>
        ///     Compares two arrays to one another sequentially.
        /// </summary>
        /// <typeparam name="T">The type of the item in the array.</typeparam>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(
            this T[] left,
            T[] right,
            InFunc<T, T, int> comparer)
        {
            if (left == null)
            {
                // Left is null, we return based on whether or not right is null as well
                return right == null ? 0 : int.MinValue;
            }

            if (right == null)
            {
                // Right is null, but not left
                return int.MaxValue;
            }

            var i = 0;
            while (true)
            {
                var b1 = i < left.Length;
                var b2 = i < right.Length;

                if (!b1 && !b2)
                {
                    // We have reached the end
                    return 0;
                }

                T c1 = b1 ? left[i] : default;
                T c2 = b2 ? right[i] : default;

                var cr = comparer(
                    in c1,
                    in c2);
                if (cr != 0)
                {
                    // We have reached the first difference
                    return cr;
                }

                // No difference at this level, let's proceed
                i++;
            }
        }
    }
}