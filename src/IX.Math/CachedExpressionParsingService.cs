// <copyright file="CachedExpressionParsingService.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;

namespace IX.Math
{
    /// <summary>
    /// A service that is able to parse strings containing mathematical expressions and solve them in a cached way.
    /// </summary>
    /// <remarks>
    /// <para>This service also caches expressions so that they can be garbage-collected after a specific time period with no use.</para>
    /// </remarks>
    public class CachedExpressionParsingService : IExpressionParsingService, IDisposable
    {
        private ExpressionParsingService eps;
        private ConcurrentDictionary<string, ComputedExpression> cachedComputedExpressions;
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedExpressionParsingService"/> class.
        /// </summary>
        public CachedExpressionParsingService()
        {
            this.eps = new ExpressionParsingService();
            this.cachedComputedExpressions = new ConcurrentDictionary<string, ComputedExpression>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedExpressionParsingService"/> class.
        /// </summary>
        /// <param name="definition">The math definition to use.</param>
        public CachedExpressionParsingService(MathDefinition definition)
        {
            this.eps = new ExpressionParsingService(definition);
            this.cachedComputedExpressions = new ConcurrentDictionary<string, ComputedExpression>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CachedExpressionParsingService"/> class.
        /// </summary>
        ~CachedExpressionParsingService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(false);
        }

        /// <summary>
        /// Interprets the mathematical expression and returns a container that can be invoked for solving using specific mathematical types.
        /// </summary>
        /// <param name="expression">The expression to interpret.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="ComputedExpression"/> that represents the interpreted expression.</returns>
        public ComputedExpression Interpret(string expression, CancellationToken cancellationToken = default) =>
            this.cachedComputedExpressions.GetOrAdd(expression, expr => this.eps.Interpret(expr, cancellationToken));

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Registers an assembly to extract compatible functions from.
        /// </summary>
        /// <param name="assembly">The assembly to register.</param>
        public void RegisterFunctionsAssembly(Assembly assembly) => this.eps.RegisterFunctionsAssembly(assembly);

        /// <summary>
        /// Returns the prototypes of all registered functions.
        /// </summary>
        /// <returns>All function names, with all possible combinations of input and output data.</returns>
        public string[] GetRegisteredFunctions() => this.eps.GetRegisteredFunctions();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.cachedComputedExpressions.Clear();
                    this.eps.Dispose();
                }

                this.cachedComputedExpressions = null;
                this.eps = null;

                this.disposedValue = true;
            }
        }
    }
}