﻿// <copyright file="OperatorSequenceGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IX.System.Collections.Generic;

namespace IX.Math.Generators
{
    internal class OperatorSequenceGenerator
    {
        internal static List<Tuple<int, int, string>> GetOperatorsInOrderInExpression(
            in string expression,
            in LevelDictionary<string, Type> operators)
        {
            var indexes = new List<Tuple<int, int, string>>();

            foreach (var level in operators.KeysByLevel)
            {
                foreach (var op in level.Value)
                {
                    var index = 0 - op.Length;

                    restartFindProcess:
                    index = expression.IndexOf(op, index + op.Length);

                    if (index != -1)
                    {
                        indexes.Add(new Tuple<int, int, string>(level.Key, index, op));

                        goto restartFindProcess;
                    }
                }
            }

            return indexes;
        }
    }
}