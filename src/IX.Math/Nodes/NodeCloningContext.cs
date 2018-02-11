// <copyright file="NodeCloningContext.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Math.Registration;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A context for cloning nodes.
    /// </summary>
    public class NodeCloningContext
    {
        /// <summary>
        /// Gets or sets the parameter registry.
        /// </summary>
        /// <value>The parameter registry.</value>
        public IParameterRegistry ParameterRegistry { get; set; }
    }
}