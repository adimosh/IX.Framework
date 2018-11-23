// <copyright file="IEnumerableSequenceEqualsExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

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
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<IEquatable<T>> left, IEnumerable<T> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
            using (IEnumerator<IEquatable<T>> e1 = left.GetEnumerator())
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
                            if (!e1.Current.Equals(e2.Current))
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<IComparable<T>> left, IEnumerable<T> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
            using (IEnumerator<IComparable<T>> e1 = left.GetEnumerator())
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
                            if (e1.Current.CompareTo(e2.Current) != 0)
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<IComparable> left, IEnumerable<object> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
            using (IEnumerator<IComparable> e1 = left.GetEnumerator())
            {
                using (IEnumerator<object> e2 = right.GetEnumerator())
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
                            if (e1.Current.CompareTo(e2.Current) != 0)
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparer)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
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
                            if (!comparer.Equals(e1.Current, e2.Current))
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
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
                            if (comparer.Compare(e1.Current, e2.Current) != 0)
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, bool> comparer)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals<T>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, T, int> comparer)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
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
                            if (comparer(e1.Current, e2.Current) != 0)
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another by object comparison.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEqualsByObjectComparison<T>(this IEnumerable<object> left, IEnumerable<object> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
            using (IEnumerator<object> e1 = left.GetEnumerator())
            {
                using (IEnumerator<object> e2 = right.GetEnumerator())
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
                            if (!e1.Current.Equals(e2.Current))
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another by reference.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEqualsByReference<T>(this IEnumerable<object> left, IEnumerable<object> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
            using (IEnumerator<object> e1 = left.GetEnumerator())
            {
                using (IEnumerator<object> e2 = right.GetEnumerator())
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
                            if (!object.ReferenceEquals(e1.Current, e2.Current))
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }
    }
}