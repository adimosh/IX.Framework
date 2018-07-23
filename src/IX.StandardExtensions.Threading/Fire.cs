// <copyright file="Fire.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace IX.StandardExtensions.Threading
{
    /// <summary>
    /// A class that provides methods and extensions to fire events.
    /// </summary>
    public static partial class Fire
    {
        private static void StandardContinuation(Task task, object innerState) => (innerState as Action<Exception>)?.Invoke(task.Exception.GetBaseException());
    }
}