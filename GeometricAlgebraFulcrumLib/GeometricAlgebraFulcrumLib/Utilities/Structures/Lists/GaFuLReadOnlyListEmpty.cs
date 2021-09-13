using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists
{
    public sealed record GaFuLReadOnlyListEmpty<T>
        : IGaFuLReadOnlyList<T>
    {
        public static GaFuLReadOnlyListEmpty<T> DefaultList { get; }
            = new GaFuLReadOnlyListEmpty<T>();


        public int Count 
            => 0;

        public T this[int index] 
            => throw new IndexOutOfRangeException(nameof(index));

        public T this[ulong index] 
            => throw new IndexOutOfRangeException(nameof(index));


        private GaFuLReadOnlyListEmpty()
        {
        }


        public int SparseCount 
            => 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return Enumerable.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong index, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, T>> GetIndexValuePairs()
        {
            return Enumerable.Empty<KeyValuePair<ulong, T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}