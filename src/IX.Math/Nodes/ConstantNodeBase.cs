// <copyright file="ConstantNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.Nodes
{
    /// <summary>
    /// A base class for constants.
    /// </summary>
    /// <seealso cref="NodeBase" />
    public abstract class ConstantNodeBase : CachedExpressionNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNodeBase"/> class.
        /// </summary>
        internal ConstantNodeBase()
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not this node is actually a constant.
        /// </summary>
        /// <value><see langword="true"/> if the node is a constant, <see langword="false"/> otherwise.</value>
        public override bool IsConstant => true;

        /// <summary>
        /// Distills the value into a usable constant.
        /// </summary>
        /// <returns>A usable constant.</returns>
        public abstract object DistillValue();

        /// <summary>
        /// Simplifies this node, if possible, reflexively returns otherwise.
        /// </summary>
        /// <returns>A reflexive return.</returns>
        public sealed override NodeBase Simplify() => this;
    }
}