// <copyright file="IListCloneExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for IList.
    /// </summary>
    [PublicAPI]
    public static partial class IListCloneExtensions
    {
        /// <summary>
        /// Deep clones the list.
        /// </summary>
        /// <typeparam name="T">The type of deep-cloneable item in the list.</typeparam>
        /// <param name="list">The list to clone.</param>
        /// <returns>
        /// A cloned list.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static List<T> DeepClone<T>(this List<T> list)
            where T : IDeepCloneable<T> => Extensions.IListCloneExtensions.DeepClone(list);

        /// <summary>
        /// Shallow clones all elements of a list into another list.
        /// </summary>
        /// <typeparam name="T">The type of shallow-cloneable item in the list.</typeparam>
        /// <param name="list">The list to act on.</param>
        /// <returns>
        /// A list .
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        [Obsolete(
            "This method is obsolete and will be removed. Please use the same method in the IX.StandardExtensions.Extensions namespace.")]
        public static List<T> CopyWithShallowClones<T>(this List<T> list)
            where T : IShallowCloneable<T> => Extensions.IListCloneExtensions.CopyWithShallowClones(list);
    }
}