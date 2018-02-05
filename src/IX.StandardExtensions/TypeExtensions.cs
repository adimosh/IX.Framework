// <copyright file="TypeExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Reflection;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether a type has a public parameterless constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if there is a parameterless constructor; otherwise, <c>false</c>.</returns>
        public static bool HasPublicParameterlessConstructor(this Type type) => type.GetTypeInfo().HasPublicParameterlessConstructor();

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        public static object Instantiate(this Type type) => Activator.CreateInstance(type);

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameters">The parameters to pass through to the constructor.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        public static object Instantiate(this Type type, params object[] parameters) => Activator.CreateInstance(type, parameters);
    }
}