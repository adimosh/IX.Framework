// <copyright file="MathDefinition.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.Serialization;

namespace IX.Math
{
    /// <summary>
    /// A definition for signs and symbols used in expression parsing of a mathematical expression.
    /// </summary>
    [DataContract]
    public class MathDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MathDefinition"/> class.
        /// </summary>
        public MathDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathDefinition"/> class.
        /// </summary>
        /// <param name="definition">The definition to use.</param>
        public MathDefinition(MathDefinition definition)
        {
            this.Parantheses = new Tuple<string, string>(definition.Parantheses.Item1, definition.Parantheses.Item2);
            this.SpecialSymbolIndicators = new Tuple<string, string>(definition.SpecialSymbolIndicators.Item1, definition.SpecialSymbolIndicators.Item2);
            this.StringIndicator = definition.StringIndicator;
            this.ParameterSeparator = definition.ParameterSeparator;
            this.AddSymbol = definition.AddSymbol;
            this.AndSymbol = definition.AndSymbol;
            this.DivideSymbol = definition.DivideSymbol;
            this.NotEqualsSymbol = definition.NotEqualsSymbol;
            this.EqualsSymbol = definition.EqualsSymbol;
            this.GreaterThanOrEqualSymbol = definition.GreaterThanOrEqualSymbol;
            this.GreaterThanSymbol = definition.GreaterThanSymbol;
            this.LessThanOrEqualSymbol = definition.LessThanOrEqualSymbol;
            this.LessThanSymbol = definition.LessThanSymbol;
            this.MultiplySymbol = definition.MultiplySymbol;
            this.NotSymbol = definition.NotSymbol;
            this.OrSymbol = definition.OrSymbol;
            this.PowerSymbol = definition.PowerSymbol;
            this.LeftShiftSymbol = definition.LeftShiftSymbol;
            this.RightShiftSymbol = definition.RightShiftSymbol;
            this.SubtractSymbol = definition.SubtractSymbol;
            this.XorSymbol = definition.XorSymbol;
        }

        /// <summary>
        /// Gets or sets what should be interpreted as parantheses.
        /// </summary>
        /// <remarks>
        /// <para>The first item in the tuple represents the opening paranthesis, whereas the second represents the closing paranthesis.</para>
        /// </remarks>
        [DataMember]
        public Tuple<string, string> Parantheses { get; set; }

        /// <summary>
        /// Gets or sets what should be interpreted as special symbols.
        /// </summary>
        /// <remarks>
        /// <para>The first item in the tuple represents the opening of the special symbol marker, whereas the second represents its closing.</para>
        /// </remarks>
        [DataMember]
        public Tuple<string, string> SpecialSymbolIndicators { get; set; }

        /// <summary>
        /// Gets or sets what should be interpreted as string markers.
        /// </summary>
        [DataMember]
        public string StringIndicator { get; set; }

        /// <summary>
        /// Gets or sets what should be interpreted as parameter separators in multi-parameter function calls.
        /// </summary>
        [DataMember]
        public string ParameterSeparator { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the addition operation.
        /// </summary>
        [DataMember]
        public string AddSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the subtraction operation.
        /// </summary>
        [DataMember]
        public string SubtractSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the multiplication operation.
        /// </summary>
        [DataMember]
        public string MultiplySymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the division operation.
        /// </summary>
        [DataMember]
        public string DivideSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the power operation.
        /// </summary>
        [DataMember]
        public string PowerSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;and&quot; logical operation.
        /// </summary>
        [DataMember]
        public string AndSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;or&quot; logical operation.
        /// </summary>
        [DataMember]
        public string OrSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;xor&quot; logical operation.
        /// </summary>
        [DataMember]
        public string XorSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;not&quot; logical operation.
        /// </summary>
        [DataMember]
        public string NotSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of equality.
        /// </summary>
        [DataMember]
        public string EqualsSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of inequality.
        /// </summary>
        [DataMember]
        public string NotEqualsSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of greater than.
        /// </summary>
        [DataMember]
        public string GreaterThanSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of greater than or equal.
        /// </summary>
        [DataMember]
        public string GreaterThanOrEqualSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than.
        /// </summary>
        [DataMember]
        public string LessThanSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than or equal.
        /// </summary>
        [DataMember]
        public string LessThanOrEqualSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than or equal.
        /// </summary>
        [DataMember]
        public string RightShiftSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than or equal.
        /// </summary>
        [DataMember]
        public string LeftShiftSymbol { get; set; }
    }
}