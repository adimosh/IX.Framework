// <copyright file="NotifyPropertyChangedBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.ComponentModel;
using System.Threading;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// A base class for advertising and notifying on changes of properties.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    public abstract class NotifyPropertyChangedBase : SynchronizationContextInvokerBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyPropertyChangedBase"/> class.
        /// </summary>
        protected NotifyPropertyChangedBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyPropertyChangedBase"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected NotifyPropertyChangedBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Triggered whenever a property has changed its value and its display should be refreshed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggers the <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="changedPropertyName">The name of the changed property.</param>
        protected void RaisePropertyChanged(string changedPropertyName) => this.Invoke(
                (invoker, propertyName) => invoker.PropertyChanged?.Invoke(invoker, new PropertyChangedEventArgs(propertyName)),
                this,
                changedPropertyName);
    }
}