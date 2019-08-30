// <copyright file="DataGridExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Extensions
{
    /// <summary>
    /// Extensions for <see cref="DataGrid"/>.
    /// </summary>
    [PublicAPI]
    public static class DataGridExtensions
    {
        /// <summary>
        /// Gets the data grid row at a specific index.
        /// </summary>
        /// <param name="dataGrid">The data grid to operate on.</param>
        /// <param name="index">The index of the row to be retrieved.</param>
        /// <returns>The data grid row, if one exists and the containers have been successfully built. Otherwise, <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dataGrid"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static DataGridRow GetRowFromIndex(this DataGrid dataGrid, int index)
        {
            Contract.RequiresNotNull(in dataGrid, nameof(dataGrid));

            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }

            return dataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }

        /// <summary>
        /// Gets the data grid row at a specific index.
        /// </summary>
        /// <param name="dataGrid">The data grid to operate on.</param>
        /// <param name="item">The item contained in the row to be retrieved.</param>
        /// <returns>The data grid row, if one exists and the containers have been successfully built. Otherwise, <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dataGrid"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static DataGridRow GetRowFromItem(this DataGrid dataGrid, object item)
        {
            Contract.RequiresNotNull(in dataGrid, nameof(dataGrid));

            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }

            return dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
        }

        /// <summary>
        /// Gets the elected data grid row.
        /// </summary>
        /// <param name="dataGrid">The data grid to operate on.</param>
        /// <returns>The data grid row, if one exists and the containers have been successfully built. Otherwise, <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dataGrid"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static DataGridRow GetSelectedRow(this DataGrid dataGrid) => GetRowFromItem(dataGrid, dataGrid?.SelectedItem);

        /// <summary>
        /// Scrolls the item at a specific index into view.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="index">The item index.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataGrid"/> is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</exception>
        public static void ScrollIntoView(this DataGrid dataGrid, int index) => (dataGrid ?? throw new ArgumentNullException(nameof(dataGrid))).ScrollIntoView(dataGrid.Items[index]);
    }
}