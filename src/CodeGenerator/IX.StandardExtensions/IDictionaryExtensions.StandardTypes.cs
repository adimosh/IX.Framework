// <copyright file="IDictionaryExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for IDictionary.
    /// </summary>
    public static partial class IDictionaryExtensions
    {
        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, byte> DeepClone<TKey>(this Dictionary<TKey, byte> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, byte>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, sbyte> DeepClone<TKey>(this Dictionary<TKey, sbyte> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, sbyte>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, short> DeepClone<TKey>(this Dictionary<TKey, short> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, short>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, ushort> DeepClone<TKey>(this Dictionary<TKey, ushort> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, ushort>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, char> DeepClone<TKey>(this Dictionary<TKey, char> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, char>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, int> DeepClone<TKey>(this Dictionary<TKey, int> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, int>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, uint> DeepClone<TKey>(this Dictionary<TKey, uint> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, uint>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, long> DeepClone<TKey>(this Dictionary<TKey, long> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, long>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, ulong> DeepClone<TKey>(this Dictionary<TKey, ulong> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, ulong>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, float> DeepClone<TKey>(this Dictionary<TKey, float> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, float>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, double> DeepClone<TKey>(this Dictionary<TKey, double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, double>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, decimal> DeepClone<TKey>(this Dictionary<TKey, decimal> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, decimal>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, DateTime> DeepClone<TKey>(this Dictionary<TKey, DateTime> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, DateTime>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, bool> DeepClone<TKey>(this Dictionary<TKey, bool> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, bool>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, TimeSpan> DeepClone<TKey>(this Dictionary<TKey, TimeSpan> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, TimeSpan>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }

        /// <summary>
        /// Creates a deep clone of a dictionary, with deep clones of its values.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A deeply-cloned dictionary.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static Dictionary<TKey, string> DeepClone<TKey>(this Dictionary<TKey, string> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new Dictionary<TKey, string>();

            source.ForEach((p, dest) => dest.Add(p.Key, p.Value), destination);

            return destination;
        }
    }
}