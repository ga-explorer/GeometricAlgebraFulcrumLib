using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.ReadOnlyLists
{
    public sealed class UInt64SequenceReadOnlyList
        : IReadOnlyList<ulong>
    {
        public UInt64 Start { get; }

        public int Count { get; }

        public UInt64 this[int index] 
            => (UInt64)index + Start;


        public UInt64SequenceReadOnlyList(int count, UInt64 start)
        {
            if (count < 0) 
                throw new InvalidOperationException();

            Start = start;
            Count = count;
        }


        public IEnumerator<ulong> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(i => (UInt64)i + Start)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}