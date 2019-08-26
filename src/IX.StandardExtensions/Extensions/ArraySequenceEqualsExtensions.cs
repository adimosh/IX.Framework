// <copyright file="ArraySequenceEqualsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using IX.StandardExtensions.Efficiency;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    /// SequenceEquals extensions specific to arrays.
    /// </summary>
    [PublicAPI]
    public static partial class ArraySequenceEqualsExtensions
    {
        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this T[] left, T[] right)
        {
            InFunc<T, T, bool> comparer;
            if (typeof(IEquatable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = EquateWithIEquatableOfT;

                bool EquateWithIEquatableOfT(in T c1, in T c2)
                    => ((IEquatable<T>)c1).Equals(c2);
            }
            else if (typeof(IComparable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = EquateWithIComparableOfT;

                bool EquateWithIComparableOfT(in T c1, in T c2)
                    => ((IComparable<T>)c1).CompareTo(c2) == 0;
            }
            else if (typeof(IComparable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                comparer = EquateWithIComparable;

                bool EquateWithIComparable(in T c1, in T c2)
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation
                    => ((IComparable)c1).CompareTo(c2) == 0;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
            }
            else
            {
                comparer = EquateAsObjects;

                bool EquateAsObjects(in T c1, in T c2)
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation
                    => c1.Equals(c2);
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
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
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this T[] left, T[] right, IEqualityComparer<T> comparer)
        {
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable, as we need a closure here anyway
            return SequenceEqualsInternal(
                left,
                right,
                EquateWithComparer);
#pragma warning restore HAA0603 // Delegate allocation from a method group

            bool EquateWithComparer(in T c1, in T c2)
                => comparer.Equals(c1, c2);
        }

        private static bool SequenceEqualsInternal<T>(T[] left, T[] right, InFunc<T, T, bool> comparer)
        {
            if (!CheckForNulls(in left, in right))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (!comparer(in left[i], in right[i]))
                {
                    return false;
                }
            }

            return true;

            bool CheckForNulls(in T[] leftOperand, in T[] rightOperand) => leftOperand == null ? rightOperand == null : rightOperand != null;
        }
    }
}