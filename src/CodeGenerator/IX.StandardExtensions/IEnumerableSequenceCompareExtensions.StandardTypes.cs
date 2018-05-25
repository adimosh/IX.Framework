// <copyright file="IEnumerableSequenceCompareExtensions.StandardTypes.cs" company="Adrian Mos">
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
#pragma warning disable HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<byte> left, IEnumerable<byte> right)
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

            using (IEnumerator<byte> e1 = left.GetEnumerator())
            {
                using (IEnumerator<byte> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<sbyte> left, IEnumerable<sbyte> right)
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

            using (IEnumerator<sbyte> e1 = left.GetEnumerator())
            {
                using (IEnumerator<sbyte> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<short> left, IEnumerable<short> right)
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

            using (IEnumerator<short> e1 = left.GetEnumerator())
            {
                using (IEnumerator<short> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<ushort> left, IEnumerable<ushort> right)
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

            using (IEnumerator<ushort> e1 = left.GetEnumerator())
            {
                using (IEnumerator<ushort> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<char> left, IEnumerable<char> right)
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

            using (IEnumerator<char> e1 = left.GetEnumerator())
            {
                using (IEnumerator<char> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<int> left, IEnumerable<int> right)
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

            using (IEnumerator<int> e1 = left.GetEnumerator())
            {
                using (IEnumerator<int> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<uint> left, IEnumerable<uint> right)
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

            using (IEnumerator<uint> e1 = left.GetEnumerator())
            {
                using (IEnumerator<uint> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<long> left, IEnumerable<long> right)
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

            using (IEnumerator<long> e1 = left.GetEnumerator())
            {
                using (IEnumerator<long> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<ulong> left, IEnumerable<ulong> right)
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

            using (IEnumerator<ulong> e1 = left.GetEnumerator())
            {
                using (IEnumerator<ulong> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<float> left, IEnumerable<float> right)
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

            using (IEnumerator<float> e1 = left.GetEnumerator())
            {
                using (IEnumerator<float> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<double> left, IEnumerable<double> right)
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

            using (IEnumerator<double> e1 = left.GetEnumerator())
            {
                using (IEnumerator<double> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<decimal> left, IEnumerable<decimal> right)
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

            using (IEnumerator<decimal> e1 = left.GetEnumerator())
            {
                using (IEnumerator<decimal> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
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

            using (IEnumerator<DateTime> e1 = left.GetEnumerator())
            {
                using (IEnumerator<DateTime> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<bool> left, IEnumerable<bool> right)
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

            using (IEnumerator<bool> e1 = left.GetEnumerator())
            {
                using (IEnumerator<bool> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right)
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

            using (IEnumerator<TimeSpan> e1 = left.GetEnumerator())
            {
                using (IEnumerator<TimeSpan> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compares two enumerable sequences to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompare(this IEnumerable<string> left, IEnumerable<string> right)
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

            using (IEnumerator<string> e1 = left.GetEnumerator())
            {
                using (IEnumerator<string> e2 = right.GetEnumerator())
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

                        var cr = c1.CompareTo(c2);
                        if (cr != 0)
                        {
                            return cr;
                        }
                    }
                }
            }
        }
#pragma warning restore HeapAnalyzerEnumeratorAllocationRule // Possible allocation of reference type enumerator
    }
}