using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Ranges;

public sealed record Int32RangeComposer :
    IPair<int>,
    IReadOnlyList<int>
{
    public int MinValue { get; private set; } 

    public int MaxValue { get; private set;}
    
    public int Count 
        => MaxValue - MinValue + 1;

    public int this[int index]
        => index >= 0 && index <= MaxValue - MinValue
            ? MinValue + index 
            : throw new IndexOutOfRangeException();

    public int Item1 
        => MinValue;
        
    public int Item2 
        => MaxValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer()
    {
        Reset();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer(int value)
    {
        Reset(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer(int minValue, int maxValue)
    {
        Reset(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int minValue, out int maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer Reset()
    {
        MinValue = int.MaxValue;
        MaxValue = int.MinValue;

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer Reset(int value)
    {
        MinValue = value;
        MaxValue = value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer Reset(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer AddValue(int value)
    {
        if (value < MinValue)
            MinValue = value;

        if (value > MaxValue)
            MaxValue = value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer AddValues(params int[] valueList)
    {
        foreach (var value in valueList)
            AddValue(value);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer AddValues(IEnumerable<int> valueList)
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
    public IEnumerator<int> GetEnumerator()
    {
        return Enumerable.Range(MinValue, Count).GetEnumerator();
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

public sealed record Int32RangeComposer<T> :
    IPair<KeyValuePair<int, T>>,
    IReadOnlyList<int>
{
    public int MinValue { get; private set; } 

    public int MaxValue { get; private set;}
    
    public T MinValueItem { get; private set; } 

    public T MaxValueItem { get; private set;}
    
    public int Count 
        => MaxValue - MinValue + 1;

    public int this[int index]
        => index >= 0 && index <= MaxValue - MinValue
            ? MinValue + index 
            : throw new IndexOutOfRangeException();

    public KeyValuePair<int, T> Item1 
        => new KeyValuePair<int, T>(MinValue, MinValueItem);
        
    public KeyValuePair<int, T> Item2 
        => new KeyValuePair<int, T>(MaxValue, MaxValueItem);

 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer(int value, T item)
    {
        Reset(value, item);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer(int minValue, T minValueItem, int maxValue, T maxValueItem)
    {
        Reset(minValue, minValueItem, maxValue, maxValueItem);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out KeyValuePair<int, T> minValueItemPair, out KeyValuePair<int, T> maxValueItemPair)
    {
        minValueItemPair = new KeyValuePair<int, T>(MinValue, MinValueItem);
        maxValueItemPair = new KeyValuePair<int, T>(MaxValue, MaxValueItem);
    }
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer<T> Reset(int value, T item)
    {
        MinValue = value;
        MaxValue = value;

        MinValueItem = item;
        MaxValueItem = item;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer<T> Reset(int minValue, T minValueItem, int maxValue, T maxValueItem)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        MinValueItem = minValueItem;
        MaxValueItem = maxValueItem;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer<T> AddValue(int value, T item)
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
    public Int32RangeComposer<T> AddValues(params KeyValuePair<int, T>[] valueList)
    {
        foreach (var (value, item) in valueList)
            AddValue(value, item);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32RangeComposer<T> AddValues(IEnumerable<KeyValuePair<int, T>> valueList)
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
    public IEnumerator<int> GetEnumerator()
    {
        return Enumerable.Range(MinValue, Count).GetEnumerator();
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