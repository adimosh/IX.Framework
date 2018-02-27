// <copyright file="StandardParameterRegistry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Linq.Expressions;
using IX.StandardExtensions.HighPerformance.Collections;

namespace IX.Math.Registration
{
    internal class StandardParameterRegistry : IParameterRegistry
    {
        private readonly HighPerformanceConcurrentDictionary<string, ParameterContext> parameterContexts;
#if DEBUG
        private readonly int id;
        private static int staticId;
#endif

        public StandardParameterRegistry()
        {
#if DEBUG
            this.id = NewId();
#endif

            this.parameterContexts = new HighPerformanceConcurrentDictionary<string, ParameterContext>();
        }

        public bool Populated => this.parameterContexts.Count > 0;

#if DEBUG
        public static int NewId() => ++staticId;
#endif

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
            if (this.parameterContexts.TryGetValue(name, out ParameterContext existingValue))
            {
                if (existingValue.Equals(previousContext))
                {
                    return existingValue;
                }
                else
                {
                    throw new InvalidOperationException(string.Format(Resources.ParameterAlreadyAdvertised, name));
                }
            }
            else
            {
                ParameterContext newContext = previousContext.DeepClone();

                this.parameterContexts.Add(name, newContext);

                return newContext;
            }
        }

        public ParameterContext[] Dump() => this.parameterContexts.CopyToArray().Select(p => p.Value).ToArray();

        public bool Exists(string name) => this.parameterContexts.ContainsKey(name);
    }
}