// <copyright file="BitwiseExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extension methods for bitwise operations regarding bitwise operations.
    /// </summary>
    public static class BitwiseExtensions
    {
        /// <summary>
        /// Shifts all the bits in the bit array to the left.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted bit array.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="howManyBits"/> is less than zero.
        /// </exception>
        public static BitArray LeftShift(this BitArray data, int howManyBits)
        {
            BitArray result = Shift(data, howManyBits);

            if (result == null)
            {
                result = new BitArray(data);

                for (var i = 0; i < result.Length - howManyBits; i++)
                {
                    result[i] = result[i + howManyBits];
                }

                for (var i = result.Length - howManyBits; i < result.Length; i++)
                {
                    result[i] = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Shifts all the bits in the bit array to the right.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted bit array.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="howManyBits"/> is less than zero.
        /// </exception>
        public static BitArray RightShift(this BitArray data, int howManyBits)
        {
            BitArray result = Shift(data, howManyBits);

            if (result == null)
            {
                result = new BitArray(data);

                for (var i = result.Length - 1; i >= howManyBits; i--)
                {
                    result[i] = result[i - howManyBits];
                }

                for (var i = 0; i < howManyBits; i++)
                {
                    result[i] = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Shifts all the bits in the byte array to the left.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted byte array.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="howManyBits"/> is less than zero.
        /// </exception>
        public static byte[] LeftShift(this byte[] data, int howManyBits)
        {
            var ba = new BitArray(data ?? throw new ArgumentNullException(nameof(data)));

            ba = LeftShift(ba, howManyBits);

            var result = new byte[data.Length];
            ((ICollection)ba).CopyTo(result, 0);

            return result;
        }

        /// <summary>
        /// Shifts all the bits in the byte array to the right.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted byte array.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="howManyBits"/> is less than zero.
        /// </exception>
        public static byte[] RightShift(this byte[] data, int howManyBits)
        {
            var ba = new BitArray(data ?? throw new ArgumentNullException(nameof(data)));

            ba = RightShift(ba, howManyBits);

            var result = new byte[data.Length];
            ((ICollection)ba).CopyTo(result, 0);

            return result;
        }

        private static BitArray Shift(BitArray data, int howManyBits)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (howManyBits < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(howManyBits));
            }

            if (howManyBits == 0)
            {
                return new BitArray(data);
            }
            else if (howManyBits >= data.Length)
            {
                return new BitArray(data.Length);
            }
            else
            {
                return null;
            }
        }
    }
}