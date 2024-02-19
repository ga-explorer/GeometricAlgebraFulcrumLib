using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists;

public sealed record GaFuLReadOnlyListRepeatedValue<T>
    : IGaFuLReadOnlyList<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaFuLReadOnlyListRepeatedValue<T> Create(T value, int count)
    {
        return new GaFuLReadOnlyListRepeatedValue<T>(count, value);
    }


    public T Value { get; }

    public int SparseCount 
        => Count;

    public int Count { get; }

    public T this[int index] 
        => index >= 0 && index < Count
            ? Value
            : throw new IndexOutOfRangeException(nameof(index));

    public T this[ulong index] 
        => index < (ulong) Count
            ? Value
            : throw new IndexOutOfRangeException(nameof(index));


    private GaFuLReadOnlyListRepeatedValue(int count, T value)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
        Value = value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return Count == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ulong> GetIndices()
    {
        return ((ulong) Count).GetRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetValues()
    {
        return Enumerable.Repeat(Value, Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(ulong index)
    {
        return index < (ulong) Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong index, out T value)
    {
        if (index < (ulong) Count)
        {
            value = Value;
            return true;
        }

        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, T>> GetIndexValuePairs()
    {
        return ((ulong) Count).GetRange().Select(index => 
            new KeyValuePair<ulong, T>(index, Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return Count.Repeat(Value).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}