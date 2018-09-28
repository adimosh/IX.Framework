// <copyright file="IEnumerableSequenceCompareExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

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
        public static int SequenceCompare<T>(this IEnumerable<IComparable<T>> left, IEnumerable<T> right)
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

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
            using (IEnumerator<IComparable<T>> e1 = left.GetEnumerator())
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

                        IComparable<T> c1 = b1 ? e1.Current : default;
                        T c2 = b2 ? e2.Current : default;

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<IComparable> left, IEnumerable<object> right)
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

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
            using (IEnumerator<IComparable> e1 = left.GetEnumerator())
            {
                using (IEnumerator<object> e2 = right.GetEnumerator())
                {
                    while (true)
                    {
                        var b1 = e1.MoveNext();
                        var b2 = e2.MoveNext();

                        if (!b1 && !b2)
                        {
                            return 0;
                        }

                        IComparable c1 = b1 ? e1.Current : default;
                        var c2 = b2 ? e2.Current : default;

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Compares two enumerable sequences to one another with the aid of a comparer.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable item.</typeparam>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <param name="comparer">The comparer to use when equating items.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare<T>(this IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
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

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
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

                        var cr = comparer.Compare(c1, c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Compares two enumerable sequences to one another with the aid of a comparer function.
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

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Compares two enumerable sequences to one another, by object comparison.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompareByObjectComparison(this IEnumerable<object> left, IEnumerable<object> right)
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

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
            using (IEnumerator<object> e1 = left.GetEnumerator())
            {
                using (IEnumerator<object> e2 = right.GetEnumerator())
                {
                    while (true)
                    {
                        var b1 = e1.MoveNext();
                        var b2 = e2.MoveNext();

                        if (!b1 && !b2)
                        {
                            return 0;
                        }

                        var c1 = b1 ? e1.Current : default;
                        var c2 = b2 ? e2.Current : default;

                        var cr = (c1 == null && c2 != null) ? -1 : ((c1 != null && c2 == null) ? 1 : (c1.Equals(c2) ? 0 : -1));
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }

        /// <summary>
        /// Compares two enumerable sequences to one another, by reference.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompareByReference(this IEnumerable<object> left, IEnumerable<object> right)
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

#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
            using (IEnumerator<object> e1 = left.GetEnumerator())
            {
                using (IEnumerator<object> e2 = right.GetEnumerator())
                {
                    while (true)
                    {
                        var b1 = e1.MoveNext();
                        var b2 = e2.MoveNext();

                        if (!b1 && !b2)
                        {
                            return 0;
                        }

                        var c1 = b1 ? e1.Current : default;
                        var c2 = b2 ? e2.Current : default;

                        var cr = (c1 == null && c2 != null) ? -1 : ((c1 != null && c2 == null) ? 1 : (object.ReferenceEquals(c1, c2) ? 0 : -1));
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
        }
    }
}