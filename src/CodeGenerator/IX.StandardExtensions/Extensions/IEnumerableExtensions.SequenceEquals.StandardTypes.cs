// <copyright file="IEnumerableExtensions.SequenceEquals.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    /// Extensions for IEnumerable.
    /// </summary>
    // ReSharper disable once InconsistentNaming - We're doing extensions for IEnumerable
    public static partial class IEnumerableExtensions
    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<byte> left, IEnumerable<byte> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<byte> e1 = left.GetEnumerator())
            {
                using (IEnumerator<byte> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<sbyte> left, IEnumerable<sbyte> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<sbyte> e1 = left.GetEnumerator())
            {
                using (IEnumerator<sbyte> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<short> left, IEnumerable<short> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<short> e1 = left.GetEnumerator())
            {
                using (IEnumerator<short> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<ushort> left, IEnumerable<ushort> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<ushort> e1 = left.GetEnumerator())
            {
                using (IEnumerator<ushort> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<char> left, IEnumerable<char> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<char> e1 = left.GetEnumerator())
            {
                using (IEnumerator<char> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<int> left, IEnumerable<int> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<int> e1 = left.GetEnumerator())
            {
                using (IEnumerator<int> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<uint> left, IEnumerable<uint> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<uint> e1 = left.GetEnumerator())
            {
                using (IEnumerator<uint> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<long> left, IEnumerable<long> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<long> e1 = left.GetEnumerator())
            {
                using (IEnumerator<long> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<ulong> left, IEnumerable<ulong> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<ulong> e1 = left.GetEnumerator())
            {
                using (IEnumerator<ulong> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<float> left, IEnumerable<float> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<float> e1 = left.GetEnumerator())
            {
                using (IEnumerator<float> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<double> left, IEnumerable<double> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<double> e1 = left.GetEnumerator())
            {
                using (IEnumerator<double> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<decimal> left, IEnumerable<decimal> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<decimal> e1 = left.GetEnumerator())
            {
                using (IEnumerator<decimal> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<DateTime> e1 = left.GetEnumerator())
            {
                using (IEnumerator<DateTime> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<bool> left, IEnumerable<bool> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<bool> e1 = left.GetEnumerator())
            {
                using (IEnumerator<bool> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<TimeSpan> e1 = left.GetEnumerator())
            {
                using (IEnumerator<TimeSpan> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><see langword="true"/> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <see langword="false"/> otherwise.</returns>
        public static bool SequenceEquals(this IEnumerable<string> left, IEnumerable<string> right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            using (IEnumerator<string> e1 = left.GetEnumerator())
            {
                using (IEnumerator<string> e2 = right.GetEnumerator())
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
                            if (e1.Current != e2.Current)
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
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}