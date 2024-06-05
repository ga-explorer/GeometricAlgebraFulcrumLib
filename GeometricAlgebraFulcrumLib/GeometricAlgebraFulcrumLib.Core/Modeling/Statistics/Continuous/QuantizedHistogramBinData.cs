using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Statistics.Continuous;

public sealed record QuantizedHistogramBinData
{
    public int Index { get; }

    private readonly double _midValue;
    public double MidValue
    {
        get => _midValue;
        init
        {
            if (value.IsNaNOrInfinite())
                throw new ArgumentException(nameof(value));

            _midValue = value;
        }
    }

    private readonly double _width;
    public double Width
    {
        get => _width;
        init
        {
            if (value.IsNaNOrInfinite() || value < 0)
                throw new ArgumentException(nameof(value));

            _width = value;
        }
    }

    public ulong HistogramSum { get; }

    public ulong Height { get; }

    public double NormalizedHeight 
        => Height / (double) HistogramSum;    

    public double MinValue 
        => MidValue - Width / 2d;

    public double MaxValue 
        => MidValue + Width / 2d;

    public double HalfWidth 
        => Width / 2;


    //public QuantizedHistogramBinData(double midValue, double width, ulong height, ulong histSum)
    //{
    //    if (height > histSum)
    //        throw new ArgumentException(nameof(height));

    //    Index = -1;
    //    MidValue = midValue;
    //    Width = width;
    //    Height = height;
    //    HistogramSum = histSum;

    //    Debug.Assert(IsValid());
    //}
    
    public QuantizedHistogramBinData(int index, double midValue, double width, ulong height, ulong histSum)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (height > histSum)
            throw new ArgumentException(nameof(height));

        Index = index;
        MidValue = midValue;
        Width = width;
        Height = height;
        HistogramSum = histSum;

        Debug.Assert(IsValid());
    }

    public void Deconstruct(out double midValue, out double width, out ulong height, out ulong histSum)
    {
        midValue = MidValue;
        width = Width;
        height = Height;
        histSum = HistogramSum;
    }


    public bool IsValid()
    {
        return Index >= 0 && 
               !MidValue.IsNaNOrInfinite() &&
               Width > 0 &&
               Height <= HistogramSum;
    }


    public bool Contains(double value)
    {
        return value >= MinValue && value < MaxValue;
    }


    public ulong GetHeightBefore(double value)
    {
        return (ulong)Math.Round(GetWidthBefore(value) / Width * Height);
    }
    
    public ulong GetHeightAfter(double value)
    {
        return (ulong)Math.Round(GetWidthAfter(value) / Width * Height);
    }
    
    public ulong GetHeightBetween(double value1, double value2)
    {
        return (ulong)Math.Round(GetWidthBetween(value1, value2) / Width * Height);
    }


    public double GetWidthBefore(double value)
    {
        if (value <= MinValue) return 0;
        if (value >= MaxValue) return Width;

        return value - MinValue;
    }
        
    public double GetWidthAfter(double value)
    {
        if (value <= MinValue) return Width;
        if (value >= MaxValue) return 0;

        return MaxValue - value;
    }
        
    public double GetWidthBetween(double value1, double value2)
    {
        var (v1, v2) = 
            value1 <= value2
                ? (value1, value2) 
                : (value2, value1);

        if (v2 <= MinValue || v1 >= MaxValue) return 0;

        v1 = Math.Max(v1, MinValue);
        v2 = Math.Min(v2, MaxValue);

        return v2 - v1;
    }

    
    public double GetArea()
    {
        return Width * Height;
    }

    public double GetAreaBefore(double value)
    {
        if (value <= MinValue) return 0;
        if (value >= MaxValue) return GetArea();

        return (value - MinValue) * Height;
    }

    public double GetAreaAfter(double value)
    {
        if (value <= MinValue) return GetArea();
        if (value >= MaxValue) return 0;

        return (MaxValue - value) * Height;
    }
        
    public double GetAreaBetween(double value1, double value2)
    {
        var (v1, v2) = 
            value1 <= value2
                ? (value1, value2) 
                : (value2, value1);

        if (v2 <= MinValue || v1 >= MaxValue) return 0;

        v1 = Math.Max(v1, MinValue);
        v2 = Math.Min(v2, MaxValue);

        return (v2 - v1) * Height;
    }


    public double GetNormalizedArea()
    {
        return Width * NormalizedHeight;
    }

    public double GetNormalizedAreaBefore(double value)
    {
        if (value <= MinValue) return 0;
        if (value >= MaxValue) return GetNormalizedArea();

        return (value - MinValue) * NormalizedHeight;
    }

    public double GetNormalizedAreaAfter(double value)
    {
        if (value <= MinValue) return GetNormalizedArea();
        if (value >= MaxValue) return 0;

        return (MaxValue - value) * NormalizedHeight;
    }
        
    public double GetNormalizedAreaBetween(double value1, double value2)
    {
        var (v1, v2) = 
            value1 <= value2
                ? (value1, value2) 
                : (value2, value1);

        if (v2 <= MinValue || v1 >= MaxValue) return 0;

        v1 = Math.Max(v1, MinValue);
        v2 = Math.Min(v2, MaxValue);

        return (v2 - v1) * NormalizedHeight;
    }

    public override string ToString()
    {
        var indexText = Index >= 0 ? $"<{Index}>" : "";

        return $@"Histogram Bin{indexText}({MidValue:G}±{HalfWidth:G}) <- ({Height}/{HistogramSum} = {NormalizedHeight:G})";
    }
}