// <copyright file="ImmutableValueTuple.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using JetBrains.Annotations;

namespace IX.StandardExtensions.Efficiency
{
#pragma warning disable SA1402 // File may only contain a single type
    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        public ImmutableValueTuple(TItem1 item1)
        {
            this.Item1 = item1;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    /// <typeparam name="TItem3">The type of the item at index 3.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2, TItem3>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// The item at index 3.
        /// </summary>
        public readonly TItem3 Item3;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2, TItem3}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        /// <param name="item3">The value of the item at index 3.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2, TItem3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    /// <typeparam name="TItem3">The type of the item at index 3.</typeparam>
    /// <typeparam name="TItem4">The type of the item at index 4.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2, TItem3, TItem4>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// The item at index 3.
        /// </summary>
        public readonly TItem3 Item3;

        /// <summary>
        /// The item at index 4.
        /// </summary>
        public readonly TItem4 Item4;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2, TItem3, TItem4}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        /// <param name="item3">The value of the item at index 3.</param>
        /// <param name="item4">The value of the item at index 4.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    /// <typeparam name="TItem3">The type of the item at index 3.</typeparam>
    /// <typeparam name="TItem4">The type of the item at index 4.</typeparam>
    /// <typeparam name="TItem5">The type of the item at index 5.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2, TItem3, TItem4, TItem5>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// The item at index 3.
        /// </summary>
        public readonly TItem3 Item3;

        /// <summary>
        /// The item at index 4.
        /// </summary>
        public readonly TItem4 Item4;

        /// <summary>
        /// The item at index 5.
        /// </summary>
        public readonly TItem5 Item5;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2, TItem3, TItem4, TItem5}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        /// <param name="item3">The value of the item at index 3.</param>
        /// <param name="item4">The value of the item at index 4.</param>
        /// <param name="item5">The value of the item at index 5.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4, TItem5 item5)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    /// <typeparam name="TItem3">The type of the item at index 3.</typeparam>
    /// <typeparam name="TItem4">The type of the item at index 4.</typeparam>
    /// <typeparam name="TItem5">The type of the item at index 5.</typeparam>
    /// <typeparam name="TItem6">The type of the item at index 6.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2, TItem3, TItem4, TItem5, TItem6>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// The item at index 3.
        /// </summary>
        public readonly TItem3 Item3;

        /// <summary>
        /// The item at index 4.
        /// </summary>
        public readonly TItem4 Item4;

        /// <summary>
        /// The item at index 5.
        /// </summary>
        public readonly TItem5 Item5;

        /// <summary>
        /// The item at index 6.
        /// </summary>
        public readonly TItem6 Item6;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2, TItem3, TItem4, TItem5, TItem6}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        /// <param name="item3">The value of the item at index 3.</param>
        /// <param name="item4">The value of the item at index 4.</param>
        /// <param name="item5">The value of the item at index 5.</param>
        /// <param name="item6">The value of the item at index 6.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4, TItem5 item5, TItem6 item6)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
            this.Item6 = item6;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    /// <typeparam name="TItem3">The type of the item at index 3.</typeparam>
    /// <typeparam name="TItem4">The type of the item at index 4.</typeparam>
    /// <typeparam name="TItem5">The type of the item at index 5.</typeparam>
    /// <typeparam name="TItem6">The type of the item at index 6.</typeparam>
    /// <typeparam name="TItem7">The type of the item at index 7.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// The item at index 3.
        /// </summary>
        public readonly TItem3 Item3;

        /// <summary>
        /// The item at index 4.
        /// </summary>
        public readonly TItem4 Item4;

        /// <summary>
        /// The item at index 5.
        /// </summary>
        public readonly TItem5 Item5;

        /// <summary>
        /// The item at index 6.
        /// </summary>
        public readonly TItem6 Item6;

        /// <summary>
        /// The item at index 7.
        /// </summary>
        public readonly TItem7 Item7;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        /// <param name="item3">The value of the item at index 3.</param>
        /// <param name="item4">The value of the item at index 4.</param>
        /// <param name="item5">The value of the item at index 5.</param>
        /// <param name="item6">The value of the item at index 6.</param>
        /// <param name="item7">The value of the item at index 7.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4, TItem5 item5, TItem6 item6, TItem7 item7)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
            this.Item6 = item6;
            this.Item7 = item7;
        }
    }

    /// <summary>
    /// An immutable value tuple.
    /// </summary>
    /// <typeparam name="TItem1">The type of the item at index 1.</typeparam>
    /// <typeparam name="TItem2">The type of the item at index 2.</typeparam>
    /// <typeparam name="TItem3">The type of the item at index 3.</typeparam>
    /// <typeparam name="TItem4">The type of the item at index 4.</typeparam>
    /// <typeparam name="TItem5">The type of the item at index 5.</typeparam>
    /// <typeparam name="TItem6">The type of the item at index 6.</typeparam>
    /// <typeparam name="TItem7">The type of the item at index 7.</typeparam>
    /// <typeparam name="TItem8">The type of the item at index 8.</typeparam>
    [PublicAPI]
    public readonly struct ImmutableValueTuple<TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7, TItem8>
    {
        /// <summary>
        /// The item at index 1.
        /// </summary>
        public readonly TItem1 Item1;

        /// <summary>
        /// The item at index 2.
        /// </summary>
        public readonly TItem2 Item2;

        /// <summary>
        /// The item at index 3.
        /// </summary>
        public readonly TItem3 Item3;

        /// <summary>
        /// The item at index 4.
        /// </summary>
        public readonly TItem4 Item4;

        /// <summary>
        /// The item at index 5.
        /// </summary>
        public readonly TItem5 Item5;

        /// <summary>
        /// The item at index 6.
        /// </summary>
        public readonly TItem6 Item6;

        /// <summary>
        /// The item at index 7.
        /// </summary>
        public readonly TItem7 Item7;

        /// <summary>
        /// The item at index 8.
        /// </summary>
        public readonly TItem8 Item8;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmutableValueTuple{TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7, TItem8}"/> struct.
        /// </summary>
        /// <param name="item1">The value of the item at index 1.</param>
        /// <param name="item2">The value of the item at index 2.</param>
        /// <param name="item3">The value of the item at index 3.</param>
        /// <param name="item4">The value of the item at index 4.</param>
        /// <param name="item5">The value of the item at index 5.</param>
        /// <param name="item6">The value of the item at index 6.</param>
        /// <param name="item7">The value of the item at index 7.</param>
        /// <param name="item8">The value of the item at index 8.</param>
        public ImmutableValueTuple(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4, TItem5 item5, TItem6 item6, TItem7 item7, TItem8 item8)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
            this.Item6 = item6;
            this.Item7 = item7;
            this.Item8 = item8;
        }
    }
#pragma warning restore SA1402 // File may only contain a single type
}