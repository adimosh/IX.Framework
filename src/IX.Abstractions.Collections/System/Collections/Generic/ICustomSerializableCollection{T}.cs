// <copyright file="ICustomSerializableCollection{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    /// An interface that is used for hiding the public list of custom serializable collections.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    [PublicAPI]
    public interface ICustomSerializableCollection<T>
    {
        /// <summary>
        /// Gets or sets the internal container.
        /// </summary>
        /// <value>The internal container.</value>
        List<T> InternalContainer { get; set; }
    }
}