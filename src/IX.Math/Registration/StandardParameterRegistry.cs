// <copyright file="StandardParameterRegistry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Linq.Expressions;
using IX.Observable;

namespace IX.Math.Registration
{
    internal class StandardParameterRegistry : IParameterRegistry
    {
        private readonly ConcurrentObservableDictionary<string, ParameterContext> parameterContexts;

        public StandardParameterRegistry()
        {
            this.parameterContexts = new ConcurrentObservableDictionary<string, ParameterContext>(true)
            {
                HistoryLevels = 0,
            };
        }

        public bool Populated => this.parameterContexts.Count > 0;

        public ParameterContext AdvertiseParameter(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return this.parameterContexts.GetOrAdd(name, (nameL1) => new ParameterContext(nameL1), name);
        }

        public ParameterContext CloneFrom(ParameterContext previousContext)
        {
            if (previousContext == null)
            {
                throw new ArgumentNullException(nameof(previousContext));
            }

            var name = previousContext.Name;
            if (this.parameterContexts.ContainsKey(name))
            {
                throw new InvalidOperationException(string.Format(Resources.ParameterAlreadyAdvertised, name));
            }

            ParameterContext newContext = previousContext.DeepClone();

            this.parameterContexts.Add(name, newContext);

            return newContext;
        }

        public ParameterContext[] Dump() => this.parameterContexts.CopyToArray().Select(p => p.Value).ToArray();

        public bool Exists(string name) => this.parameterContexts.ContainsKey(name);

        public ParameterExpression GetParameterExpression(string name) => throw new NotImplementedException();
    }
}