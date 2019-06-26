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

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is unavoidable here
        /// <summary>
        ///     Sends a notification to all the viewers of an observable collection inheriting this base class that their view
        ///     should be refreshed.
        /// </summary>
        /// <remarks>
        ///     This method is not affected by a notification suppression context, which means that it will send notifications even
        ///     if there currently is an ambient notification suppression context.
        /// </remarks>
        public void RefreshViewers()
        {
            if (this.CollectionChanged != null)
            {
                this.Invoke(this.CollectionResetInvocationMethod);
            }
        }

        /// <summary>
        ///     Asynchronously sends a notification to all the viewers of an observable collection inheriting this base class
        ///     that their view should be refreshed.
        /// </summary>
        /// <remarks>
        ///     This method is not affected by a notification suppression context, which means that it will send notifications even
        ///     if there currently is an ambient notification suppression context.
        /// </remarks>
        public void RefreshViewersAsynchronously()
        {
            if (this.CollectionChanged != null)
            {
                this.InvokePost(this.CollectionResetInvocationMethod);
            }
        }

        /// <summary>
        ///     Synchronously sends a notification to all the viewers of an observable collection inheriting this base class
        ///     that their view should be refreshed.
        /// </summary>
        /// <remarks>
        ///     This method is not affected by a notification suppression context, which means that it will send notifications even
        ///     if there currently is an ambient notification suppression context.
        /// </remarks>
        public void RefreshViewersSynchronously()
        {
            if (this.CollectionChanged != null)
            {
                this.InvokeSend(this.CollectionResetInvocationMethod);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection reset event.
        /// </summary>
        protected void RaiseCollectionReset()
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.Invoke(this.CollectionResetInvocationMethod);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection reset event asynchronously.
        /// </summary>
        protected void PostCollectionReset()
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(this.CollectionResetInvocationMethod);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection reset event synchronously.
        /// </summary>
        protected void SendCollectionReset()
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(this.CollectionResetInvocationMethod);
            }
        }

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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionAddItemInvocationMethod,
                    index,
                    item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection addition event of one element asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was added.</param>
        /// <param name="item">The item that was added.</param>
        protected void PostCollectionAdd<T>(
            int index,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionAddItemInvocationMethod,
                    index,
                    item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection addition event of one element synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was added.</param>
        /// <param name="item">The item that was added.</param>
        protected void SendCollectionAdd<T>(
            int index,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionAddItemInvocationMethod,
                    index,
                    item);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionAddItemsInvocationMethod,
                    index,
                    items);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection addition event of multiple elements
        ///     asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were added.</param>
        /// <param name="items">The items that were added.</param>
        protected void PostCollectionAdd<T>(
            int index,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionAddItemsInvocationMethod,
                    index,
                    items);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection addition event of multiple elements
        ///     synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were added.</param>
        /// <param name="items">The items that were added.</param>
        protected void SendCollectionAdd<T>(
            int index,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionAddItemsInvocationMethod,
                    index,
                    items);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionRemoveItemInvocationMethod,
                    index,
                    item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection removal event of one element asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was removed.</param>
        /// <param name="item">The item that was added.</param>
        protected void PostCollectionRemove<T>(
            int index,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionRemoveItemInvocationMethod,
                    index,
                    item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection removal event of one element synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was removed.</param>
        /// <param name="item">The item that was added.</param>
        protected void SendCollectionRemove<T>(
            int index,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionRemoveItemInvocationMethod,
                    index,
                    item);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionRemoveItemsInvocationMethod,
                    index,
                    items);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection removal event of multiple elements
        ///     asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were removed.</param>
        /// <param name="items">The items that were removed.</param>
        protected void PostCollectionRemove<T>(
            int index,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionRemoveItemsInvocationMethod,
                    index,
                    items);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection removal event of multiple elements
        ///     synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were removed.</param>
        /// <param name="items">The items that were removed.</param>
        protected void SendCollectionRemove<T>(
            int index,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionRemoveItemsInvocationMethod,
                    index,
                    items);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionMoveItemInvocationMethod,
                    oldIndex,
                    newIndex,
                    item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection move event of one element asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="oldIndex">The index from which the item was moved.</param>
        /// <param name="newIndex">The index at which the item was moved.</param>
        /// <param name="item">The item that was added.</param>
        protected void PostCollectionMove<T>(
            int oldIndex,
            int newIndex,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionMoveItemInvocationMethod,
                    oldIndex,
                    newIndex,
                    item);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection move event of one element synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="oldIndex">The index from which the item was moved.</param>
        /// <param name="newIndex">The index at which the item was moved.</param>
        /// <param name="item">The item that was added.</param>
        protected void SendCollectionMove<T>(
            int oldIndex,
            int newIndex,
            T item)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionMoveItemInvocationMethod,
                    oldIndex,
                    newIndex,
                    item);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionMoveItemsInvocationMethod,
                    oldIndex,
                    newIndex,
                    items);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection move event of multiple elements asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="oldIndex">The index from which the items were moved.</param>
        /// <param name="newIndex">The index at which the items were moved.</param>
        /// <param name="items">The items that were added.</param>
        protected void PostCollectionMove<T>(
            int oldIndex,
            int newIndex,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionMoveItemsInvocationMethod,
                    oldIndex,
                    newIndex,
                    items);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection move event of multiple elements synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="oldIndex">The index from which the items were moved.</param>
        /// <param name="newIndex">The index at which the items were moved.</param>
        /// <param name="items">The items that were added.</param>
        protected void SendCollectionMove<T>(
            int oldIndex,
            int newIndex,
            IEnumerable<T> items)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionMoveItemsInvocationMethod,
                    oldIndex,
                    newIndex,
                    items);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionReplaceItemInvocationMethod,
                    index,
                    oldItem,
                    newItem);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection replacement event of one element
        ///     asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was added.</param>
        /// <param name="oldItem">The original item.</param>
        /// <param name="newItem">The new item.</param>
        protected void PostCollectionReplace<T>(
            int index,
            T oldItem,
            T newItem)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionReplaceItemInvocationMethod,
                    index,
                    oldItem,
                    newItem);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection replacement event of one element synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the item was added.</param>
        /// <param name="oldItem">The original item.</param>
        /// <param name="newItem">The new item.</param>
        protected void SendCollectionReplace<T>(
            int index,
            T oldItem,
            T newItem)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionReplaceItemInvocationMethod,
                    index,
                    oldItem,
                    newItem);
            }
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

            if (this.CollectionChanged != null)
            {
                this.Invoke(
                    this.CollectionReplaceItemsInvocationMethod,
                    index,
                    oldItems,
                    newItems);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection replacement event of multiple elements
        ///     asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were added.</param>
        /// <param name="oldItems">The original items.</param>
        /// <param name="newItems">The new items.</param>
        protected void PostCollectionReplace<T>(
            int index,
            IEnumerable<T> oldItems,
            IEnumerable<T> newItems)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokePost(
                    this.CollectionReplaceItemsInvocationMethod,
                    index,
                    oldItems,
                    newItems);
            }
        }

        /// <summary>
        ///     Triggers the <see cref="CollectionChanged" /> event as a collection replacement event of multiple elements
        ///     synchronously.
        /// </summary>
        /// <typeparam name="T">The type of the item of the collection.</typeparam>
        /// <param name="index">The index at which the items were added.</param>
        /// <param name="oldItems">The original items.</param>
        /// <param name="newItems">The new items.</param>
        protected void SendCollectionReplace<T>(
            int index,
            IEnumerable<T> oldItems,
            IEnumerable<T> newItems)
        {
            if (SuppressNotificationsContext.AmbientSuppressionActive.Value)
            {
                return;
            }

            if (this.CollectionChanged != null)
            {
                this.InvokeSend(
                    this.CollectionReplaceItemsInvocationMethod,
                    index,
                    oldItems,
                    newItems);
            }
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

        private void CollectionResetInvocationMethod() =>
            this.CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation - This is the way event arguments work
        private void CollectionAddItemInvocationMethod<T>(
            int internalIndex,
            T internalItem)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        internalItem,
                        internalIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionAddItemsInvocationMethod<T>(
            int internalIndex,
            IEnumerable<T> internalItems)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        internalItems.ToList(),
                        internalIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionRemoveItemInvocationMethod<T>(
            int internalIndex,
            T internalItem)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove,
                        internalItem,
                        internalIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionRemoveItemsInvocationMethod<T>(
            int internalIndex,
            IEnumerable<T> internalItems)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove,
                        internalItems.ToList(),
                        internalIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionMoveItemInvocationMethod<T>(
            int internalOldIndex,
            int internalNewIndex,
            T internalItem)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Move,
                        internalItem,
                        internalNewIndex,
                        internalOldIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionMoveItemsInvocationMethod<T>(
            int internalOldIndex,
            int internalNewIndex,
            IEnumerable<T> internalItems)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Move,
                        internalItems.ToList(),
                        internalNewIndex,
                        internalOldIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionReplaceItemInvocationMethod<T>(
            int internalIndex,
            T internalOldItem,
            T internalNewItem)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        internalNewItem,
                        internalOldItem,
                        internalIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void CollectionReplaceItemsInvocationMethod<T>(
            int internalIndex,
            IEnumerable<T> internalOldItems,
            IEnumerable<T> internalNewItems)
        {
            try
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        internalNewItems.ToList(),
                        internalOldItems.ToList(),
                        internalIndex));
            }
            catch (Exception) when (EnvironmentSettings.ResetOnCollectionChangeNotificationException)
            {
                this.CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
#pragma warning restore HAA0601 // Value type to reference type conversion causing boxing allocation
    }
}