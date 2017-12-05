// <copyright file="ArraySequenceCompareWithMsbExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extension methods for comparison in array with regard to the most significant byte.
    /// </summary>
    public static partial class ArraySequenceCompareWithMsbExtensions
    {
        /// <summary>
        /// Compares two arrays to one another sequentially with regard to the most significant byte.
        /// </summary>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompareWithMsb(this byte[] left, byte[] right)
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

            var length = left.Length > right.Length ? left.Length : right.Length;

            if (left.Length < length)
            {
                byte[] newLeft = new byte[length];
                left.CopyTo(newLeft, 0);
                left = newLeft;
            }

            if (right.Length < length)
            {
                byte[] newRight = new byte[length];
                right.CopyTo(newRight, 0);
                right = newRight;
            }

            for (var i = length - 1; i >= 0; i--)
            {
                var cr = left[i].CompareTo(right[i]);

                if (cr != 0)
                {
                    return cr;
                }
            }

            return 0;
        }
    }
}