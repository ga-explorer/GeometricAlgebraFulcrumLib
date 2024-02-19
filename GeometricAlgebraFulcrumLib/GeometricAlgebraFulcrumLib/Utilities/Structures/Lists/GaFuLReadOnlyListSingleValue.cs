using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists;

public sealed record GaFuLReadOnlyListSingleValue<T>
    : IGaFuLReadOnlyList<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaFuLReadOnlyListSingleValue<T> Create(T value, ulong index)
    {
        return new GaFuLReadOnlyListSingleValue<T>((int) index + 1, index, value, default);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaFuLReadOnlyListSingleValue<T> Create(T value, ulong index, int count)
    {
        if ((ulong) count <= index) 
            count = (int) index + 1;

        return new GaFuLReadOnlyListSingleValue<T>(count, index, value, default);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GaFuLReadOnlyListSingleValue<T> Create(T value, ulong index, int count, T defaultValue)
    {
        if ((ulong) count <= index) 
            count = (int) index + 1;

        return new GaFuLReadOnlyListSingleValue<T>(count, index, value, defaultValue);
    }


    public ulong Index { get; }

    public T Value { get; }

    public T DefaultValue { get; }

    public int SparseCount 
        => 1;

    public int Count { get; }

    public T this[int index] 
        => index >= 0 && index < Count
            ? ((ulong) index == Index ? Value : DefaultValue)
            : throw new IndexOutOfRangeException(nameof(index));

    public T this[ulong index] 
        => index < (ulong) Count
            ? (index == Index ? Value : DefaultValue)
            : throw new IndexOutOfRangeException(nameof(index));


    //public GaFuLReadOnlyListSingleValue(int count, T value)
    //{
    //    if (count < 0)
    //        throw new ArgumentOutOfRangeException(nameof(count));

    //    Index = 0;
    //    Count = count;
    //    Value = value;
    //    DefaultValue = default;
    //}

    //public GaFuLReadOnlyListSingleValue(int count, T value, T defaultValue)
    //{
    //    if (count < 0)
    //        throw new ArgumentOutOfRangeException(nameof(count));

    //    Index = 0;
    //    Count = count;
    //    Value = value;
    //    DefaultValue = defaultValue;
    //}

    //public GaFuLReadOnlyListSingleValue(int count, int index, T value)
    //{
    //    if (count < 0)
    //        throw new ArgumentOutOfRangeException(nameof(count));

    //    if (index < 0 || index >= count)
    //        throw new ArgumentOutOfRangeException(nameof(index));

    //    Index = index;
    //    Count = count;
    //    Value = value;
    //    DefaultValue = default;
    //}

    private GaFuLReadOnlyListSingleValue(int count, ulong index, T value, T defaultValue)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (index >= (ulong) count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Index = index;
        Count = count;
        Value = value;
        DefaultValue = defaultValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ulong> GetIndices()
    {
        yield return Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetValues()
    {
        yield return Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(ulong index)
    {
        return index == Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong index, out T value)
    {
        if (index == Index)
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
        yield return new KeyValuePair<ulong, T>(Index, Value);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0UL; i < Index; i++)
            yield return DefaultValue;

        yield return Value;

        for (var i = Index + 1; i < (ulong) Count; i++)
            yield return DefaultValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}