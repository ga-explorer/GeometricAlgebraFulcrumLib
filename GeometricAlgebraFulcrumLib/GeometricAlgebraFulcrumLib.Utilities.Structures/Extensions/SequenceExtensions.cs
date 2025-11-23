using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

public static class SequenceExtensions
{
    /// <summary>
    /// Gets the first item of the sequence.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence</typeparam>
    /// <param name="source">The sequence to get the first item from</param>
    /// <returns>The first item in the sequence</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when the sequence is empty</exception>
    public static T FirstItem<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        
        if (source is IList<T> list)
        {
            if (list.Count > 0)
                return list[0];

            throw new InvalidOperationException("The sequence is empty.");
        }
        
        if (source is IReadOnlyList<T> readOnlyList)
        {
            if (readOnlyList.Count > 0)
                return readOnlyList[0];

            throw new InvalidOperationException("The sequence is empty.");
        }

        using var enumerator = source.GetEnumerator();
        if (enumerator.MoveNext())
            return enumerator.Current;

        throw new InvalidOperationException("The sequence is empty.");
    }
    
    /// <summary>
    /// Gets the second item of the sequence.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence</typeparam>
    /// <param name="source">The sequence to get the second item from</param>
    /// <returns>The second item in the sequence</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when the sequence has fewer than 2 items</exception>
    public static T SecondItem<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        
        if (source is IList<T> list)
        {
            if (list.Count > 1)
                return list[1];

            throw new InvalidOperationException("The sequence does not contain a second item.");
        }
        
        if (source is IReadOnlyList<T> readOnlyList)
        {
            if (readOnlyList.Count > 1)
                return readOnlyList[1];

            throw new InvalidOperationException("The sequence does not contain a second item.");
        }

        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new InvalidOperationException("The sequence does not contain a second item.");
            
        if (!enumerator.MoveNext())
            throw new InvalidOperationException("The sequence does not contain a second item.");
            
        return enumerator.Current;
    }

    /// <summary>
    /// Safely gets the first item of the sequence, or a default value if empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence</typeparam>
    /// <param name="source">The sequence to get the first item from</param>
    /// <param name="defaultValue"></param>
    /// <returns>The first item in the sequence, or defaultValue if empty</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null</exception>
    public static T FirstItemOrDefault<T>(this IEnumerable<T> source, T defaultValue = default!)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        
        if (source is IList<T> list)
            return (list.Count > 0 ? list[0] : defaultValue)!;
        
        if (source is IReadOnlyList<T> readOnlyList)
            return (readOnlyList.Count > 0 ? readOnlyList[0] : defaultValue)!;

        using var enumerator = source.GetEnumerator();
        return (enumerator.MoveNext() ? enumerator.Current : defaultValue)!;
    }

    /// <summary>
    /// Safely gets the second item of the sequence, or a default value if not enough items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence</typeparam>
    /// <param name="source">The sequence to get the second item from</param>
    /// <param name="defaultValue"></param>
    /// <returns>The second item in the sequence, or defaultValue if fewer than 2 items</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null</exception>
    public static T SecondItemOrDefault<T>(this IEnumerable<T> source, T defaultValue = default!)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        
        if (source is IList<T> list)
            return (list.Count > 1 ? list[1] : defaultValue)!;
        
        if (source is IReadOnlyList<T> readOnlyList)
            return (readOnlyList.Count > 1 ? readOnlyList[1] : defaultValue)!;

        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
            return defaultValue;
            
        if (!enumerator.MoveNext())
            return defaultValue;
            
        return enumerator.Current;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BitwiseXor(this IEnumerable<int> sequence)
    {
        return sequence.Aggregate(0, (acc, value) => acc ^ value);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SelectFirstIndexWhere<T>(this IEnumerable<T> sequence, Func<T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(value)) 
                return i;

            i++;
        }

        return -1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SelectFirstIndexWhere<T>(this IEnumerable<T> sequence, Func<int, T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(i, value)) 
                return i;

            i++;
        }

        return -1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<int, T> SelectFirstIndexValueWhere<T>(this IEnumerable<T> sequence, Func<T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(value)) 
                return new KeyValuePair<int, T>(i, value);

            i++;
        }

        return new KeyValuePair<int, T>(-1, default!);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<int, T> SelectFirstIndexValueWhere<T>(this IEnumerable<T> sequence, Func<int, T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(i, value)) 
                return new KeyValuePair<int, T>(i, value);

            i++;
        }

        return new KeyValuePair<int, T>(-1, default!);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> SelectIndexWhere<T>(this IEnumerable<T> sequence, Func<T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(value)) 
                yield return i;

            i++;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> SelectIndexWhere<T>(this IEnumerable<T> sequence, Func<int, T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(i, value)) 
                yield return i;

            i++;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<int, T>> SelectIndexValueWhere<T>(this IEnumerable<T> sequence, Func<T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(value)) 
                yield return new KeyValuePair<int, T>(i, value);

            i++;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<int, T>> SelectIndexValueWhere<T>(this IEnumerable<T> sequence, Func<int, T, bool> valueFilter)
    {
        var i = 0;
        foreach (var value in sequence)
        {
            if (valueFilter(i, value)) 
                yield return new KeyValuePair<int, T>(i, value);

            i++;
        }
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> SortDistinct<T>(this IEnumerable<T> sequence)
    {
        return sequence.Distinct().OrderBy(i => i);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> sequence)
    {
        return new SortedSet<T>(sequence);
    }
}