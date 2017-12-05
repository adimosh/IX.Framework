// <copyright file="DictionaryDebugView{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IX.Observable.DebugAide
{
    internal sealed class DictionaryDebugView<TKey, TValue>
    {
        private readonly ObservableDictionary<TKey, TValue> dict;

        public DictionaryDebugView(ObservableDictionary<TKey, TValue> dictionary)
        {
            this.dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KVP<TKey, TValue>[] Items
        {
            get
            {
                KeyValuePair<TKey, TValue>[] items = new KeyValuePair<TKey, TValue>[this.dict.InternalContainer.Count];
                ((ICollection<KeyValuePair<TKey, TValue>>)this.dict.InternalContainer).CopyTo(items, 0);
                return items.Select(p => new KVP<TKey, TValue> { Key = p.Key, Value = p.Value }).ToArray();
            }
        }
    }
}