using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists;

public sealed record GaFuLReadOnlyListComputedValues<T>
    : IGaFuLReadOnlyList<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaFuLReadOnlyListComputedValues<T> Create(int count, Func<ulong, T> valueFunc)
    {
        return new GaFuLReadOnlyListComputedValues<T>(count, valueFunc);
    }


    public Func<ulong, T> ValueFunc { get; }

    public int SparseCount 
        => Count;

    public int Count { get; }

    public T this[int index] 
        => index >= 0 && index < Count
            ? ValueFunc((ulong) index)
            : throw new IndexOutOfRangeException(nameof(index));
        
    public T this[ulong index] 
        => index < (ulong) Count
            ? ValueFunc(index)
            : throw new IndexOutOfRangeException(nameof(index));


    private GaFuLReadOnlyListComputedValues(int count, Func<ulong, T> valueFunc)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        Count = count;
        ValueFunc = valueFunc;
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
        return ((ulong) Count).MapRange(ValueFunc);
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
            value = ValueFunc(index);
            return true;
        }

        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, T>> GetIndexValuePairs()
    {
        return ((ulong) Count).GetRange().Select(index => 
            new KeyValuePair<ulong, T>(index, ValueFunc(index))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return ((ulong) Count).MapRange(ValueFunc).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}