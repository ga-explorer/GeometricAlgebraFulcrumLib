using System;
using System.Collections;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ReadOnlyLists
{
    public sealed record GaReadOnlyListSingleValue<T>
        : IReadOnlyList<T>
    {
        public int Index { get; }

        public T Value { get; }

        public T DefaultValue { get; }

        public int Count { get; }

        public T this[int index] 
            => index >= 0 && index < Count
                ? (index == Index ? Value : DefaultValue)
                : throw new IndexOutOfRangeException(nameof(index));


        public GaReadOnlyListSingleValue(int count, T value)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Index = 0;
            Count = count;
            Value = value;
            DefaultValue = default;
        }

        public GaReadOnlyListSingleValue(int count, T value, T defaultValue)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Index = 0;
            Count = count;
            Value = value;
            DefaultValue = defaultValue;
        }

        public GaReadOnlyListSingleValue(int count, int index, T value)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Index = index;
            Count = count;
            Value = value;
            DefaultValue = default;
        }

        public GaReadOnlyListSingleValue(int count, int index, T value, T defaultValue)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Index = index;
            Count = count;
            Value = value;
            DefaultValue = defaultValue;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Index; i++)
                yield return DefaultValue;

            yield return Value;

            for (var i = Index + 1; i < Count; i++)
                yield return DefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}