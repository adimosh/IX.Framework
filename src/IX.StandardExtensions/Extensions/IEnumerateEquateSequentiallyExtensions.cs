// <copyright file="IEnumerateEquateSequentiallyExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> and <see cref="IEnumerable"/> dealing with sequential equality.
    /// </summary>
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
        public static IEnumerable<bool> EquateSequentially<T>(this IEnumerable<T> left, IEnumerable<T> right)
            => EquateSequentially(left, right, null, null);

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
        public static IEnumerable<bool> EquateSequentially<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, bool> determineEmpty)
            => EquateSequentially(left, right, null, determineEmpty);

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
        public static IEnumerable<bool> EquateSequentially<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer)
            => EquateSequentially(left, right, comparer, null);

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
        public static IEnumerable<bool> EquateSequentially<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer, Func<T, bool> determineEmpty)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Currently unavoidable, will do more later

            // TODO: #68 - Eliminate boxing from IEnumerable implementations
            using (IEnumerator<T> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<T> rightEnumerator = right.GetEnumerator())
                {
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
                    if (comparer == null)
                    {
                        if (typeof(IEquatable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
                        {
                            comparer = EquateUsingIEquatableOfT;

                            bool EquateUsingIEquatableOfT(T l, T r)
                                 => (l as IEquatable<T>)?.Equals(r) ?? (r as IEquatable<T>)?.Equals(l) ?? true;
                        }
                        else if (typeof(IComparable<T>).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
                        {
                            comparer = EquateSequentiallyUsingIComparerOfT;

                            bool EquateSequentiallyUsingIComparerOfT(T l, T r)
                                => ((l as IComparable<T>)?.CompareTo(r) ?? (r as IComparable<T>)?.CompareTo(l) ?? 0) == 0;
                        }
                        else if (typeof(IComparable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
                        {
                            comparer = EquateSequentiallyWithIComparable;

                            bool EquateSequentiallyWithIComparable(T l, T r)
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Unavoidable here
                                 => ((l as IComparable)?.CompareTo(r) ?? (r as IComparable)?.CompareTo(l) ?? 0) == 0;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
                        }
                        else
                        {
                            comparer = EquateSequentiallyAsObjects;

                            bool EquateSequentiallyAsObjects(T l, T r)
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - Unavoidable here
                                => l?.Equals(r) ?? r?.Equals(l) ?? true;
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
                        }
                    }

                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        T leftCompare = leftBool ? leftEnumerator.Current : default;
                        T rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return comparer(leftCompare, rightCompare);

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<T> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty == null)
                {
                    return true;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }
    }
}