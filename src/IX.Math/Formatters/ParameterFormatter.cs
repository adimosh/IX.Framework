// <copyright file="ParameterFormatter.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Math.Nodes;
using IX.Math.Nodes.Parameters;

namespace IX.Math.Formatters
{
    internal static class ParameterFormatter
    {
        internal static object[] FormatArgumentsAccordingToParameters(object[] parameterValues, ParameterNodeBase[] parameters)
        {
            if (parameterValues.Length != parameterValues.Length)
            {
                throw new InvalidOperationException();
            }

            object[] finalValues = new object[parameterValues.Length];

            var i = 0;

            while (i < finalValues.Length)
            {
                switch (parameters[i])
                {
                    case NumericParameterNode n:
                        switch (parameterValues[i])
                        {
                            case string se:
                                if (ParsingFormatter.ParseNumeric(se, out object val))
                                {
                                    if (n.RequireFloat == false)
                                    {
                                        finalValues[i] = Convert.ToInt64(val);
                                    }
                                    else
                                    {
                                        finalValues[i] = Convert.ToDouble(val);
                                    }
                                }
                                else
                                {
                                    throw new InvalidCastException();
                                }

                                break;
                            default:
                                if (n.RequireFloat == false)
                                {
                                    finalValues[i] = Convert.ToInt64(parameterValues[i]);
                                }
                                else
                                {
                                    finalValues[i] = Convert.ToDouble(parameterValues[i]);
                                }

                                break;
                        }

                        break;
                    case StringParameterNode s:
                        finalValues[i] = parameterValues[i].ToString();
                        break;
                    case BoolParameterNode b:
                        switch (parameterValues[i])
                        {
                            case bool be:
                                finalValues[i] = be;
                                break;
                            case byte bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case sbyte bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case short bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case char bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case ushort bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case int bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case uint bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case long bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case ulong bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case float bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case double bbe:
                                finalValues[i] = bbe != 0;
                                break;
                            case string se:
                                if (bool.TryParse(se, out bool bbb3))
                                {
                                    finalValues[i] = bbb3;
                                }
                                else
                                {
                                    throw new InvalidCastException();
                                }

                                break;
                            default:
                                throw new InvalidCastException();
                        }

                        break;
                    case ByteArrayParameterNode bapn:
                        switch (parameterValues[i])
                        {
                            case bool be:
                                finalValues[i] = BitConverter.GetBytes(be);
                                break;
                            case byte bbe:
                                finalValues[i] = new byte[1] { bbe };
                                break;
                            case sbyte bbe:
                                finalValues[i] = new byte[1] { (byte)bbe };
                                break;
                            case short bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case char bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case ushort bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case int bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case uint bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case long bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case ulong bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case float bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case double bbe:
                                finalValues[i] = BitConverter.GetBytes(bbe);
                                break;
                            case byte[] ba:
                                finalValues[i] = ba;
                                break;
                            case string se:
                                if (ParsingFormatter.ParseByteArray(se, out byte[] val))
                                {
                                    finalValues[i] = val;
                                }
                                else
                                {
                                    throw new InvalidCastException();
                                }

                                break;
                            default:
                                throw new InvalidCastException();
                        }

                        break;
                    case UndefinedParameterNode u:
                        switch (parameterValues[i])
                        {
                            case bool be:
                                parameters[i] = u.DetermineBool();
                                break;
                            case byte bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case sbyte bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case short bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case char bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case ushort bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case int bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case uint bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case long bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case ulong bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case float bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case double bbe:
                                parameters[i] = u.DetermineNumeric();
                                break;
                            case byte[] ba:
                                parameters[i] = u.DetermineByteArray();
                                break;
                            default:
                                parameters[i] = u.DetermineString();
                                break;
                        }

                        continue;
                    default:
                        throw new InvalidCastException();
                }

                i++;
            }

            return finalValues;
        }
    }
}