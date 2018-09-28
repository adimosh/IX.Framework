// <copyright file="RandomIntegerPredictableDataStore.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.StandardExtensions.TestUtils
{
    /// <summary>
    /// A data store for storing random integers in a predictable way, such as any iteration through the store will produce the same output. This class is thread-safe, and its internal items container is immutable.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.TestUtils.PredictableDataStore{T}" />
    public class RandomIntegerPredictableDataStore : PredictableDataStore<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomIntegerPredictableDataStore"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public RandomIntegerPredictableDataStore(int capacity)
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
            : base(capacity, DataGenerator.RandomInteger)
#pragma warning restore HAA0603 // Delegate allocation from a method group
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomIntegerPredictableDataStore"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="maximumValue">The maximum value.</param>
        public RandomIntegerPredictableDataStore(int capacity, int maximumValue)
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is unavoidable, as we cannot have a generic type parameter in a constructor
            : base(capacity, (state) => DataGenerator.RandomInteger((int)state), maximumValue)
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomIntegerPredictableDataStore"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        public RandomIntegerPredictableDataStore(int capacity, int minimumValue, int maximumValue)
            : base(
                capacity,
                (state) =>
                {
                    var expandedState = (Tuple<int, int>)state;
                    return DataGenerator.RandomInteger(expandedState.Item1, expandedState.Item2);
                },
                new Tuple<int, int>(minimumValue, maximumValue))
        {
        }
    }
}