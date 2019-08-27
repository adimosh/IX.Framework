// <copyright file="TypeExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Reflection;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="Type"/>.
    /// </summary>
    [PublicAPI]
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether a type has a public parameterless constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if there is a parameterless constructor; otherwise, <see langword="false"/>.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static bool HasPublicParameterlessConstructor(this Type type) =>
            Extensions.TypeExtensions.HasPublicParameterlessConstructor(type);

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static object Instantiate(this Type type) => Extensions.TypeExtensions.Instantiate(type);

        /// <summary>
        /// Instantiates an object of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameters">The parameters to pass through to the constructor.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static object Instantiate(
            this Type type,
            params object[] parameters) => Extensions.TypeExtensions.Instantiate(
            type,
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
            this Type typeInfo,
            string name,
            params Type[] parameters) => Extensions.TypeExtensions.GetMethodWithExactParameters(
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
            this Type typeInfo,
            string name,
            params TypeInfo[] parameters) => Extensions.TypeExtensions.GetMethodWithExactParameters(
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
            this Type typeInfo,
            out TReturn value) =>
            Extensions.TypeExtensions.GetAttributeDataByTypeWithoutVersionBinding<TAttribute, TReturn>(
                typeInfo,
                out value);
    }
}