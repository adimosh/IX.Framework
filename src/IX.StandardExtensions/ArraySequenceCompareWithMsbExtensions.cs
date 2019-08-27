// <copyright file="ArraySequenceCompareWithMsbExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using JetBrains.Annotations;

namespace IX.StandardExtensions
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
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static int SequenceCompareWithMsb(
            this byte[] left,
            byte[] right) => Extensions.ArrayExtensions.SequenceCompareWithMsb(
            left,
            right);
    }
}