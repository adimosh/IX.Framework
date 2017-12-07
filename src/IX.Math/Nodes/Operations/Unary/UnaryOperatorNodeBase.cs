// <copyright file="UnaryOperatorNodeBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;

namespace IX.Math.Nodes.Operations.Unary
{
    internal abstract class UnaryOperatorNodeBase : OperationNodeBase
    {
        protected UnaryOperatorNodeBase(NodeBase operand)
        {
            this.Operand = operand ?? throw new ArgumentNullException(nameof(operand));
        }

        public NodeBase Operand { get; private set; }

        public override NodeBase RefreshParametersRecursive()
        {
            this.Operand = this.Operand.RefreshParametersRecursive();

            return this;
        }
    }
}