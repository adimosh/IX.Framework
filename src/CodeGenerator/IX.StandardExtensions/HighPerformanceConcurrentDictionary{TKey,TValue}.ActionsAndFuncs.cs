// <copyright file="HighPerformanceConcurrentDictionary{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using IX.StandardExtensions.Threading;
using IX.System.Threading;

namespace IX.StandardExtensions.HighPerformance.Collections
{
    public partial class HighPerformanceConcurrentDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1>(TKey key, Func<TParam1, TValue> valueGenerator, TParam1 param1)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1>(TKey key, Func<TParam1, TValue> valueGenerator, Action<TValue, TParam1> valueAction, TParam1 param1)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1);
                    return value;
                }

                value = valueGenerator(param1);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2>(TKey key, Func<TParam1, TParam2, TValue> valueGenerator, TParam1 param1, TParam2 param2)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2>(TKey key, Func<TParam1, TParam2, TValue> valueGenerator, Action<TValue, TParam1, TParam2> valueAction, TParam1 param1, TParam2 param2)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2);
                    return value;
                }

                value = valueGenerator(param1, param2);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2, TParam3>(TKey key, Func<TParam1, TParam2, TParam3, TValue> valueGenerator, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2, param3);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2, TParam3>(TKey key, Func<TParam1, TParam2, TParam3, TValue> valueGenerator, Action<TValue, TParam1, TParam2, TParam3> valueAction, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2, param3);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2, param3);
                    return value;
                }

                value = valueGenerator(param1, param2, param3);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2, TParam3, TParam4>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TValue> valueGenerator, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2, TParam3, TParam4>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TValue> valueGenerator, Action<TValue, TParam1, TParam2, TParam3, TParam4> valueAction, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2, param3, param4);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2, param3, param4);
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2, TParam3, TParam4, TParam5>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> valueGenerator, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2, TParam3, TParam4, TParam5>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> valueGenerator, Action<TValue, TParam1, TParam2, TParam3, TParam4, TParam5> valueAction, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2, param3, param4, param5);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2, param3, param4, param5);
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> valueGenerator, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5, param6);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> valueGenerator, Action<TValue, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> valueAction, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2, param3, param4, param5, param6);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2, param3, param4, param5, param6);
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5, param6);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> valueGenerator, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5, param6, param7);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> valueGenerator, Action<TValue, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7> valueAction, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2, param3, param4, param5, param6, param7);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2, param3, param4, param5, param6, param7);
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5, param6, param7);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> valueGenerator, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5, param6, param7, param8);

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <typeparam name="TParam1">The type of parameter to be passed to the invoked method at index 0.</typeparam>
        /// <typeparam name="TParam2">The type of parameter to be passed to the invoked method at index 1.</typeparam>
        /// <typeparam name="TParam3">The type of parameter to be passed to the invoked method at index 2.</typeparam>
        /// <typeparam name="TParam4">The type of parameter to be passed to the invoked method at index 3.</typeparam>
        /// <typeparam name="TParam5">The type of parameter to be passed to the invoked method at index 4.</typeparam>
        /// <typeparam name="TParam6">The type of parameter to be passed to the invoked method at index 5.</typeparam>
        /// <typeparam name="TParam7">The type of parameter to be passed to the invoked method at index 6.</typeparam>
        /// <typeparam name="TParam8">The type of parameter to be passed to the invoked method at index 7.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <param name="param1">A parameter of type <typeparamref name="TParam1" /> to pass to the invoked method at index 0.</param>
        /// <param name="param2">A parameter of type <typeparamref name="TParam2" /> to pass to the invoked method at index 1.</param>
        /// <param name="param3">A parameter of type <typeparamref name="TParam3" /> to pass to the invoked method at index 2.</param>
        /// <param name="param4">A parameter of type <typeparamref name="TParam4" /> to pass to the invoked method at index 3.</param>
        /// <param name="param5">A parameter of type <typeparamref name="TParam5" /> to pass to the invoked method at index 4.</param>
        /// <param name="param6">A parameter of type <typeparamref name="TParam6" /> to pass to the invoked method at index 5.</param>
        /// <param name="param7">A parameter of type <typeparamref name="TParam7" /> to pass to the invoked method at index 6.</param>
        /// <param name="param8">A parameter of type <typeparamref name="TParam8" /> to pass to the invoked method at index 7.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>(TKey key, Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> valueGenerator, Action<TValue, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8> valueAction, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value, param1, param2, param3, param4, param5, param6, param7, param8);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value, param1, param2, param3, param4, param5, param6, param7, param8);
                    return value;
                }

                value = valueGenerator(param1, param2, param3, param4, param5, param6, param7, param8);

                this.items.Add(key, value);

                return value;
            }
        }
    }
}