// <copyright file="OperatorPrecedenceStyle.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Runtime.Serialization;

namespace IX.Math
{
    /// <summary>
    /// Defines precedence styles in the order of operations.
    /// </summary>
    [DataContract]
    public enum OperatorPrecedenceStyle
    {
        /// <summary>
        /// Mathematical precedence: comparison and equation, logical operators, add and subtract, multiply and divide, power, byte shift
        /// </summary>
        [EnumMember]
        Mathematical = 0,

        /// <summary>
        /// C-style precedence: comparison and equation, or, and, xor, add and subtract, multiply and divide, power, byte shift
        /// </summary>
        [EnumMember]
        CStyle = 1,
    }
}