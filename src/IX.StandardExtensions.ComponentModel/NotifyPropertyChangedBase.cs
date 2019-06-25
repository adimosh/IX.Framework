// <copyright file="NotifyPropertyChangedBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.ComponentModel;
using System.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    ///     A base class for advertising and notifying on changes of properties.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [PublicAPI]
    public abstract class NotifyPropertyChangedBase : SynchronizationContextInvokerBase, INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyPropertyChangedBase" /> class.
        /// </summary>
        protected NotifyPropertyChangedBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyPropertyChangedBase" /> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected NotifyPropertyChangedBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        ///     Triggered whenever a property has changed its value and its display should be refreshed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Triggers the <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="changedPropertyName">The name of the changed property.</param>
        protected void RaisePropertyChanged(string changedPropertyName)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.PropertyChanged != null)
            {
                this.Invoke(
                    (
                        invoker,
                        propertyName) => invoker.PropertyChanged?.Invoke(
                        invoker,
                        new PropertyChangedEventArgs(propertyName)), this,
                    changedPropertyName);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="PropertyChanged" /> event asynchronously.
        /// </summary>
        /// <param name="changedPropertyName">The name of the changed property.</param>
        protected void PostPropertyChanged(string changedPropertyName)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.PropertyChanged != null)
            {
                this.InvokePost(
                    (
                        invoker,
                        propertyName) => invoker.PropertyChanged?.Invoke(
                        invoker,
                        new PropertyChangedEventArgs(propertyName)), this,
                    changedPropertyName);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="PropertyChanged" /> event synchronously.
        /// </summary>
        /// <param name="changedPropertyName">The name of the changed property.</param>
        protected void SendPropertyChanged(string changedPropertyName)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.PropertyChanged != null)
            {
                this.InvokeSend(
                    (
                        invoker,
                        propertyName) => invoker.PropertyChanged?.Invoke(
                        invoker,
                        new PropertyChangedEventArgs(propertyName)), this,
                    changedPropertyName);
            }
        }
    }
}