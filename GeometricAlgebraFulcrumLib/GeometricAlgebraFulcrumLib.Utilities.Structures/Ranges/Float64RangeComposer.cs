using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Ranges;

public sealed record Float64RangeComposer :
    IPair<double>
{
    public double MinValue { get; private set; } 

    public double MaxValue { get; private set;}

    public double MidValue 
        => 0.5d * (MinValue + MaxValue);

    public double Length 
        => MaxValue - MinValue;

    public double Item1 
        => MinValue;
        
    public double Item2 
        => MaxValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer()
    {
        Reset();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer(double value)
    {
        Reset(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer(double minValue, double maxValue)
    {
        Reset(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out double minValue, out double maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer Reset()
    {
        MinValue = double.PositiveInfinity;
        MaxValue = double.NegativeInfinity;

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer Reset(double value)
    {
        if (double.IsNaN(value))
            throw new ArgumentException(nameof(value));
            
        MinValue = value;
        MaxValue = value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer Reset(double minValue, double maxValue)
    {
        if (double.IsNaN(minValue))
            throw new ArgumentException(nameof(minValue));

        if (double.IsNaN(maxValue))
            throw new ArgumentException(nameof(maxValue));

        MinValue = minValue;
        MaxValue = maxValue;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer AddValue(double value)
    {
        if (double.IsNaN(value))
            throw new ArgumentException(nameof(value));

        if (value < MinValue)
            MinValue = value;

        if (value > MaxValue)
            MaxValue = value;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer AddValues(params double[] valueList)
    {
        foreach (var value in valueList)
            AddValue(value);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer AddValues(IEnumerable<double> valueList)
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
    public override string ToString()
    {
        return $"[{MinValue:G}, {MaxValue:G}]";
    }
}

public sealed record Float64RangeComposer<T> :
    IPair<KeyValuePair<double, T>>
{
    public double MinValue { get; private set; } 

    public double MaxValue { get; private set;}

    public double MidValue 
        => 0.5d * (MinValue + MaxValue);

    public double Length 
        => MaxValue - MinValue;

    public T MinValueItem { get; private set; }

    public T MaxValueItem { get; private set; }

    public KeyValuePair<double, T> Item1 
        => new KeyValuePair<double, T>(MinValue, MinValueItem);
        
    public KeyValuePair<double, T> Item2 
        => new KeyValuePair<double, T>(MaxValue, MaxValueItem);

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer(double value, T item)
    {
        Reset(value, item);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer(double minValue, T minValueItem, double maxValue, T maxValueItem)
    {
        Reset(minValue, minValueItem, maxValue, maxValueItem);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out KeyValuePair<double, T> minValueItemPair, out KeyValuePair<double, T> maxValueItemPair)
    {
        minValueItemPair = new KeyValuePair<double, T>(MinValue, MinValueItem);
        maxValueItemPair = new KeyValuePair<double, T>(MaxValue, MaxValueItem);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer<T> Reset(double value, T item)
    {
        if (double.IsNaN(value))
            throw new ArgumentException(nameof(value));
            
        MinValue = value;
        MaxValue = value;

        MinValueItem = item;
        MaxValueItem = item;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer<T> Reset(double minValue, T minValueItem, double maxValue, T maxValueItem)
    {
        if (double.IsNaN(minValue))
            throw new ArgumentException(nameof(minValue));

        if (double.IsNaN(maxValue))
            throw new ArgumentException(nameof(maxValue));

        MinValue = minValue;
        MaxValue = maxValue;

        MinValueItem = minValueItem; 
        MaxValueItem = maxValueItem;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer<T> AddValue(double value, T item)
    {
        if (double.IsNaN(value))
            throw new ArgumentException(nameof(value));

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
    public Float64RangeComposer<T> AddValues(params KeyValuePair<double, T>[] valueList)
    {
        foreach (var (value, item) in valueList)
            AddValue(value, item);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RangeComposer<T> AddValues(IEnumerable<KeyValuePair<double, T>> valueList)
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
    public override string ToString()
    {
        return $"[{MinValue:G}, {MaxValue:G}]";
    }
}