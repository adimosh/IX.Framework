// <copyright file="MathDefinition.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.Serialization;
using IX.StandardExtensions;

namespace IX.Math
{
    /// <summary>
    /// A definition for signs and symbols used in expression parsing of a mathematical expression.
    /// </summary>
    [DataContract]
    public class MathDefinition : IDeepCloneable<MathDefinition>
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
            this.Parentheses = new Tuple<string, string>(definition.Parentheses.Item1, definition.Parentheses.Item2);
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
            this.AutoConvertStringFormatSpecifier = definition.AutoConvertStringFormatSpecifier;
            this.OperatorPrecedenceStyle = definition.OperatorPrecedenceStyle;
        }

        /// <summary>
        /// Gets or sets what should be interpreted as parentheses.
        /// </summary>
        /// <value>The parentheses indicators.</value>
        /// <remarks>The first item in the tuple represents the opening parenthesis, whereas the second represents the closing parenthesis.</remarks>
        [DataMember]
        public Tuple<string, string> Parentheses { get; set; }

        /// <summary>
        /// Gets or sets what should be interpreted as special symbols.
        /// </summary>
        /// <value>The special symbol indicators.</value>
        /// <remarks>The first item in the tuple represents the opening of the special symbol marker, whereas the second represents its closing.</remarks>
        [DataMember]
        public Tuple<string, string> SpecialSymbolIndicators { get; set; }

        /// <summary>
        /// Gets or sets what should be interpreted as string markers.
        /// </summary>
        /// <value>The string indicator.</value>
        [DataMember]
        public string StringIndicator { get; set; }

        /// <summary>
        /// Gets or sets what should be interpreted as parameter separators in multi-parameter function calls.
        /// </summary>
        /// <value>The parameter separator.</value>
        [DataMember]
        public string ParameterSeparator { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the addition operation.
        /// </summary>
        /// <value>The add symbol.</value>
        [DataMember]
        public string AddSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the subtraction operation.
        /// </summary>
        /// <value>The subtract symbol.</value>
        [DataMember]
        public string SubtractSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the multiplication operation.
        /// </summary>
        /// <value>The multiply symbol.</value>
        [DataMember]
        public string MultiplySymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the division operation.
        /// </summary>
        /// <value>The divide symbol.</value>
        [DataMember]
        public string DivideSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the power operation.
        /// </summary>
        /// <value>The power symbol.</value>
        [DataMember]
        public string PowerSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;and&quot; logical operation.
        /// </summary>
        /// <value>The and symbol.</value>
        [DataMember]
        public string AndSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;or&quot; logical operation.
        /// </summary>
        /// <value>The or symbol.</value>
        [DataMember]
        public string OrSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;xor&quot; logical operation.
        /// </summary>
        /// <value>The xor symbol.</value>
        [DataMember]
        public string XorSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for the &quot;not&quot; logical operation.
        /// </summary>
        /// <value>The not symbol.</value>
        [DataMember]
        public string NotSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of equality.
        /// </summary>
        /// <value>The equals symbol.</value>
        [DataMember]
        public string EqualsSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of inequality.
        /// </summary>
        /// <value>The not equals symbol.</value>
        [DataMember]
        public string NotEqualsSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of greater than.
        /// </summary>
        /// <value>The greater than symbol.</value>
        [DataMember]
        public string GreaterThanSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of greater than or equal.
        /// </summary>
        /// <value>The greater than or equal symbol.</value>
        [DataMember]
        public string GreaterThanOrEqualSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than.
        /// </summary>
        /// <value>The less than symbol.</value>
        [DataMember]
        public string LessThanSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than or equal.
        /// </summary>
        /// <value>The less than or equal symbol.</value>
        [DataMember]
        public string LessThanOrEqualSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than or equal.
        /// </summary>
        /// <value>The right shift symbol.</value>
        [DataMember]
        public string RightShiftSymbol { get; set; }

        /// <summary>
        /// Gets or sets a symbol for a comparison of less than or equal.
        /// </summary>
        /// <value>The left shift symbol.</value>
        [DataMember]
        public string LeftShiftSymbol { get; set; }

        /// <summary>
        /// Gets or sets the automatic convert string format specifier.
        /// </summary>
        /// <value>The automatic convert string format specifier.</value>
        [DataMember]
        public string AutoConvertStringFormatSpecifier { get; set; }

        /// <summary>
        /// Gets or sets the operator precedence style. Default is mathematical.
        /// </summary>
        /// <value>The operator precedence style.</value>
        [DataMember]
        public OperatorPrecedenceStyle OperatorPrecedenceStyle { get; set; }

        /// <summary>
        /// Creates a deep clone of the source object.
        /// </summary>
        /// <returns>A deep clone.</returns>
        public MathDefinition DeepClone() =>
            new MathDefinition
            {
                AddSymbol = this.AddSymbol,
                AndSymbol = this.AndSymbol,
                DivideSymbol = this.DivideSymbol,
                EqualsSymbol = this.EqualsSymbol,
                GreaterThanOrEqualSymbol = this.GreaterThanOrEqualSymbol,
                GreaterThanSymbol = this.GreaterThanSymbol,
                LeftShiftSymbol = this.LeftShiftSymbol,
                LessThanOrEqualSymbol = this.LessThanOrEqualSymbol,
                LessThanSymbol = this.LessThanSymbol,
                MultiplySymbol = this.MultiplySymbol,
                NotEqualsSymbol = this.NotEqualsSymbol,
                NotSymbol = this.NotSymbol,
                OrSymbol = this.OrSymbol,
                ParameterSeparator = this.ParameterSeparator,
                Parentheses = new Tuple<string, string>(this.Parentheses.Item1, this.Parentheses.Item2),
                PowerSymbol = this.PowerSymbol,
                RightShiftSymbol = this.RightShiftSymbol,
                SpecialSymbolIndicators = new Tuple<string, string>(this.SpecialSymbolIndicators.Item1, this.SpecialSymbolIndicators.Item2),
                StringIndicator = this.StringIndicator,
                SubtractSymbol = this.SubtractSymbol,
                XorSymbol = this.XorSymbol,
                AutoConvertStringFormatSpecifier = this.AutoConvertStringFormatSpecifier,
                OperatorPrecedenceStyle = this.OperatorPrecedenceStyle,
            };
    }
}