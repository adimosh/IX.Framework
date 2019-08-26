// <copyright file="AssemblyExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
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
        /// <summary>
        ///     Gets the types assignable from a specified type from an assembly.
        /// </summary>
        /// <typeparam name="T">The type that all fetched types must be assignable from.</typeparam>
        /// <param name="assembly">The assembly to search.</param>
        /// <returns>An enumeration of types that are assignable from the given type.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<TypeInfo> GetTypesAssignableFrom<T>(this Assembly assembly) =>
            Extensions.AssemblyExtensions.GetTypesAssignableFrom<T>(assembly);

        /// <summary>
        ///     Gets the types assignable from a specified type from an enumeration of assemblies.
        /// </summary>
        /// <typeparam name="T">The type that all fetched types must be assignable from.</typeparam>
        /// <param name="assemblies">The assemblies to search.</param>
        /// <returns>An enumeration of types that are assignable from the given type.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static IEnumerable<TypeInfo> GetTypesAssignableFrom<T>(this IEnumerable<Assembly> assemblies) =>
            Extensions.AssemblyExtensions.GetTypesAssignableFrom<T>(assemblies);
    }
}