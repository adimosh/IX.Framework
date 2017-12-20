// <copyright file="DependencyObjectExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace IX.StandardExtensions.WPF.Extensions
{
    /// <summary>
    /// Extensions for <see cref="DependencyObject"/>.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Gets the topmost visual parent of a specific type from the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the visual parent to get.</typeparam>
        /// <param name="childObject">The child object.</param>
        /// <returns>A visual object of the specified type, or <c>null</c> (<c>Nothing</c> in Visual Basic) if one does not exist.</returns>
        public static T GetTopmostVisualParent<T>(this DependencyObject childObject)
            where T : DependencyObject
        {
            if (childObject == null)
            {
                return null;
            }

            DependencyObject parent = VisualTreeHelper.GetParent(childObject);

            // Iteratively traverse the visual tree
            while ((parent != null) && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        /// <summary>
        /// Get1sts the first level of level visual children.
        /// </summary>
        /// <typeparam name="T">The type of children to seek.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <returns>A list of visual children.</returns>
        public static List<T> GetFirstLevelVisualChildren<T>(this DependencyObject parent)
            where T : Visual
        {
            var visualCollection = new List<T>();
            GetFirstLevelVisualChildren(parent, visualCollection);
            return visualCollection;
        }

        /// <summary>
        /// Get1sts the first level of visual children in a specific list.
        /// </summary>
        /// <typeparam name="T">The type of children to seek.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="visualCollection">The visual collection.</param>
        /// <remarks>
        /// <para>This method is NOT thread-safe and should not be invoked when changes to the visual children lists are expected.</para>
        /// <para>It is relatively safe to invoke this method by dispatcher. Care must be taken to not invoke via non-exclusive or non-owning dispatcher.</para>
        /// </remarks>
        public static void GetFirstLevelVisualChildren<T>(this DependencyObject parent, List<T> visualCollection)
            where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
            }
        }

        /// <summary>
        /// Gets a visual child of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the visual child to get.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <returns>The visual child, if found, or <c>null</c> otherwise.</returns>
        public static T GetVisualChild<T>(this DependencyObject parent)
            where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T child = null;
            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < numVisuals; i++)
            {
                // Traverse all visual tree and look for a child
                var v = VisualTreeHelper.GetChild(parent, i) as DependencyObject;
                child = v as T;
                if (child == null && v != null)
                {
                    // The child is of the wrong type, let's see if it has children of the right type
                    child = GetVisualChild<T>(v);
                }

                if (child != null)
                {
                    // Child was found, let's stop searching
                    break;
                }
            }

            return child;
        }

        /// <summary>
        /// Gets the main window.
        /// </summary>
        /// <param name="current">The current dependency object.</param>
        /// <returns>The main window.</returns>
        public static Window GetWindow(this DependencyObject current)
        {
            if (current is Window)
            {
                return current as Window;
            }

            return GetTopmostVisualParent<Window>(current);
        }
    }
}