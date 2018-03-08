// <copyright file="RandomIntegerPredictableDataStore.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

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
            : base(capacity, DataGenerator.RandomInteger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomIntegerPredictableDataStore"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="maximumValue">The maximum value.</param>
        public RandomIntegerPredictableDataStore(int capacity, int maximumValue)
            : base(capacity, () => DataGenerator.RandomInteger(maximumValue))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomIntegerPredictableDataStore"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        public RandomIntegerPredictableDataStore(int capacity, int minimumValue, int maximumValue)
            : base(capacity, () => DataGenerator.RandomInteger(minimumValue, maximumValue))
        {
        }
    }
}