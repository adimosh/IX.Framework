// <copyright file="OperatorSequenceGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.StandardExtensions.Contracts;
using IX.System.Collections.Generic;

namespace IX.Math.Generators
{
    internal class OperatorSequenceGenerator
    {
        internal static List<Tuple<int, int, string>> GetOperatorsInOrderInExpression(
            string expression,
            LevelDictionary<string, Type> operators)
        {
            Contract.RequiresNotNullPrivate(
                expression,
                nameof(expression));
            Contract.RequiresNotNullPrivate(
                operators,
                nameof(operators));

            var indexes = new List<Tuple<int, int, string>>();

            foreach (KeyValuePair<int, string[]> level in operators.KeysByLevel)
            {
                foreach (var op in level.Value)
                {
                    var index = 0 - op.Length;

                    restartFindProcess:
                    index = expression.IndexOf(
                        op,
                        index + op.Length,
                        StringComparison.Ordinal);

                    if (index != -1)
                    {
                        indexes.Add(
                            new Tuple<int, int, string>(
                                level.Key,
                                index,
                                op));

                        goto restartFindProcess;
                    }
                }
            }

            return indexes;
        }
    }
}