// <copyright file="IListCloneExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.StandardExtensions
{
    /// <summary>
    /// Extensions for IList.
    /// </summary>
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
        public static List<T> DeepClone<T>(this List<T> list)
            where T : IDeepCloneable<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var clonedList = new List<T>();

            foreach (T item in list)
            {
                clonedList.Add(item.DeepClone());
            }

            return clonedList;
        }

        /// <summary>
        /// Shallow clones all elements of a list into another list.
        /// </summary>
        /// <typeparam name="T">The type of shallow-cloneable item in the list.</typeparam>
        /// <param name="list">The list to act on.</param>
        /// <returns>
        /// A list .
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static List<T> CopyWithShallowClones<T>(this List<T> list)
            where T : IShallowCloneable<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var clonedList = new List<T>();

            foreach (T item in list)
            {
                clonedList.Add(item.ShallowClone());
            }

            return clonedList;
        }
    }
}