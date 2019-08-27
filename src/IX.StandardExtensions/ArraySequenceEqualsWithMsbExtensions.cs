// <copyright file="ArraySequenceEqualsWithMsbExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extension methods for equality in array with regard to the most significant byte.
    /// </summary>
    public static class ArraySequenceEqualsWithMsbExtensions
    {
        /// <summary>
        /// Compares two arrays to one another sequentially with regard to the most significant byte.
        /// </summary>
        /// <param name="left">The left operand array.</param>
        /// <param name="right">The right operand array.</param>
        /// <returns>The result of the comparison.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool SequenceEqualsWithMsb(
            this byte[] left,
            byte[] right) => Extensions.ArrayExtensions.SequenceEqualsWithMsb(
            left,
            right);
    }
}