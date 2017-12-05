// <copyright file="ArraySequenceEqualsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;

namespace IX.StandardExtensions
{
    /// <summary>
    /// SequenceEquals extensions specific to arrays.
    /// </summary>
    public static partial class ArraySequenceEqualsExtensions
    {
        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals<T>(this T[] left, T[] right)
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
        public static bool SequenceEquals<T>(this T[] left, T[] right, IEqualityComparer<T> comparer) =>
            SequenceEqualsInternal(
                left,
                right,
                (c1, c2) => comparer.Equals(c1, c2));

        private static bool SequenceEqualsInternal<T>(T[] left, T[] right, Func<T, T, bool> comparer)
        {
            if (!CheckForNulls(left, right))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (!comparer(left[i], right[i]))
                {
                    return false;
                }
            }

            return true;

            bool CheckForNulls(T[] leftOperand, T[] rightOperand) => (leftOperand == null) ? (rightOperand == null) : (rightOperand != null);
        }
    }
}