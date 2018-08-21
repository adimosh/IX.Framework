// <copyright file="IEnumerateEquateSequentiallyExtensions.StandardTypes.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/> and <see cref="System.Collections.IEnumerable"/> dealing with sequential equality.
    /// </summary>
    public static partial class IEnumerateEquateSequentiallyExtensions
    {
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - This is acceptable, as these are IEnumerable extensions
        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<byte> left, IEnumerable<byte> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<byte> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<byte> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<byte> left, IEnumerable<byte> right, Func<byte, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<byte> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<byte> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<byte> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<sbyte> left, IEnumerable<sbyte> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<sbyte> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<sbyte> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<sbyte> left, IEnumerable<sbyte> right, Func<sbyte, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<sbyte> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<sbyte> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<sbyte> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<short> left, IEnumerable<short> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<short> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<short> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<short> left, IEnumerable<short> right, Func<short, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<short> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<short> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<short> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ushort> left, IEnumerable<ushort> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<ushort> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<ushort> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ushort> left, IEnumerable<ushort> right, Func<ushort, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<ushort> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<ushort> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<ushort> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<char> left, IEnumerable<char> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<char> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<char> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<char> left, IEnumerable<char> right, Func<char, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<char> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<char> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<char> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<int> left, IEnumerable<int> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<int> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<int> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<int> left, IEnumerable<int> right, Func<int, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<int> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<int> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<int> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<uint> left, IEnumerable<uint> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<uint> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<uint> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<uint> left, IEnumerable<uint> right, Func<uint, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<uint> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<uint> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<uint> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<long> left, IEnumerable<long> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<long> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<long> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<long> left, IEnumerable<long> right, Func<long, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<long> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<long> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<long> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ulong> left, IEnumerable<ulong> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<ulong> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<ulong> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<ulong> left, IEnumerable<ulong> right, Func<ulong, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<ulong> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<ulong> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<ulong> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<float> left, IEnumerable<float> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<float> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<float> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<float> left, IEnumerable<float> right, Func<float, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<float> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<float> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<float> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<double> left, IEnumerable<double> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<double> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<double> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<double> left, IEnumerable<double> right, Func<double, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<double> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<double> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<double> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<decimal> left, IEnumerable<decimal> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<decimal> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<decimal> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<decimal> left, IEnumerable<decimal> right, Func<decimal, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<decimal> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<decimal> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<decimal> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<DateTime> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<DateTime> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<DateTime> left, IEnumerable<DateTime> right, Func<DateTime, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<DateTime> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<DateTime> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<DateTime> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<bool> left, IEnumerable<bool> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<bool> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<bool> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<bool> left, IEnumerable<bool> right, Func<bool, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<bool> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<bool> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<bool> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<TimeSpan> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<TimeSpan> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<TimeSpan> left, IEnumerable<TimeSpan> right, Func<TimeSpan, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<TimeSpan> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<TimeSpan> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<TimeSpan> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<string> left, IEnumerable<string> right)
        {
            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<string> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<string> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = leftEnumerator.MoveNext();
                    var rightBool = rightEnumerator.MoveNext();

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = leftEnumerator.MoveNext();
                        rightBool = rightEnumerator.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Equates two enumerable collections sequentially, skipping items defined as &quot;empty&quot;.
        /// </summary>
        /// <param name="left">The left item of comparison.</param>
        /// <param name="right">The right item of comparison.</param>
        /// <param name="determineEmpty">A function that determines whether an item is &quot;empty&quot; or not.</param>
        /// <returns>A sequence of comparison results.</returns>
        public static IEnumerable<bool> EquateSequentially(this IEnumerable<string> left, IEnumerable<string> right, Func<string, bool> determineEmpty)
        {
            if (determineEmpty == null)
            {
                throw new ArgumentNullException(nameof(determineEmpty));
            }

            if ((left == null || !left.Any()) && (right == null || !right.Any()))
            {
                yield return true;
                yield break;
            }

            if (left == null || !left.Any())
            {
                yield return false;
                yield break;
            }

            if (right == null || !right.Any())
            {
                yield return false;
                yield break;
            }

            using (IEnumerator<string> leftEnumerator = left.GetEnumerator())
            {
                using (IEnumerator<string> rightEnumerator = right.GetEnumerator())
                {
                    var leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                    var rightBool = EquateSequentiallyMoveNext(rightEnumerator);

                    while (leftBool || rightBool)
                    {
                        var leftCompare = leftBool ? leftEnumerator.Current : default;
                        var rightCompare = rightBool ? rightEnumerator.Current : default;

                        yield return leftCompare == rightCompare;

                        leftBool = EquateSequentiallyMoveNext(leftEnumerator);
                        rightBool = EquateSequentiallyMoveNext(rightEnumerator);
                    }
                }
            }

            bool EquateSequentiallyMoveNext(IEnumerator<string> source)
            {
                init:
                var moved = source.MoveNext();

                if (!moved)
                {
                    return false;
                }

                if (determineEmpty(source.Current))
                {
                    goto init;
                }

                return true;
            }
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
    }
}