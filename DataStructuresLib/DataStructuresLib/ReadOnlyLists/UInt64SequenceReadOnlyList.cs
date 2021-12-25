using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.ReadOnlyLists
{
    public sealed class UInt64SequenceReadOnlyList
        : IReadOnlyList<ulong>
    {
        public ulong Start { get; }

        public int Count { get; }

        public ulong this[int index] 
            => (ulong)index + Start;


        public UInt64SequenceReadOnlyList(int count, ulong start)
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
                .Select(i => (ulong)i + Start)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}