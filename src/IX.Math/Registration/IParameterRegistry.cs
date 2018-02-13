// <copyright file="IParameterRegistry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Linq.Expressions;

namespace IX.Math.Registration
{
    /// <summary>
    /// A service contract for a parameter registry.
    /// </summary>
    public interface IParameterRegistry
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IParameterRegistry"/> is populated.
        /// </summary>
        /// <value><c>true</c> if populated; otherwise, <c>false</c>.</value>
        bool Populated { get; }

        /// <summary>
        /// Checks whether a specific parameter name exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the parameter exists, <c>false</c> otherwise.</returns>
        bool Exists(string name);

        /// <summary>
        /// Dumps all parameters.
        /// </summary>
        /// <returns>The existing parameters.</returns>
        ParameterContext[] Dump();

        /// <summary>
        /// Gets the parameter expression.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ParameterExpression.</returns>
        ParameterExpression GetParameterExpression(string name);

        /// <summary>
        /// Advertises a potentially new parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>A parameter context.</returns>
        ParameterContext AdvertiseParameter(string name);

        /// <summary>
        /// Clones from a previous, unrelated context.
        /// </summary>
        /// <param name="previousContext">The previous context.</param>
        /// <returns>The new parameter context.</returns>
        ParameterContext CloneFrom(ParameterContext previousContext);
    }
}