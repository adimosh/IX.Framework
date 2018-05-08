// <copyright file="IEnumerableSequenceEqualsExtensions.cs" company="Adrian Mos">
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
    public static partial class IEnumerableSequenceEqualsExtensions
    {
        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> left, IEnumerable<T> right)
        {
            Func<T, T, bool> comparer;
            if (typeof(IEquatable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = (c1, c2) => ((IEquatable<T>)c1).Equals(c2);
            }
            else if (typeof(IComparable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = (c1, c2) => ((IComparable<T>)c1).CompareTo(c2) == 0;
            }
            else
            {
                comparer = (c1, c2) => c1.Equals(c2);
            }

            return SequenceEqualsInternal(
                left,
                right,
                comparer);
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another by using a comparer.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparer) =>
            SequenceEqualsInternal(
                left,
                right,
                (c1, c2) => comparer.Equals(c1, c2));

        private static bool CheckForNulls<T>(in IEnumerable<T> left, in IEnumerable<T> right) => (left == null) ? (right == null) : (right != null);

        private static bool SequenceEqualsInternal<T>(in IEnumerable<T> left, in IEnumerable<T> right, in Func<T, T, bool> comparer)
        {
            if (!CheckForNulls(left, right))
            {
                return false;
            }

            using (IEnumerator<T> e1 = left.GetEnumerator())
            {
                using (IEnumerator<T> e2 = right.GetEnumerator())
                {
                    while (true)
                    {
                        var b1 = e1.MoveNext();
                        var b2 = e2.MoveNext();

                        if (b1 != b2)
                        {
                            return false;
                        }

                        if (b1)
                        {
                            if (!comparer(e1.Current, e2.Current))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }
    }
}