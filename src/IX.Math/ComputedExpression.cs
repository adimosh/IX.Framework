// <copyright file="ComputedExpression.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IX.Math.Formatters;
using IX.Math.Nodes;

namespace IX.Math
{
    /// <summary>
    /// A representation of a computed expression, resulting from a string expression.
    /// </summary>
    public class ComputedExpression : IDisposable
    {
        private readonly string initialExpression;
        private NodeBase body;
        private ParameterNodeBase[] parameters;
        private object locker;
        private Dictionary<int, Delegate> computedBodies;
        private bool disposedValue;

        internal ComputedExpression(string initialExpression, NodeBase body, ParameterNodeBase[] parameters, bool isRecognized)
        {
            this.initialExpression = initialExpression;
            this.body = body;
            this.RecognizedCorrectly = isRecognized;
            this.locker = new object();
            this.computedBodies = new Dictionary<int, Delegate>();
            this.parameters = parameters;
            this.ParameterNames = parameters?.Select(p => p.Name).ToArray() ?? new string[0];
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ComputedExpression"/> class.
        /// </summary>
        ~ComputedExpression()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether or not the expression was actually recognized. <c>true</c> can possibly return an actual expression or a static value.
        /// </summary>
        public bool RecognizedCorrectly { get; private set; }

        /// <summary>
        /// Gets the names of the parameters this expression requires, if any.
        /// </summary>
        public string[] ParameterNames { get; private set; }

        /// <summary>
        /// Disposes an instance of the <see cref="ComputedExpression"/> class.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Computes the expression and returns a result.
        /// </summary>
        /// <param name="arguments">The arguments with which to invoke the execution of the expression.</param>
        /// <returns>The computed result, or, if the expression is not recognized correctly, the expression as a <see cref="string"/>.</returns>
        public object Compute(params object[] arguments)
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(ComputedExpression));
            }

            if (!this.RecognizedCorrectly)
            {
                return this.initialExpression;
            }

            object[] convertedArguments = ParameterFormatter.FormatArgumentsAccordingToParameters(arguments, this.parameters);

            this.body = this.body.RefreshParametersRecursive();

            Delegate del;
            try
            {
                LambdaExpression lambda = Expression.Lambda(
                    this.body.GenerateExpression(),
                    this.parameters.Select(p => (ParameterExpression)p.GenerateExpression()));

                if (lambda == null)
                {
                    del = null;
                }
                else
                {
                    del = lambda?.Compile();
                }
            }
            catch
            {
                del = null;
            }

            if (del == null)
            {
                return this.initialExpression;
            }

            try
            {
                return del.DynamicInvoke(convertedArguments);
            }
            catch
            {
                return this.initialExpression;
            }
        }

        /// <summary>
        /// Computes the expression and returns a result.
        /// </summary>
        /// <param name="dataFinder">The data finder for the arguments with which to invoke execution of the expression.</param>
        /// <returns>The computed result, or, if the expression is not recognized correctly, the expression as a <see cref="string"/>.</returns>
        public object Compute(IDataFinder dataFinder)
        {
            if (this.disposedValue)
            {
                throw new ObjectDisposedException(nameof(ComputedExpression));
            }

            if (!this.RecognizedCorrectly)
            {
                return this.initialExpression;
            }

            var pars = new List<object>();

            foreach (ParameterNodeBase p in this.parameters)
            {
                if (!dataFinder.TryGetData(p.Name, out object data))
                {
                    data = null;
                }

                pars.Add(data);
            }

            if (pars.Any(p => p == null))
            {
                return this.initialExpression;
            }

            return this.Compute(pars.ToArray());
        }

        /// <summary>
        /// Disposes an instance of the <see cref="ComputedExpression"/> class.
        /// </summary>
        /// <param name="disposing">Indicates whether or not disposal is a result of a normal dispose usage.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    lock (this.locker)
                    {
                        this.computedBodies.Clear();
                    }
                }

                this.computedBodies = null;
                this.body = null;
                this.parameters = null;

                this.disposedValue = true;
            }
        }
    }
}