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
        internal static MethodInfo GetTypeMethod(this Type type, string name, params Type[] parameters) =>
            type.GetRuntimeMethod(name, parameters);
    }
}