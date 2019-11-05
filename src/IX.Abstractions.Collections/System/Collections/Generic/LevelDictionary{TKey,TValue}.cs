// <copyright file="LevelDictionary{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using IX.Abstractions.Collections;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace IX.System.Collections.Generic
{
    /// <summary>
    /// A dictionary that saves its objects in multiple levels.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [PublicAPI]
    public class LevelDictionary<TKey, TValue> : DisposableBase, IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> internalDictionary;
        private Dictionary<int, List<TKey>> keyLevels;
        private Dictionary<TKey, int> levelKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelDictionary{TKey, TValue}"/> class.
        /// </summary>
        public LevelDictionary()
        {
            this.internalDictionary = new Dictionary<TKey, TValue>();
            this.keyLevels = new Dictionary<int, List<TKey>>();
            this.levelKeys = new Dictionary<TKey, int>();
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public ICollection<TKey> Keys
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.internalDictionary.Keys;
            }
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>The values.</value>
        public ICollection<TValue> Values
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.internalDictionary.Values;
            }
        }

        /// <summary>
        /// Gets the keys by level.
        /// </summary>
        /// <value>The keys by level.</value>
        public KeyValuePair<int, TKey[]>[] KeysByLevel
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.keyLevels.OrderBy(p => p.Key).Select(p => new KeyValuePair<int, TKey[]>(p.Key, p.Value.ToArray())).ToArray();
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.internalDictionary.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><see langword="true"/> if this instance is read only; otherwise, <see langword="false"/>.</value>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets or sets the <typeparamref name="TValue" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>TValue.</returns>
        public TValue this[TKey key]
        {
            get
            {
                this.ThrowIfCurrentObjectDisposed();

                return this.internalDictionary[key];
            }

            set
            {
                this.ThrowIfCurrentObjectDisposed();

                this.internalDictionary[key] = value;
            }
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="NotImplementedByDesignException">This method is not implemented by design. Do not call it, as it will always throw an exception.</exception>
        void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new NotImplementedByDesignException();

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="NotImplementedByDesignException">This method is not implemented by design. Do not call it, as it will always throw an exception.</exception>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw new NotImplementedByDesignException();

        /// <summary>
        /// Adds the specified key and value to a level in the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="level">The level.</param>
        /// <exception cref="InvalidOperationException">The key was already present in the dictionary.</exception>
        public void Add(TKey key, TValue value, int level)
        {
            if (level < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            if (this.internalDictionary.ContainsKey(key))
            {
                throw new InvalidOperationException(Resources.ErrorKeyFoundInDictionary);
            }

            this.internalDictionary.Add(key, value);

            if (this.keyLevels.TryGetValue(level, out List<TKey> list))
            {
                list.Add(key);
            }
            else
            {
                this.keyLevels.Add(level, new List<TKey> { key });
            }

            this.levelKeys.Add(key, level);
        }

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        public void Clear()
        {
            this.ThrowIfCurrentObjectDisposed();

            this.internalDictionary.Clear();
            this.keyLevels.Clear();
            this.levelKeys.Clear();
        }

        /// <summary>
        /// Determines whether the specified item is contained in the dictionary.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><see langword="true"/> if the dictionary contains the specified item; otherwise, <see langword="false"/>.</returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            this.ThrowIfCurrentObjectDisposed();

            return (this.internalDictionary as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);
        }

        /// <summary>
        /// Determines whether the dictionary contains they key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><see langword="true"/> if the dictionary contains the key; otherwise, <see langword="false"/>.</returns>
        public bool ContainsKey(TKey key)
        {
            this.ThrowIfCurrentObjectDisposed();

            return this.internalDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Copies the contents of the dictionary to an array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.ThrowIfCurrentObjectDisposed();

            (this.internalDictionary as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The dictionary's enumerator.</returns>
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            this.ThrowIfCurrentObjectDisposed();

            return this.internalDictionary.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The dictionary enumerator.</returns>
        [SuppressMessage("Performance", "HAA0601:Value type to reference type conversion causing boxing allocation", Justification = "Unavoidable here.")]
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
            this.GetEnumerator();

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><see langword="true"/> if the key has been removed, <see langword="false"/> otherwise.</returns>
        public bool Remove(TKey key)
        {
            this.ThrowIfCurrentObjectDisposed();

            if (!this.levelKeys.TryGetValue(
                key,
                out var level))
            {
                return false;
            }

            if (!this.internalDictionary.Remove(key))
            {
                return false;
            }

            this.keyLevels[level].Remove(key);
            this.levelKeys.Remove(key);

            return true;
        }

        /// <summary>
        /// Removes the specified item from the dictionary.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><see langword="true"/> if the removal was a success, <see langword="false"/> otherwise.</returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => this.Remove(item.Key);

        /// <summary>
        /// Tries to get a value by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value was found, <see langword="false"/> otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            this.ThrowIfCurrentObjectDisposed();

            return this.internalDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        [SuppressMessage("Performance", "HAA0601:Value type to reference type conversion causing boxing allocation", Justification = "Unavoidable here.")]
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            this.internalDictionary.Clear();
            this.keyLevels.Clear();
            this.levelKeys.Clear();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes the general context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            base.DisposeGeneralContext();

            Interlocked.Exchange(ref this.internalDictionary, null);
            Interlocked.Exchange(ref this.keyLevels, null);
            Interlocked.Exchange(ref this.levelKeys, null);
        }
    }
}