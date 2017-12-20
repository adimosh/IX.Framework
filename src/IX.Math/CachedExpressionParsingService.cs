// <copyright file="CachedExpressionParsingService.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using IX.StandardExtensions.ComponentModel;

namespace IX.Math
{
    /// <summary>
    /// A service that is able to parse strings containing mathematical expressions and solve them in a cached way.
    /// </summary>
    /// <remarks>
    /// <para>This service also caches expressions so that they can be garbage-collected after a specific time period with no use.</para>
    /// </remarks>
    public class CachedExpressionParsingService : DisposableBase, IExpressionParsingService
    {
        private ExpressionParsingService eps;
        private ConcurrentDictionary<string, ComputedExpression> cachedComputedExpressions;

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
        /// Interprets the mathematical expression and returns a container that can be invoked for solving using specific mathematical types.
        /// </summary>
        /// <param name="expression">The expression to interpret.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="ComputedExpression"/> that represents the interpreted expression.</returns>
        public ComputedExpression Interpret(string expression, CancellationToken cancellationToken = default) =>
            this.cachedComputedExpressions.GetOrAdd(expression, expr => this.eps.Interpret(expr, cancellationToken));

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
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            this.cachedComputedExpressions.Clear();
            this.eps.Dispose();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            base.DisposeGeneralContext();

            this.cachedComputedExpressions = null;
            this.eps = null;
        }
    }
}