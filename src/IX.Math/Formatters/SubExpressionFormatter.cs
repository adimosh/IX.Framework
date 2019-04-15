// <copyright file="SubExpressionFormatter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using IX.StandardExtensions.Contracts;

namespace IX.Math.Formatters
{
    internal static class SubExpressionFormatter
    {
        internal static string Cleanup(string expression)
        {
            Contract.RequiresNotNullPrivate(
                expression,
                nameof(expression));

            return expression.Trim().Replace(
                " ",
                string.Empty);
        }
    }
}