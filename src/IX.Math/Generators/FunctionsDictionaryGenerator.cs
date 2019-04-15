// <copyright file="FunctionsDictionaryGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using IX.Math.Extensibility;
using IX.Math.Nodes;
using IX.StandardExtensions;
using IX.StandardExtensions.Contracts;

namespace IX.Math.Generators
{
    internal static class FunctionsDictionaryGenerator
    {
        internal static Dictionary<string, Type> GenerateInternalNonaryFunctionsDictionary(
            in IEnumerable<Assembly> assemblies) => GenerateTypeAssignableFrom<NonaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalUnaryFunctionsDictionary(
            in IEnumerable<Assembly> assemblies) => GenerateTypeAssignableFrom<UnaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalBinaryFunctionsDictionary(
            in IEnumerable<Assembly> assemblies) => GenerateTypeAssignableFrom<BinaryFunctionNodeBase>(assemblies);

        internal static Dictionary<string, Type> GenerateInternalTernaryFunctionsDictionary(
            in IEnumerable<Assembly> assemblies) => GenerateTypeAssignableFrom<TernaryFunctionNodeBase>(assemblies);

        private static Dictionary<string, Type> GenerateTypeAssignableFrom<T>(IEnumerable<Assembly> assemblies)
            where T : FunctionNodeBase
        {
            Contract.RequiresNotNullPrivate(
                assemblies,
                nameof(assemblies));

            var typeDictionary = new Dictionary<string, Type>();

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable
            assemblies.GetTypesAssignableFrom<T>().ForEach(
                AddToTypeDictionary,
                typeDictionary);
#pragma warning restore HAA0603 // Delegate allocation from a method group

            void AddToTypeDictionary(
                TypeInfo p,
                Dictionary<string, Type> td)
            {
                CallableMathematicsFunctionAttribute attr;
                try
                {
                    attr = p.GetCustomAttribute<CallableMathematicsFunctionAttribute>();
                }
                catch
                {
                    // We need not do anything special here.
#pragma warning disable ERP022 // Catching everything considered harmful. - This is acceptable
                    return;
#pragma warning restore ERP022 // Catching everything considered harmful.
                }

                if (attr == null)
                {
                    return;
                }

                attr.Names.ForEach(
                    (
                        q,
                        p2,
                        td2) =>
                    {
                        if (td2.ContainsKey(q))
                        {
                            return;
                        }

                        td2.Add(
                            q,
                            p2.AsType());
                    }, p,
                    td);
            }

            return typeDictionary;
        }
    }
}