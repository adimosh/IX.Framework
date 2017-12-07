// <copyright file="ExpressionSymbol.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.ExpressionState
{
    /// <summary>
    /// An expression symbol.
    /// </summary>
    public class ExpressionSymbol
    {
        private string expression;

        private ExpressionSymbol()
        {
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The name.</value>
        public string Expression
        {
            get => this.expression;
            set => this.expression = string.IsNullOrWhiteSpace(value) ? null : value?.Trim();
        }

        /// <summary>
        /// Gets a value indicating whether this symbol represents a function call.
        /// </summary>
        /// <value><c>true</c> if this symbol is a function call; otherwise, <c>false</c>.</value>
        public bool IsFunctionCall { get; private set; }

        /// <summary>
        /// Gets or sets the name of the expression symbol.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of symbols this symbol is contained in.
        /// </summary>
        /// <value>The number of containers.</value>
        public int ContainedIn { get; set; }

        /// <summary>
        /// Gets or sets the number of symbols that this symbol contains.
        /// </summary>
        /// <value>The number of contained symbols.</value>
        public int Contains { get; set; }

        internal static ExpressionSymbol GenerateSymbol(string name, string expression) => new ExpressionSymbol
        {
            Name = name,
            Expression = expression,
        };

        internal static ExpressionSymbol GenerateFunctionCall(string name, string expression)
        {
            ExpressionSymbol generatedExpression = GenerateSymbol(name, expression);
            generatedExpression.IsFunctionCall = true;
            return generatedExpression;
        }
    }
}