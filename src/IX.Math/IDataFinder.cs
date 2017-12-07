// <copyright file="IDataFinder.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math
{
    /// <summary>
    /// A contract for an external service that fetches data based on data keys.
    /// </summary>
    public interface IDataFinder
    {
        /// <summary>
        /// Gets data based on a data key.
        /// </summary>
        /// <param name="dataKey">The data key to search data for.</param>
        /// <param name="data">The data output, if found.</param>
        /// <returns><c>true</c> if data was found, <c>false</c> otherwise.</returns>
        bool TryGetData(string dataKey, out object data);
    }
}