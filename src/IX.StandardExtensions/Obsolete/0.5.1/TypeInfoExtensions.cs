// <copyright file="TypeInfoExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Reflection;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="TypeInfo"/>.
    /// </summary>
    [PublicAPI]
    public static class TypeInfoExtensions
    {
        /// <summary>
        /// Determines whether a type has a public parameterless constructor.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <returns><see langword="true"/> if there is a parameterless constructor; otherwise, <see langword="false"/>.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool HasPublicParameterlessConstructor(this TypeInfo info) =>
            Extensions.TypeInfoExtensions.HasPublicParameterlessConstructor(info);

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static object Instantiate(this TypeInfo info) => Extensions.TypeInfoExtensions.Instantiate(info);

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <param name="parameters">The parameters to pass through to the constructor.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static object Instantiate(
            this TypeInfo info,
            params object[] parameters) => Extensions.TypeInfoExtensions.Instantiate(
            info,
            parameters);

        /// <summary>
        /// Gets a method with exact parameters, if one exists.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="name">The name of the method to find.</param>
        /// <param name="parameters">The parameters list, if any.</param>
        /// <returns>A <see cref="MethodInfo"/> object representing the found method, or <see langword="null"/> (<see langword="Nothing"/> in Visual Basic), if none is found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="typeInfo"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static MethodInfo GetMethodWithExactParameters(
            this TypeInfo typeInfo,
            string name,
            params Type[] parameters) => Extensions.TypeInfoExtensions.GetMethodWithExactParameters(
            typeInfo,
            name,
            parameters);

        /// <summary>
        /// Gets a method with exact parameters, if one exists.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="name">The name of the method to find.</param>
        /// <param name="parameters">The parameters list, if any.</param>
        /// <returns>A <see cref="MethodInfo"/> object representing the found method, or <see langword="null"/> (<see langword="Nothing"/> in Visual Basic), if none is found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="typeInfo"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static MethodInfo GetMethodWithExactParameters(
            this TypeInfo typeInfo,
            string name,
            params TypeInfo[] parameters) => Extensions.TypeInfoExtensions.GetMethodWithExactParameters(
            typeInfo,
            name,
            parameters);

        /// <summary>
        /// Gets the attribute data by type without version binding.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Boolean.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool GetAttributeDataByTypeWithoutVersionBinding<TAttribute, TReturn>(
            this TypeInfo typeInfo,
            out TReturn value) =>
            Extensions.TypeInfoExtensions.GetAttributeDataByTypeWithoutVersionBinding<TAttribute, TReturn>(
                typeInfo,
                out value);
    }
}