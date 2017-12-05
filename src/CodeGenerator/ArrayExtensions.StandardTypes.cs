// <copyright file="ArrayExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for array types.
    /// </summary>
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static byte[] DeepClone(this byte[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            byte[] destination = new byte[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static sbyte[] DeepClone(this sbyte[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            sbyte[] destination = new sbyte[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static short[] DeepClone(this short[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            short[] destination = new short[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static ushort[] DeepClone(this ushort[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            ushort[] destination = new ushort[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static char[] DeepClone(this char[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            char[] destination = new char[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static int[] DeepClone(this int[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            int[] destination = new int[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static uint[] DeepClone(this uint[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            uint[] destination = new uint[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static long[] DeepClone(this long[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            long[] destination = new long[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static ulong[] DeepClone(this ulong[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            ulong[] destination = new ulong[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static float[] DeepClone(this float[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            float[] destination = new float[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static double[] DeepClone(this double[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            double[] destination = new double[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static decimal[] DeepClone(this decimal[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            decimal[] destination = new decimal[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static DateTime[] DeepClone(this DateTime[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            DateTime[] destination = new DateTime[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static bool[] DeepClone(this bool[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            bool[] destination = new bool[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static TimeSpan[] DeepClone(this TimeSpan[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            TimeSpan[] destination = new TimeSpan[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }

        /// <summary>
        /// Deep clones an array.
        /// </summary>
        /// <param name="source">The source array to deep clone.</param>
        /// <returns>An array of deeply-copied elements from the original array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static string[] DeepClone(this string[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var length = source.Length;

            string[] destination = new string[length];

            for (var i = 0; i < length; i++)
            {
                destination[i] = source[i];
            }

            return destination;
        }
    }
}