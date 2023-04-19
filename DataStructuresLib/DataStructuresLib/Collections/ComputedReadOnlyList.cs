using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataStructuresLib.Collections
{
    public class ComputedReadOnlyList<T> :
        IReadOnlyList<T>
    {
        public Func<int, T> ItemMapping { get; }

        public int Count { get; }

        public T this[int index]
            => ItemMapping(index);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ComputedReadOnlyList(int count, Func<int, T> itemMapping)
        {
            Count = count;
            ItemMapping = itemMapping;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Range(0, Count).Select(ItemMapping).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}