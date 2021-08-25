using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.ReadOnly
{
    public sealed record GaReadOnlyListComputedValues<T>
        : IReadOnlyList<T>
    {
        public Func<int, T> ValueFunc { get; }

        public int Count { get; }

        public T this[int index] 
            => index >= 0 && index < Count
                ? ValueFunc(index)
                : throw new IndexOutOfRangeException(nameof(index));


        public GaReadOnlyListComputedValues(int count, [NotNull] Func<int, T> valueFunc)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            ValueFunc = valueFunc;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return Count.MapRange(ValueFunc).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}