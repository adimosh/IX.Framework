// <copyright file="TernaryFunctionNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for a function that takes three parameters.
    /// </summary>
    /// <seealso cref="FunctionNodeBase" />
    public abstract class TernaryFunctionNodeBase : FunctionNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TernaryFunctionNodeBase" /> class.
        /// </summary>
        /// <param name="firstParameter">The first parameter.</param>
        /// <param name="secondParameter">The second parameter.</param>
        /// <param name="thirdParameter">The third parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="firstParameter" />
        /// or
        /// <paramref name="secondParameter" />
        /// or
        /// <paramref name="thirdParameter" />
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        protected TernaryFunctionNodeBase(NodeBase firstParameter, NodeBase secondParameter, NodeBase thirdParameter)
        {
            this.FirstParameter = firstParameter ?? throw new ArgumentNullException(nameof(firstParameter));
            this.SecondParameter = secondParameter ?? throw new ArgumentNullException(nameof(secondParameter));
            this.ThirdParameter = thirdParameter ?? throw new ArgumentNullException(nameof(thirdParameter));
        }

        /// <summary>
        /// Gets or sets the first parameter.
        /// </summary>
        /// <value>The first parameter.</value>
        public NodeBase FirstParameter { get; protected set; }

        /// <summary>
        /// Gets or sets the second parameter.
        /// </summary>
        /// <value>The second parameter.</value>
        public NodeBase SecondParameter { get; protected set; }

        /// <summary>
        /// Gets or sets the third parameter.
        /// </summary>
        /// <value>The third parameter.</value>
        public NodeBase ThirdParameter { get; protected set; }

        /// <summary>
        /// Refreshes all the parameters recursively.
        /// </summary>
        /// <returns>A reference to the same conceptual node, but possibly a different instance.</returns>
        public override NodeBase RefreshParametersRecursive()
        {
            this.FirstParameter = this.FirstParameter.RefreshParametersRecursive();
            this.SecondParameter = this.SecondParameter.RefreshParametersRecursive();
            this.ThirdParameter = this.ThirdParameter.RefreshParametersRecursive();

            return this;
        }
    }
}