// <copyright file="IShallowCloneable{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Interface for implementing shallow cloning for an object.
    /// </summary>
    /// <typeparam name="T">The type of object to clone.</typeparam>
    [PublicAPI]
    public interface IShallowCloneable<out T>
    {
        /// <summary>
        /// Creates a shallow clone of the source object.
        /// </summary>
        /// <returns>A shallow clone.</returns>
        T ShallowClone();
    }
}