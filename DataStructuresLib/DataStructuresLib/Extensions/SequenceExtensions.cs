using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataStructuresLib.Extensions;

public static class SequenceExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BitwiseXor(this IEnumerable<int> sequence)
    {
        return sequence.Aggregate(0, (acc, value) => acc ^ value);
    }


        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T2> MapItems<T, T2>(this IEnumerable<T> sequence, Func<T, T2> itemMapping)
    {
        return sequence.Select(itemMapping);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T2> MapItems<T, T2>(this IEnumerable<T> sequence, Func<int, T, T2> itemMapping)
    {
        return sequence.Select((item, index) => itemMapping(index, item));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ReduceItems<T>(this IEnumerable<T> sequence, Func<T, T, T> itemMapping)
    {
        return sequence.Aggregate(itemMapping);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T2 FoldItems<T, T2>(this IEnumerable<T> sequence, T2 initialValue, Func<T2, T, T2> itemMapping)
    {
        return sequence.Aggregate(initialValue, itemMapping);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T2> ScanItems<T, T2>(this IEnumerable<T> sequence, T2 initialValue, Func<T2, T, T2> itemMapping)
    {
        var oldItem = initialValue;
        yield return oldItem;

        foreach (var item in sequence)
        {
            oldItem = itemMapping(oldItem, item);
            yield return oldItem;
        }
    }

}