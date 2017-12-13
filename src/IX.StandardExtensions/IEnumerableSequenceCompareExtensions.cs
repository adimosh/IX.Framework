// <copyright file="IEnumerableSequenceCompareExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;

namespace IX.StandardExtensions
{
    /// <summary>
    /// SequenceEquals extensions for IEnumerable.
    /// </summary>
    public static partial class IEnumerableSequenceCompareExtensions
    {
        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this IEnumerable<T> left, IEnumerable<T> right)
        {
            Func<T, T, int> comparer;
            if (typeof(IComparable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = (c1, c2) => ((IComparable<T>)c1).CompareTo(c2);
            }
            else if (typeof(IComparable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = (c1, c2) => ((IComparable)c1).CompareTo(c2);
            }
            else
            {
                comparer = (c1, c2) => c1.Equals(c2) ? 0 : -1;
            }

            return SequenceCompare(
                left,
                right,
                comparer);
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer) =>
            SequenceCompare(
                left,
                right,
                (c1, c2) => comparer.Compare(c1, c2));

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, int> comparer)
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

            using (IEnumerator<T> e1 = left.GetEnumerator())
            {
                using (IEnumerator<T> e2 = right.GetEnumerator())
                {
                    while (true)
                    {
                        var b1 = e1.MoveNext();
                        var b2 = e2.MoveNext();

                        if (!b1 && !b2)
                        {
                            return 0;
                        }

                        T c1 = b1 ? e1.Current : default;
                        T c2 = b2 ? e2.Current : default;

                        var cr = comparer(c1, c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }
    }
}