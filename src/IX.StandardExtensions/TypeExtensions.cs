// <copyright file="TypeExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Reflection;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for <see cref="Type"/>.
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

        /// <summary>
        /// Gets a method with exact parameters, if one exists.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="name">The name of the method to find.</param>
        /// <param name="parameters">The parameters list, if any.</param>
        /// <returns>A <see cref="MethodInfo"/> object representing the found method, or <c>null</c> (<c>Nothing</c> in Visual Basic), if none is found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="typeInfo"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static MethodInfo GetMethodWithExactParameters(this Type typeInfo, string name, params Type[] parameters)
        {
            MethodInfo mi = null;
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Unavoidable
            foreach (MethodInfo p in (typeInfo ?? throw new ArgumentNullException(nameof(typeInfo))).GetRuntimeMethods())
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
            {
                if (p.Name != name)
                {
                    continue;
                }

                ParameterInfo[] ps = p.GetParameters();
                if ((parameters?.Length ?? 0) != ps.Length)
                {
                    continue;
                }

                if (parameters?.SequenceEqual(ps.Select(q => q.ParameterType)) ?? true)
                {
                    if (mi != null)
                    {
                        throw new InvalidOperationException(Resources.SingleOrDefaultMultipleElements);
                    }
                    else
                    {
                        mi = p;
                    }
                }
            }

            return mi;
        }

        /// <summary>
        /// Gets a method with exact parameters, if one exists.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="name">The name of the method to find.</param>
        /// <param name="parameters">The parameters list, if any.</param>
        /// <returns>A <see cref="MethodInfo"/> object representing the found method, or <c>null</c> (<c>Nothing</c> in Visual Basic), if none is found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="typeInfo"/> is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public static MethodInfo GetMethodWithExactParameters(this Type typeInfo, string name, params TypeInfo[] parameters) => typeInfo.GetMethodWithExactParameters(name, parameters.Select(p => p.AsType()).ToArray());

        /// <summary>
        /// Gets the attribute data by type without version binding.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="typeInfo">The type information.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Boolean.</returns>
        public static bool GetAttributeDataByTypeWithoutVersionBinding<TAttribute, TReturn>(this Type typeInfo, out TReturn value) => typeInfo.GetTypeInfo().GetAttributeDataByTypeWithoutVersionBinding<TAttribute, TReturn>(out value);
    }
}