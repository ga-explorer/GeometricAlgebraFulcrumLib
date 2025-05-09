using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Ranges;

public sealed record Int64RangeComposer :
    IPair<long>,
    IReadOnlyList<long>
{
    public long MinValue { get; private set; } 

    public long MaxValue { get; private set;}
    
    public int Count 
        => (int) (MaxValue - MinValue + 1L);

    public long this[int index]
        => index >= 0 && index <= MaxValue - MinValue
            ? MinValue + index 
            : throw new IndexOutOfRangeException();

    public long Item1 
        => MinValue;
        
    public long Item2 
        => MaxValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer()
    {
        Reset();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer(long value)
    {
        Reset(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer(long minValue, long maxValue)
    {
        Reset(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out long minValue, out long maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer Reset()
    {
        MinValue = long.MaxValue;
        MaxValue = long.MinValue;

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer Reset(long value)
    {
        MinValue = value;
        MaxValue = value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer Reset(long minValue, long maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer AddValue(long value)
    {
        if (value < MinValue)
            MinValue = value;

        if (value > MaxValue)
            MaxValue = value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer AddValues(params long[] valueList)
    {
        foreach (var value in valueList)
            AddValue(value);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer AddValues(IEnumerable<long> valueList)
    {
        foreach (var value in valueList)
            AddValue(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return t * MaxValue + (1d - t) * MinValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<long> GetEnumerator()
    {
        for (var i = MinValue; i <= MaxValue; i++)
            yield return i;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"[{MinValue:G}, {MaxValue:G}]";
    }
}


public sealed record Int64RangeComposer<T> :
    IPair<KeyValuePair<long, T>>,
    IReadOnlyList<long>
{
    public long MinValue { get; private set; } 

    public long MaxValue { get; private set;}
    
    public T MinValueItem { get; private set; } 

    public T MaxValueItem { get; private set;}
    
    public int Count 
        => (int) (MaxValue - MinValue + 1);

    public long this[int index]
        => index >= 0 && index <= MaxValue - MinValue
            ? MinValue + index 
            : throw new IndexOutOfRangeException();

    public KeyValuePair<long, T> Item1 
        => new KeyValuePair<long, T>(MinValue, MinValueItem);
        
    public KeyValuePair<long, T> Item2 
        => new KeyValuePair<long, T>(MaxValue, MaxValueItem);

 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer(long value, T item)
    {
        Reset(value, item);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer(long minValue, T minValueItem, long maxValue, T maxValueItem)
    {
        Reset(minValue, minValueItem, maxValue, maxValueItem);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out KeyValuePair<long, T> minValueItemPair, out KeyValuePair<long, T> maxValueItemPair)
    {
        minValueItemPair = new KeyValuePair<long, T>(MinValue, MinValueItem);
        maxValueItemPair = new KeyValuePair<long, T>(MaxValue, MaxValueItem);
    }
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer<T> Reset(long value, T item)
    {
        MinValue = value;
        MaxValue = value;

        MinValueItem = item;
        MaxValueItem = item;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer<T> Reset(long minValue, T minValueItem, long maxValue, T maxValueItem)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        MinValueItem = minValueItem;
        MaxValueItem = maxValueItem;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer<T> AddValue(long value, T item)
    {
        if (value < MinValue)
        {
            MinValue = value;
            MinValueItem = item;
        }

        if (value > MaxValue)
        {
            MaxValue = value;
            MaxValueItem = item;
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer<T> AddValues(params KeyValuePair<long, T>[] valueList)
    {
        foreach (var (value, item) in valueList)
            AddValue(value, item);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64RangeComposer<T> AddValues(IEnumerable<KeyValuePair<long, T>> valueList)
    {
        foreach (var (value, item) in valueList)
            AddValue(value, item);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return t * MaxValue + (1d - t) * MinValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<long> GetEnumerator()
    {
        for (var i = MinValue; i <= MaxValue; i++)
            yield return i;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"[{MinValue:G}, {MaxValue:G}]";
    }
}