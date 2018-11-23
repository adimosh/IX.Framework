// <copyright file="ConcurrentDictionary{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using IX.StandardExtensions.Debugging;

namespace IX.StandardExtensions.Efficiency
{
    /// <summary>
    /// An efficientized version of the concurrent dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="System.Collections.Concurrent.ConcurrentDictionary{TKey, TValue}" />
    [ComVisible(false)]
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
    [DefaultMember("Item")]
    public partial class ConcurrentDictionary<TKey, TValue> : global::System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>
    {
        [ThreadStatic]
        private static object threadStaticMethods;
        [ThreadStatic]
        private static object threadStaticAddFactory;
        [ThreadStatic]
        private static object threadStaticUpdateFactory;

        private static TValue AddInternal<TState>(TKey key)
        {
            var innerState = (TState)threadStaticMethods;
            var innerAdd = (Func<TKey, TState, TValue>)threadStaticAddFactory;

            return innerAdd(key, innerState);
        }

        private static TValue UpdateInternal<TState>(TKey key, TValue oldValue)
        {
            var innerState = (TState)threadStaticMethods;
            var innerUpdate = (Func<TKey, TValue, TState, TValue>)threadStaticUpdateFactory;

            return innerUpdate(key, oldValue, innerState);
        }

        /// <summary>
        /// Uses the specified functions to add a key/value pair to the <see cref="ConcurrentDictionary{TKey,TValue}" /> if the key does not already exist, or to update a key/value pair in the <see cref="ConcurrentDictionary{TKey,TValue}" /> if the key already exists.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="key">The key to be added or whose value should be updated.</param>
        /// <param name="addValueFactory">The function used to generate a value for an absent key.</param>
        /// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value.</param>
        /// <param name="state">The state object.</param>
        /// <returns>The new value for the key. This will be either be the result of addValueFactory (if the key was absent) or the result of updateValueFactory (if the key was present).</returns>
        public TValue AddOrUpdate<TState>(TKey key, Func<TKey, TState, TValue> addValueFactory, Func<TKey, TValue, TState, TValue> updateValueFactory, TState state)
        {
            threadStaticMethods = state;
            threadStaticAddFactory = addValueFactory;
            threadStaticUpdateFactory = updateValueFactory;

            try
            {
                return this.AddOrUpdate(
                    key,
                    AddInternal<TState>,
                    UpdateInternal<TState>);
            }
            finally
            {
                threadStaticMethods = null;
                threadStaticAddFactory = null;
                threadStaticUpdateFactory = null;
            }
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey,TValue}" /> if the key does not already exist, or updates a key/value pair in the <see cref="ConcurrentDictionary{TKey,TValue}" /> by using the specified function if the key already exists.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="key">The key to be added or whose value should be updated.</param>
        /// <param name="addValue">The value to be added for an absent key.</param>
        /// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value.</param>
        /// <param name="state">The state object.</param>
        /// <returns>The new value for the key. This will be either be addValue (if the key was absent) or the result of updateValueFactory (if the key was present).</returns>
        public TValue AddOrUpdate<TState>(TKey key, TValue addValue, Func<TKey, TValue, TState, TValue> updateValueFactory, TState state)
        {
            threadStaticMethods = state;
            threadStaticUpdateFactory = updateValueFactory;

            try
            {
                return this.AddOrUpdate(
                    key,
                    addValue,
                    UpdateInternal<TState>);
            }
            finally
            {
                threadStaticMethods = null;
                threadStaticAddFactory = null;
                threadStaticUpdateFactory = null;
            }
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey,TValue}" /> by using the specified function, if the key does not already exist.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="valueFactory">The function used to generate a value for the key.</param>
        /// <param name="state">The state object.</param>
        /// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value for the key as returned by valueFactory if the key was not in the dictionary.</returns>
        public TValue GetOrAdd<TState>(TKey key, Func<TKey, TState, TValue> valueFactory, TState state)
        {
            threadStaticMethods = state;
            threadStaticAddFactory = valueFactory;

            try
            {
                return this.GetOrAdd(
                    key,
                    AddInternal<TState>);
            }
            finally
            {
                threadStaticMethods = null;
                threadStaticAddFactory = null;
                threadStaticUpdateFactory = null;
            }
        }
    }
}