// <copyright file="ArraySequenceEqualsWithMsbExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extension methods for equality in array with regard to the most significant byte.
    /// </summary>
    public static partial class ArraySequenceEqualsWithMsbExtensions
    {
        /// <summary>
        /// Compares two arrays to one another sequentially with regard to the most significant byte.
        /// </summary>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <returns>The result of the comparison.</returns>
        public static bool SequenceEqualsWithMsb(this byte[] left, byte[] right)
        {
            if (left == null)
            {
                // Left is null, we return based on whether or not right is null as well
                return right == null;
            }

            if (right == null)
            {
                // Right is null, but not left
                return false;
            }

            var length = left.Length > right.Length ? left.Length : right.Length;

            if (left.Length < length)
            {
                var newLeft = new byte[length];
                left.CopyTo(newLeft, 0);
                left = newLeft;
            }

            if (right.Length < length)
            {
                var newRight = new byte[length];
                right.CopyTo(newRight, 0);
                right = newRight;
            }

            for (var i = length - 1; i >= 0; i--)
            {
                var cr = left[i].Equals(right[i]);

                if (!cr)
                {
                    return false;
                }
            }

            return true;
        }
    }
}