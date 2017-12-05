// <copyright file="KVP{TKey,TValue}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Diagnostics;

namespace IX.Observable.DebugAide
{
    [DebuggerDisplay("[{Key}] = \"{Value}\"")]
    internal sealed class KVP<TKey, TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TKey Key { get; internal set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TValue Value { get; internal set; }
    }
}