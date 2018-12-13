// <copyright file="ViewModelBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Threading;

namespace IX.StandardExtensions.WPF
{
    /// <summary>
    /// A base class for view models.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.ViewModelBase" />
    public class ViewModelBase : ComponentModel.ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        protected ViewModelBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected ViewModelBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Gets a value indicating whether this view model is in design mode.
        /// </summary>
        /// <value><see langword="true" /> if this view model is in design mode; otherwise, <see langword="false"/>.</value>
        [global::System.ComponentModel.Browsable(false)]
        public bool IsInDesignMode => DesignMode.IsInDesignMode;
    }
}