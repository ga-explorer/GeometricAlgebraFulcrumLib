﻿using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections
{
    public sealed class SparseReadOnlyListUInt64<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyDictionary<ulong, T> _dictionary;

        public T DefaultValue { get; }

        public int Count { get; }

        public T this[int index] 
            => _dictionary.TryGetValue((ulong)index, out var value) ? value : DefaultValue;


        public SparseReadOnlyListUInt64(int count, IReadOnlyDictionary<ulong, T> dictionary)
        {
            Count = count;
            DefaultValue = default;
            _dictionary = dictionary;
        }

        public SparseReadOnlyListUInt64(int count, T defaultValue, IReadOnlyDictionary<ulong, T> dictionary)
        {
            Count = count;
            DefaultValue = defaultValue;
            _dictionary = dictionary;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return this[i];
        }
    }
}