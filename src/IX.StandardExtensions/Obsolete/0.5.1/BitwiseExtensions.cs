// <copyright file="BitwiseExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     Extension methods for bitwise operations regarding bitwise operations.
    /// </summary>
    [PublicAPI]
    public static class BitwiseExtensions
    {
        /// <summary>
        ///     Shifts all the bits in the bit array to the left.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted bit array.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="data" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="howManyBits" /> is less than zero.
        /// </exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static BitArray LeftShift(
            this BitArray data,
            int howManyBits) => Extensions.BitwiseExtensions.LeftShift(
            data,
            howManyBits);

        /// <summary>
        ///     Shifts all the bits in the bit array to the right.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted bit array.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="data" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="howManyBits" /> is less than zero.
        /// </exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static BitArray RightShift(
            this BitArray data,
            int howManyBits) => Extensions.BitwiseExtensions.RightShift(
            data,
            howManyBits);

        /// <summary>
        ///     Shifts all the bits in the byte array to the left.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted byte array.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="data" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="howManyBits" /> is less than zero.
        /// </exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static byte[] LeftShift(
            this byte[] data,
            int howManyBits) => Extensions.BitwiseExtensions.LeftShift(
            data,
            howManyBits);

        /// <summary>
        ///     Shifts all the bits in the byte array to the right.
        /// </summary>
        /// <param name="data">The original data.</param>
        /// <param name="howManyBits">How many bits to shift by.</param>
        /// <returns>The shifted byte array.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="data" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="howManyBits" /> is less than zero.
        /// </exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static byte[] RightShift(
            this byte[] data,
            int howManyBits) => Extensions.BitwiseExtensions.RightShift(
            data,
            howManyBits);
    }
}