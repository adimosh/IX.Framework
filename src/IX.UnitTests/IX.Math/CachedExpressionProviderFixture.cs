// <copyright file="CachedExpressionProviderFixture.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Math;

namespace IX.UnitTests.IX.Math
{
    /// <summary>
    ///     A fixture for a cached expression provider test suite.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class CachedExpressionProviderFixture : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CachedExpressionProviderFixture" /> class.
        /// </summary>
        public CachedExpressionProviderFixture()
        {
            this.Service = new CachedExpressionParsingService();
        }

        /// <summary>
        ///     Gets the service.
        /// </summary>
        /// <value>The service.</value>
        public CachedExpressionParsingService Service { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => this.Service.Dispose();
    }
}