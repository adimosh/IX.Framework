// <copyright file="StandardParameterRegistry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using IX.Math.Nodes;
using IX.Math.Nodes.Parameters;
using IX.Observable;

namespace IX.Math.Registration
{
    internal class StandardParameterRegistry : IParameterRegistry
    {
        private readonly ConcurrentObservableDictionary<string, ParameterNodeBase> parameters;

        public StandardParameterRegistry()
        {
            this.parameters = new ConcurrentObservableDictionary<string, ParameterNodeBase>(true)
            {
                HistoryLevels = 0,
            };
        }

        public bool Populated => this.parameters.Count > 0;

        public ParameterNodeBase[] Dump() => this.parameters.Values.ToArray();

        public bool Exists(string name) => this.parameters.ContainsKey(name);

        public ParameterNodeBase RegisterParameter(string name, SupportedValueType valueType = SupportedValueType.Unknown)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            ParameterNodeBase parameter = this.parameters.GetOrAdd(
                name,
                (nameL1, valueTypeL1) =>
                {
                    ParameterNodeBase pnb;
                    switch (valueTypeL1)
                    {
                        case SupportedValueType.Boolean:
                            pnb = new BoolParameterNode(nameL1);
                            break;
                        case SupportedValueType.ByteArray:
                            pnb = new ByteArrayParameterNode(nameL1);
                            break;
                        case SupportedValueType.Numeric:
                            pnb = new NumericParameterNode(nameL1);
                            break;
                        case SupportedValueType.String:
                            pnb = new StringParameterNode(nameL1);
                            break;
                        default:
                            pnb = new UndefinedParameterNode(nameL1, this);
                            break;
                    }

                    return pnb;
                },
                name,
                valueType);

            if (valueType == SupportedValueType.Unknown)
            {
                return parameter;
            }

            if (parameter is UndefinedParameterNode upn)
            {
                switch (valueType)
                {
                    case SupportedValueType.Boolean:
                        parameter = upn.DetermineBool();
                        break;
                    case SupportedValueType.ByteArray:
                        parameter = upn.DetermineByteArray();
                        break;
                    case SupportedValueType.Numeric:
                        parameter = upn.DetermineNumeric();
                        break;
                    case SupportedValueType.String:
                        parameter = upn.DetermineString();
                        break;
                    default:
                        throw new ExpressionNotValidLogicallyException(string.Format(Resources.ParameterTypeNotRecognized, name));
                }
            }

            if (parameter.ReturnType != valueType)
            {
                throw new ExpressionNotValidLogicallyException(string.Format(Resources.ParameterRequiredOfMismatchedTypes, name));
            }

            return parameter;
        }
    }
}