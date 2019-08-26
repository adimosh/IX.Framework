// <copyright file="ArraySequenceCompareWithMsbExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using JetBrains.Annotations;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    ///     Extension methods for comparison in array with regard to the most significant byte.
    /// </summary>
    [PublicAPI]
    public static class ArraySequenceCompareWithMsbExtensions
    {
        /// <summary>
        ///     Compares two arrays to one another sequentially with regard to the most significant byte.
        /// </summary>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <returns>The result of the comparison.</returns>
        public static int SequenceCompareWithMsb(
            this byte[] left,
            byte[] right)
        {
            if (left == null)
            {
                // Left is null, we return based on whether or not right is null as well
                return right == null ? 0 : int.MinValue;
            }

            if (right == null)
            {
                // Right is null, but not left
                return int.MaxValue;
            }

            var length = left.Length > right.Length ? left.Length : right.Length;

            if (left.Length < length)
            {
                var newLeft = new byte[length];
                left.CopyTo(
                    newLeft,
                    0);
                left = newLeft;
            }

            if (right.Length < length)
            {
                var newRight = new byte[length];
                right.CopyTo(
                    newRight,
                    0);
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