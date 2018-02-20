// <copyright file="IConstantsRegistry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Linq.Expressions;

namespace IX.Math.Registration
{
    /// <summary>
    /// A service contract for a constants registry.
    /// </summary>
    public interface IConstantsRegistry
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConstantsRegistry"/> is populated.
        /// </summary>
        /// <value><c>true</c> if populated; otherwise, <c>false</c>.</value>
        bool Populated { get; }

        /// <summary>
        /// Checks whether a specific parameter name exists.
        /// </summary>
        /// <param name="constant">The constant.</param>
        /// <returns><c>true</c> if the parameter exists, <c>false</c> otherwise.</returns>
        bool Exists(string constant);

        /// <summary>
        /// Dumps all parameters.
        /// </summary>
        /// <returns>The existing parameters.</returns>
        ConstantContext[] Dump();

        /// <summary>
        /// Gets the parameter expression.
        /// </summary>
        /// <param name="constant">The constant.</param>
        /// <returns>A <see cref="ConstantExpression"/> that represents the constant.</returns>
        ConstantExpression GetConstantExpression(string constant);

        /// <summary>
        /// Advertises a potentially new parameter.
        /// </summary>
        /// <param name="constant">The constant.</param>
        /// <returns>A constant context.</returns>
        ConstantContext AdvertiseConstant(string constant);

        /// <summary>
        /// Clones from a previous, unrelated context.
        /// </summary>
        /// <param name="previousContext">The previous context.</param>
        /// <returns>The new constant context.</returns>
        ConstantContext CloneFrom(ConstantContext previousContext);
    }
}