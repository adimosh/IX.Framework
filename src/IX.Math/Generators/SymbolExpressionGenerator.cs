// <copyright file="SymbolExpressionGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using IX.Math.ExpressionState;

namespace IX.Math.Generators
{
    internal static class SymbolExpressionGenerator
    {
        internal static string GenerateSymbolExpression(
            Dictionary<string, ExpressionSymbol> symbolTable,
            Dictionary<string, string> reverseSymbolTable,
            string expression,
            bool isFunction = false)
        {
            if (!reverseSymbolTable.TryGetValue(expression, out string itemName))
            {
                itemName = $"item{symbolTable.Count.ToString().PadLeft(4, '0')}";
                ExpressionSymbol symb;
                if (isFunction)
                {
                    symb = ExpressionSymbol.GenerateFunctionCall(itemName, expression);
                }
                else
                {
                    symb = ExpressionSymbol.GenerateSymbol(itemName, expression);
                }

                symbolTable.Add(itemName, symb);
                reverseSymbolTable.Add(symb.Expression, itemName);
            }

            return itemName;
        }
    }
}