// <copyright file="ParametersGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.Math.Nodes;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Generators
{
    internal static class ParametersGenerator
    {
        public static void GenerateParameter(IDictionary<string, ParameterNodeBase> parameters, string name)
        {
            if (!parameters.ContainsKey(name))
            {
                parameters.Add(name, new UndefinedParameterNode(name, parameters));
            }
        }

        public static NumericParameterNode DetermineNumeric(IDictionary<string, ParameterNodeBase> parameters, string name, bool? determineFloat)
        {
            if (parameters.TryGetValue(name, out var p))
            {
                if (p is UndefinedParameterNode)
                {
                    var result = new NumericParameterNode(name)
                    {
                        RequireFloat = determineFloat,
                    };
                    parameters[name] = result;
                    return result;
                }
                else if (!(p is NumericParameterNode))
                {
                    throw new InvalidCastException();
                }
                else
                {
                    var n = (NumericParameterNode)p;
                    if (determineFloat == false)
                    {
                        n.ParameterMustBeInteger();
                    }
                    else if (determineFloat == true)
                    {
                        n.ParameterMustBeFloat();
                    }

                    return n;
                }
            }
            else
            {
                var result = new NumericParameterNode(name);
                parameters.Add(name, result);
                return result;
            }
        }

        public static BoolParameterNode DetermineBool(IDictionary<string, ParameterNodeBase> parameters, string name)
        {
            if (parameters.TryGetValue(name, out var p))
            {
                if (p is UndefinedParameterNode)
                {
                    var result = new BoolParameterNode(name);
                    parameters[name] = result;
                    return result;
                }
                else if (!(p is BoolParameterNode))
                {
                    throw new InvalidCastException();
                }
                else
                {
                    return (BoolParameterNode)p;
                }
            }
            else
            {
                var result = new BoolParameterNode(name);
                parameters.Add(name, result);
                return result;
            }
        }

        public static ByteArrayParameterNode DetermineByteArray(IDictionary<string, ParameterNodeBase> parameters, string name)
        {
            var trueName = name.ToLower();

            if (parameters.TryGetValue(trueName, out var p))
            {
                if (p is UndefinedParameterNode)
                {
                    var result = new ByteArrayParameterNode(trueName);
                    parameters[trueName] = result;
                    return result;
                }
                else if (!(p is ByteArrayParameterNode))
                {
                    throw new InvalidCastException();
                }
                else
                {
                    return (ByteArrayParameterNode)p;
                }
            }
            else
            {
                var result = new ByteArrayParameterNode(trueName);
                parameters.Add(trueName, result);
                return result;
            }
        }

        public static StringParameterNode DetermineString(IDictionary<string, ParameterNodeBase> parameters, string name)
        {
            if (parameters.TryGetValue(name, out var p))
            {
                if (p is UndefinedParameterNode)
                {
                    var result = new StringParameterNode(name);
                    parameters[name] = result;
                    return result;
                }
                else if (!(p is StringParameterNode))
                {
                    throw new InvalidCastException();
                }
                else
                {
                    return (StringParameterNode)p;
                }
            }
            else
            {
                var result = new StringParameterNode(name);
                parameters.Add(name, result);
                return result;
            }
        }
    }
}