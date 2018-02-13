// <copyright file="FunctionsDictionaryGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using IX.Math.Extensibility;
using IX.Math.Nodes;
using IX.StandardExtensions;

namespace IX.Math.Generators
{
    internal static class FunctionsDictionaryGenerator
    {
        internal static Dictionary<string, Type> GenerateInternalNonaryFunctionsDictionary(in IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<NonaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalUnaryFunctionsDictionary(in IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<UnaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalBinaryFunctionsDictionary(in IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<BinaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalTernaryFunctionsDictionary(in IEnumerable<Assembly> assemblies)
            => GenerateTypeAssignableFrom<TernaryFunctionNodeBase>(assemblies);

        private static Dictionary<string, Type> GenerateTypeAssignableFrom<T>(in IEnumerable<Assembly> assemblies)
            where T : FunctionNodeBase
        {
            var typeDictionary = new Dictionary<string, Type>();

            assemblies.GetTypesAssignableFrom<T>().ForEach(AddToTypeDictionary);

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

            return typeDictionary;
        }
    }
}