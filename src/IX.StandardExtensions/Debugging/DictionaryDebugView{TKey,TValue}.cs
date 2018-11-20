// <copyright file="DictionaryDebugView{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace IX.StandardExtensions.Debugging
{
    /// <summary>
    /// A debug view for dictionaries. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [ComVisible(false)]
    public sealed class DictionaryDebugView<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dict;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryDebugView{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/>
        /// is <c>null</c> (<c>Nothing</c> in Visual Basic).</exception>
        public DictionaryDebugView(IDictionary<TKey, TValue> dictionary)
        {
            this.dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KyeValuePairDebugView<TKey, TValue>[] Items
        {
            get
            {
                var items = new KeyValuePair<TKey, TValue>[this.dict.Count];
                this.dict.CopyTo(items, 0);
                return items.Select(p => new KyeValuePairDebugView<TKey, TValue> { Key = p.Key, Value = p.Value }).ToArray();
            }
        }
    }
}