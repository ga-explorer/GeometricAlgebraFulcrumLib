using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.ReadOnly
{
    public sealed record GaReadOnlyListRepeatedValue<T>
        : IReadOnlyList<T>
    {
        public T Value { get; }

        public int Count { get; }

        public T this[int index] 
            => index >= 0 && index < Count
                ? Value
                : throw new IndexOutOfRangeException(nameof(index));


        public GaReadOnlyListRepeatedValue(int count, T value)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            Value = value;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return Count.Repeat(Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}