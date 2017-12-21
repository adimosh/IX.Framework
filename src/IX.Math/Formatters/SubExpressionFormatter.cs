// <copyright file="SubExpressionFormatter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

namespace IX.Math.Formatters
{
    internal static class SubExpressionFormatter
    {
        internal static string Cleanup(in string expression) => expression.Trim().Replace(" ", string.Empty);
    }
}