// <copyright file="NotifyCollectionChangedInvokerBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    ///     A base class for collections that notify of changes.
    /// </summary>
    /// <seealso cref="IX.StandardExtensions.ComponentModel.NotifyPropertyChangedBase" />
    /// <seealso cref="INotifyCollectionChanged" />
    [PublicAPI]
    public class NotifyCollectionChangedInvokerBase : NotifyPropertyChangedBase, INotifyCollectionChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyCollectionChangedInvokerBase" /> class.
        /// </summary>
        protected NotifyCollectionChangedInvokerBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyCollectionChangedInvokerBase" /> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected NotifyCollectionChangedInvokerBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        ///     Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>Sends a notification to all the viewers of an observable collection inheriting this base class that their view should be refreshed.</summary>
        /// <remarks>
        /// This method is not affected by a notification suppression context, which means that it will send notifications even if there currently is an ambient notification suppression context.
        /// </remarks>
        public void RefreshViewers() =>
            this.Invoke(
                invoker => invoker.CollectionChanged?.Invoke(
                    invoker,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)), this);

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection reset event.
        /// </summary>
        protected void RaiseCollectionReset()
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
                invoker => invoker.CollectionChanged?.Invoke(
                    invoker,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)), this);
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is the way event arguments work
        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection addition event of one element.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was added.</param>
        /// <param name="item">The item that was added.</param>
        protected void RaiseCollectionAdd<T>(
            int index,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalIndex,
                        internalItem) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Add,
                                internalItem,
                                internalIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                index,
                item);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection addition event of multiple elements.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were added.</param>
        /// <param name="items">The items that were added.</param>
        protected void RaiseCollectionAdd<T>(
            int index,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalIndex,
                        internalItems) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Add,
                                internalItems.ToList(),
                                internalIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                index,
                items);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection removal event of one element.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was removed.</param>
        /// <param name="item">The item that was added.</param>
        protected void RaiseCollectionRemove<T>(
            int index,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalIndex,
                        internalItem) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Remove,
                                internalItem,
                                internalIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                index,
                item);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection removal event of multiple elements.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were removed.</param>
        /// <param name="items">The items that were removed.</param>
        protected void RaiseCollectionRemove<T>(
            int index,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalIndex,
                        internalItems) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Remove,
                                internalItems.ToList(),
                                internalIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                index,
                items);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection move event of one element.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="oldIndex">The index from which the item was moved.</param>
        /// <param name="newIndex">The index at which the item was moved.</param>
        /// <param name="item">The item that was added.</param>
        protected void RaiseCollectionMove<T>(
            int oldIndex,
            int newIndex,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalOldIndex,
                        internalNewIndex,
                        internalItem) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Move,
                                internalItem,
                                internalNewIndex,
                                internalOldIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                oldIndex,
                newIndex,
                item);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection move event of multiple elements.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="oldIndex">The index from which the items were moved.</param>
        /// <param name="newIndex">The index at which the items were moved.</param>
        /// <param name="items">The items that were added.</param>
        protected void RaiseCollectionMove<T>(
            int oldIndex,
            int newIndex,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalOldIndex,
                        internalNewIndex,
                        internalItems) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Move,
                                internalItems.ToList(),
                                internalNewIndex,
                                internalOldIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                oldIndex,
                newIndex,
                items);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection replacement event of one element.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was added.</param>
        /// <param name="oldItem">The original item.</param>
        /// <param name="newItem">The new item.</param>
        protected void RaiseCollectionReplace<T>(
            int index,
            T oldItem,
            T newItem)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalIndex,
                        internalOldItem,
                        internalNewItem) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Replace,
                                internalNewItem,
                                internalOldItem,
                                internalIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                index,
                oldItem,
                newItem);
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection replacement event of multiple elements.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were added.</param>
        /// <param name="oldItems">The original items.</param>
        /// <param name="newItems">The new items.</param>
        protected void RaiseCollectionReplace<T>(
            int index,
            IEnumerable<T> oldItems,
            IEnumerable<T> newItems)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            this.Invoke(
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance - Acceptable, as the lambda itself would also translate into a generic
                (
                        invoker,
                        internalIndex,
                        internalOldItems,
                        internalNewItems) =>
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
                {
                    try
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Replace,
                                internalNewItems.ToList(),
                                internalOldItems.ToList(),
                                internalIndex));
                    }
                    catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
                    {
                        invoker.CollectionChanged?.Invoke(
                            invoker,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
#pragma warning disable ERP022 // Catching everything considered harmful. - Catching this particular exception is used specifically in this scenario
                    }
#pragma warning restore ERP022 // Catching everything considered harmful.
                }, this,
                index,
                oldItems,
                newItems);
        }
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
    }
}