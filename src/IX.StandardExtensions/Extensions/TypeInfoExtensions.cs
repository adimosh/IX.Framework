// <copyright file="TypeInfoExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace IX.StandardExtensions.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="TypeInfo" />.
    /// </summary>
    [PublicAPI]
    public static class TypeInfoExtensions
    {
        /// <summary>
        ///     Determines whether a type has a public parameterless constructor.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <returns><see langword="true" /> if there is a parameterless constructor; otherwise, <see langword="false" />.</returns>
        public static bool HasPublicParameterlessConstructor(this TypeInfo info) =>
            !info.IsInterface && !info.IsAbstract && !info.IsGenericTypeDefinition && info.DeclaredConstructors.Any(
                p => !p.IsStatic && p.IsPublic && !p.IsGenericMethodDefinition && p.GetParameters().Length == 0);

        /// <summary>
        ///     Instantiates an object of the specified type.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        public static object Instantiate(this TypeInfo info) => Activator.CreateInstance(info.AsType());

        /// <summary>
        ///     Instantiates an object of the specified type.
        /// </summary>
        /// <param name="info">The type information.</param>
        /// <param name="parameters">The parameters to pass through to the constructor.</param>
        /// <returns>An instance of the object to instantiate.</returns>
        public static object Instantiate(
            this TypeInfo info,
            params object[] parameters) => Activator.CreateInstance(
            info.AsType(),
            parameters);

        /// <summary>
        ///     Gets a method with exact parameters, if one exists.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="name">The name of the method to find.</param>
        /// <param name="parameters">The parameters list, if any.</param>
        /// <returns>
        ///     A <see cref="MethodInfo" /> object representing the found method, or <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic), if none is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="typeInfo" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public static MethodInfo GetMethodWithExactParameters(
            this TypeInfo typeInfo,
            string name,
            params Type[] parameters) => typeInfo.AsType().GetMethodWithExactParameters(
            name,
            parameters);

        /// <summary>
        ///     Gets a method with exact parameters, if one exists.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="name">The name of the method to find.</param>
        /// <param name="parameters">The parameters list, if any.</param>
        /// <returns>
        ///     A <see cref="MethodInfo" /> object representing the found method, or <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic), if none is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="typeInfo" /> is <see langword="null" /> (
        ///     <see langword="Nothing" /> in Visual Basic).
        /// </exception>
        public static MethodInfo GetMethodWithExactParameters(
            this TypeInfo typeInfo,
            string name,
            params TypeInfo[] parameters) => typeInfo.AsType().GetMethodWithExactParameters(
            name,
            parameters.Select(p => p.AsType()).ToArray());

#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - The delegate is generic too
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator
        /// <summary>
        ///     Gets the attribute data by type without version binding.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Boolean.</returns>
        public static bool GetAttributeDataByTypeWithoutVersionBinding<TAttribute, TReturn>(
            this TypeInfo typeInfo,
            out TReturn value)
        {
            CustomAttributeData attributeData = typeInfo.CustomAttributes.FirstOrDefault(
                attribute => attribute.AttributeType.FullName == typeof(TAttribute).FullName);

            if (attributeData == null)
            {
                value = default;
                return false;
            }

            using (IEnumerator<CustomAttributeTypedArgument> arguments = attributeData.ConstructorArguments
                .Where(p => p.ArgumentType == typeof(TReturn)).GetEnumerator())
            {
                if (arguments.MoveNext())
                {
                    value = (TReturn)arguments.Current.Value;

                    if (!arguments.MoveNext())
                    {
                        return true;
                    }
                }
            }

            if (attributeData.NamedArguments != null)
            {
                using (IEnumerator<CustomAttributeTypedArgument> arguments = attributeData.NamedArguments
                    .Where(p => p.TypedValue.ArgumentType == typeof(TReturn)).Select(p => p.TypedValue).GetEnumerator())
                {
                    if (arguments.MoveNext())
                    {
                        value = (TReturn)arguments.Current.Value;

                        if (!arguments.MoveNext())
                        {
                            return true;
                        }
                    }
                }
            }

            value = default;
            return false;
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
    }
}