// <copyright file="AssemblyExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    ///     Extensions for <see cref="Assembly" />.
    /// </summary>
    [PublicAPI]
    public static class AssemblyExtensions
    {
#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable for now
        /// <summary>
        ///     Gets the types assignable from a specified type from an assembly.
        /// </summary>
        /// <typeparam name="T">The type that all fetched types must be assignable from.</typeparam>
        /// <param name="assembly">The assembly to search.</param>
        /// <returns>An enumeration of types that are assignable from the given type.</returns>
        public static IEnumerable<TypeInfo> GetTypesAssignableFrom<T>(this Assembly assembly)
        {
            return assembly?.DefinedTypes?.Where(Filter) ?? throw new ArgumentNullException(nameof(assembly));

            bool Filter(TypeInfo p)
            {
                return typeof(T).GetTypeInfo().IsAssignableFrom(p);
            }
        }

        /// <summary>
        ///     Gets the types assignable from a specified type from an enumeration of assemblies.
        /// </summary>
        /// <typeparam name="T">The type that all fetched types must be assignable from.</typeparam>
        /// <param name="assemblies">The assemblies to search.</param>
        /// <returns>An enumeration of types that are assignable from the given type.</returns>
        public static IEnumerable<TypeInfo> GetTypesAssignableFrom<T>(this IEnumerable<Assembly> assemblies)
        {
            return assemblies?.SelectMany(GetAssignableTypes) ?? throw new ArgumentNullException(nameof(assemblies));

            IEnumerable<TypeInfo> GetAssignableTypes(Assembly p)
            {
                return p.GetTypesAssignableFrom<T>();
            }
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group
    }
}