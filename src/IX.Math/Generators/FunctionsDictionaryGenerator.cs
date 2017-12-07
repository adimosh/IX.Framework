// <copyright file="FunctionsDictionaryGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IX.Math.Extensibility;
using IX.Math.Nodes;
using IX.StandardExtensions;

namespace IX.Math.Generators
{
    internal static class FunctionsDictionaryGenerator
    {
        internal static Dictionary<string, Type> GenerateInternalNonaryFunctionsDictionary(IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<NonaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalUnaryFunctionsDictionary(IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<UnaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalBinaryFunctionsDictionary(IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<BinaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalTernaryFunctionsDictionary(IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<TernaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateTypeAssignableFrom<T>(IEnumerable<Assembly> assemblies)
            where T : FunctionNodeBase
        {
            var typeDictionary = new Dictionary<string, Type>();

            assemblies.ForEach(InterpretAssembly);

            void InterpretAssembly(Assembly assembly)
            {
                assembly.DefinedTypes
                    .Where(p => typeof(T).GetTypeInfo().IsAssignableFrom(p))
                    .ForEach(AddToTypeDictionary);

                void AddToTypeDictionary(TypeInfo p)
                {
                    CallableMathematicsFunctionAttribute attr;
                    try
                    {
                        attr = p.GetCustomAttribute<CallableMathematicsFunctionAttribute>();
                    }
                    catch
                    {
                        // We need not do anything special here.
                        return;
                    }

                    if (attr == null)
                    {
                        return;
                    }

                    attr.Names.ForEach(q =>
                    {
                        if (typeDictionary.ContainsKey(q))
                        {
                            return;
                        }

                        typeDictionary.Add(q, p.AsType());
                    });
                }
            }

            return typeDictionary;
        }
    }
}