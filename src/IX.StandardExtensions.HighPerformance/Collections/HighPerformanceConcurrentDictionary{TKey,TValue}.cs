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
    /// <summary>
    /// A high-performance concurrent dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="IX.StandardExtensions.Threading.ReaderWriterSynchronizedBase" />
    /// <seealso cref="IDictionary{TKey, TValue}" />
    public partial class HighPerformanceConcurrentDictionary<TKey, TValue> : ReaderWriterSynchronizedBase, IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        public HighPerformanceConcurrentDictionary()
        {
            this.items = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public HighPerformanceConcurrentDictionary(int capacity)
        {
            this.items = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public HighPerformanceConcurrentDictionary(IEqualityComparer<TKey> comparer)
        {
            this.items = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public HighPerformanceConcurrentDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.items = new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public HighPerformanceConcurrentDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.items = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        public HighPerformanceConcurrentDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            this.items = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentDictionary(IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentDictionary(int capacity, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentDictionary(IEqualityComparer<TKey> comparer, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentDictionary(IDictionary<TKey, TValue> dictionary, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentDictionary(int capacity, IEqualityComparer<TKey> comparer, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="locker">The locker.</param>
        public HighPerformanceConcurrentDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, IReaderWriterLock locker)
            : base(locker)
        {
            this.items = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>The keys.</value>
        public ICollection<TKey> Keys => this.ReadLock(() => this.items.Keys);

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>The values.</value>
        public ICollection<TValue> Values => this.ReadLock(() => this.items.Values);

        /// <summary>
        /// Gets the number of elements contained in the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <value>The count.</value>
        public int Count => this.ReadLock(() => this.items.Count);

        /// <summary>
        /// Gets a value indicating whether the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> is read-only. Always returns false.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets or sets the <typeparamref name="TValue"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The contained value.</returns>
        public TValue this[TKey key]
        {
            get => this.ReadLock(keyL1 => this.items[keyL1], key);
            set => this.WriteLock((keyL1, valueL1) => this.items[keyL1] = valueL1, key, value);
        }

        /// <summary>
        /// Determines whether the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.</param>
        /// <returns>true if the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> contains an element with the key; otherwise, false.</returns>
        public bool ContainsKey(TKey key) => this.ReadLock(keyL1 => this.items.ContainsKey(keyL1), key);

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value) => this.WriteLock((keyL1, valueL1) => this.items.Add(keyL1, valueL1), key, value);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.</returns>
        public bool Remove(TKey key) => this.WriteLock(keyL1 => this.Remove(keyL1), key);

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the object that implements <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            using (this.ReadLock())
            {
                return this.items.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.</param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => this.WriteLock(itemL1 => this.items.Add(itemL1.Key, itemL1.Value), item);

        /// <summary>
        /// Removes all items from the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        public void Clear() => this.WriteLock(this.items.Clear);

        /// <summary>
        /// Determines whether the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.</param>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>; otherwise, false.</returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => this.ReadLock(itemL1 => ((ICollection<KeyValuePair<TKey, TValue>>)this.items).Contains(itemL1), item);

        /// <summary>
        /// Copies the elements of the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>. The <see cref="T:System.Array" /> must have
        /// zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this.ReadLock((arrayL1, arrayIndexL1) => ((ICollection<KeyValuePair<TKey, TValue>>)this.items).CopyTo(arrayL1, arrayIndexL1), array, arrayIndex);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.</param>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>; otherwise, false. This method also returns false if <paramref name="item" /> is not found
        /// in the original <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/>.</returns>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => this.WriteLock(itemL1 => ((ICollection<KeyValuePair<TKey, TValue>>)this.items).Remove(itemL1), item);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => new AtomicEnumerator<KeyValuePair<TKey, TValue>>(this.items.GetEnumerator(), this.ReadLock);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Determines whether the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> contains the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the <see cref="HighPerformanceConcurrentDictionary{TKey, TValue}"/> contains the value; otherwise, <c>false</c>.</returns>
        public bool ContainsValue(TValue value) => this.ReadLock(valueL1 => this.items.ContainsValue(valueL1), value);

        /// <summary>
        /// Gets a value from the dictionary, optionally generating one if the key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <returns>The value corresponding to the key, that is guaranteed to exist in the dictionary after this method.</returns>
        /// <remarks>
        /// <para>The <paramref name="valueGenerator" /> method is guaranteed to not be invoked if the key exists.</para>
        /// <para>When the <paramref name="valueGenerator" /> method is invoked, it will be invoked within the write lock. Please ensure that no member of the dictionary is called within it.</para>
        /// </remarks>
        public TValue GetOrAdd(TKey key, Func<TValue> valueGenerator)
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

                value = valueGenerator();

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Creates an item or changes its state, if one exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueGenerator">The value generator.</param>
        /// <param name="valueAction">The value action.</param>
        /// <returns>The created or state-changed item.</returns>
        public TValue CreateOrChangeState(TKey key, Func<TValue> valueGenerator, Action<TValue> valueAction)
        {
            using (ReadWriteSynchronizationLocker locker = this.ReadWriteLock())
            {
                if (this.items.TryGetValue(key, out TValue value))
                {
                    valueAction(value);
                    return value;
                }

                locker.Upgrade();

                if (this.items.TryGetValue(key, out value))
                {
                    valueAction(value);
                    return value;
                }

                value = valueGenerator();

                this.items.Add(key, value);

                return value;
            }
        }

        /// <summary>
        /// Copies all items to array.
        /// </summary>
        /// <returns>A new array.</returns>
        public KeyValuePair<TKey, TValue>[] CopyToArray() => this.ReadLock(() =>
            {
                KeyValuePair<TKey, TValue>[] collection = new KeyValuePair<TKey, TValue>[this.items.Count];

                ((ICollection<KeyValuePair<TKey, TValue>>)this.items).CopyTo(collection, 0);

                return collection;
            });
    }
}