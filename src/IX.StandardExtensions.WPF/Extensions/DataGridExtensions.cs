// <copyright file="DataGridExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace IX.StandardExtensions.WPF.Extensions
{
    /// <summary>
    /// Extensions for <see cref="DataGrid"/>.
    /// </summary>
    public static class DataGridExtensions
    {
        /// <summary>
        /// Gets the data grid row at a specific index.
        /// </summary>
        /// <param name="dataGrid">The data grid to operate on.</param>
        /// <param name="index">The index of the row to be retrieved.</param>
        /// <returns>The data grid row, if one exists and the containers have been successfully built. Otherwise, <c>null</c> (<c>Nothing</c> in Visual Basic).</returns>
        public static DataGridRow GetRowFromIndex(this DataGrid dataGrid, int index)
        {
            if (dataGrid == null)
            {
                throw new ArgumentNullException(nameof(dataGrid));
            }

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
        /// <returns>The data grid row, if one exists and the containers have been successfully built. Otherwise, <c>null</c> (<c>Nothing</c> in Visual Basic).</returns>
        /// <exception cref="System.ArgumentNullException">dataGrid</exception>
        public static DataGridRow GetRowFromItem(this DataGrid dataGrid, object item)
        {
            if (dataGrid == null)
            {
                throw new ArgumentNullException(nameof(dataGrid));
            }

            if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return null;
            }

            return dataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
        }
    }
}