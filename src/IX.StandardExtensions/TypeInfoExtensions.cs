// <copyright file="TypeInfoExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Reflection;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="TypeInfo"/>.
    /// </summary>
    public static class TypeInfoExtensions
    {
        /// <summary>
        /// Determines whether a type has a public parameterless constructor.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <returns><c>true</c> if there is a parameterless constructor; otherwise, <c>false</c>.</returns>
        public static bool HasPublicParameterlessConstructor(this TypeInfo info) =>
            !info.IsInterface && !info.IsAbstract && !info.IsGenericTypeDefinition && info.DeclaredConstructors.Any(p => !p.IsStatic && p.IsPublic && !p.IsGenericMethodDefinition && (p.GetParameters().Length == 0));

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        public static object Instantiate(this TypeInfo info) => Activator.CreateInstance(info.AsType());

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <param name="parameters">The parameters to pass through to the constructor.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        public static object Instantiate(this TypeInfo info, params object[] parameters) => Activator.CreateInstance(info.AsType(), parameters);
    }
}