// <copyright file="IParameterRegistry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.Math.Nodes;

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
        /// Registers a parameter of a certain value type.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="valueType">The value type of the parameter.</param>
        /// <returns>A parameter node base.</returns>
        ParameterNodeBase RegisterParameter(string name, SupportedValueType valueType = SupportedValueType.Unknown);

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
        ParameterNodeBase[] Dump();
    }
}