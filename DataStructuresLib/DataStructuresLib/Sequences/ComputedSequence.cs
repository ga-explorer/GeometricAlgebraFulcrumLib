using System;
using System.Runtime.CompilerServices;

namespace DataStructuresLib.Sequences
{
    public class ComputedSequence<T>
    {
        public int Index { get; protected set; }

        public Func<int, T> IndexMapping { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ComputedSequence(Func<int, T> indexMapping)
        {
            Index = 0;
            IndexMapping = indexMapping;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            Index = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            Index = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetNextItem()
        {
            return IndexMapping(Index++);
        }
    }
}
