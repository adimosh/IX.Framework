// <copyright file="ArraySequenceEqualsExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

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
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this byte[] left, byte[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this sbyte[] left, sbyte[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this short[] left, short[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this ushort[] left, ushort[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this char[] left, char[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this int[] left, int[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this uint[] left, uint[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this long[] left, long[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this ulong[] left, ulong[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this float[] left, float[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this double[] left, double[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this decimal[] left, decimal[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this DateTime[] left, DateTime[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this bool[] left, bool[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this TimeSpan[] left, TimeSpan[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two enumerable objects have all members in sequence equal to one another.
        /// </summary>
        /// <param name="left">The left operand enumerable.</param>
        /// <param name="right">The right operand enumerable.</param>
        /// <returns><c>true</c> if the two enumerable objects have the same length and each element at each position
        /// in one enumerable is equal to the equivalent in the other, <c>false</c> otherwise.</returns>
        public static bool SequenceEquals(this string[] left, string[] right)
        {
            if ((left == null) ? (right != null) : (right == null))
            {
                return false;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}