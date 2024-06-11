using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Statistics.Continuous;

public sealed record HistogramBinData
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

    private readonly double _height;
    public double Height
    {
        get => _height;
        init
        {
            if (value.IsNaNOrInfinite() || value < 0)
                throw new ArgumentException(nameof(value));

            _height = value;
        }
    }


    public double MinValue 
        => MidValue - Width / 2d;

    public double MaxValue 
        => MidValue + Width / 2d;

    public double HalfWidth 
        => Width / 2;

    public double Area 
        => Width * Height;

    
    public HistogramBinData(double midValue, double width, double height)
    {
        Index = -1;
        MidValue = midValue;
        Width = width;
        Height = height;
    }
    
    public HistogramBinData(int index, double midValue, double width, double height)
    {
        Index = index;
        MidValue = midValue;
        Width = width;
        Height = height;
    }

    public void Deconstruct(out double midValue, out double width, out double height)
    {
        midValue = MidValue;
        width = Width;
        height = Height;
    }


    public bool IsValid()
    {
        return //Index >= 0 &&
               !MidValue.IsNaNOrInfinite() &&
               Width > 0 &&
               Height >= 0;
    }


    public bool Contains(double value)
    {
        return value >= MinValue && value < MaxValue;
    }
        

    public double GetLengthBefore(double value)
    {
        if (value <= MinValue) return 0;
        if (value >= MaxValue) return Width;

        return value - MinValue;
    }
        
    public double GetLengthAfter(double value)
    {
        if (value <= MinValue) return Width;
        if (value >= MaxValue) return 0;

        return MaxValue - value;
    }
        
    public double GetLengthBetween(double value1, double value2)
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


    public double GetAreaBefore(double value)
    {
        if (value <= MinValue) return 0;
        if (value >= MaxValue) return Area;

        return (value - MinValue) * Height;
    }

    public double GetAreaAfter(double value)
    {
        if (value <= MinValue) return Area;
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


    public override string ToString()
    {
        return $@"Histogram Bin<{Index}>({MidValue:G}±{HalfWidth:G}) = {Height:G}";
    }

}