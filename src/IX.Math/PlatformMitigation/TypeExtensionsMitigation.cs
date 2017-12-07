// <copyright file="TypeExtensionsMitigation.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Linq;
using System.Reflection;

namespace IX.Math.PlatformMitigation
{
    internal static class TypeExtensionsMitigation
    {
        internal static PropertyInfo GetTypeProperty(this Type type, string name) =>
            type.GetRuntimeProperty(name);

        internal static MethodInfo GetTypeMethod(this Type type, string name) =>
            type.GetRuntimeMethods()
                .Where(p => p.Name == name)
                .OrderBy(p => p.GetParameters().Length)
                .FirstOrDefault();

        internal static MethodInfo GetTypeMethod(this Type type, string name, params Type[] parameters) =>
            type.GetRuntimeMethods().SingleOrDefault(p =>
            {
                if (p.Name != name)
                {
                    return false;
                }

                ParameterInfo[] pars = p.GetParameters();

                if (pars.Length != parameters.Length)
                {
                    return false;
                }

                for (var i = 0; i < parameters.Length; i++)
                {
                    if (pars[i].ParameterType != parameters[i])
                    {
                        return false;
                    }
                }

                return true;
            });

        internal static MethodInfo GetTypeMethod(this Type type, string name, Type returnType, Type[] parameters) =>
            type.GetRuntimeMethods().SingleOrDefault(p =>
            {
                if (p.Name != name || p.ReturnType != returnType)
                {
                    return false;
                }

                ParameterInfo[] pars = p.GetParameters();

                if (pars.Length != parameters.Length)
                {
                    return false;
                }

                for (var i = 0; i < parameters.Length; i++)
                {
                    if (pars[i].ParameterType != parameters[i])
                    {
                        return false;
                    }
                }

                return true;
            });

        internal static ConstructorInfo[] GetTypeConstructors(this Type type) => type.GetTypeInfo().DeclaredConstructors.ToArray();
    }
}