using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal;

public abstract class TemporalValue
    : ITemporalValue
{
    public abstract Float64ScalarRange TimeRange { get; }

    public double MinTime 
        => TimeRange.MinValue.ScalarValue;
    
    public double MidTime
        => TimeRange.MidValue.ScalarValue;

    public double MaxTime 
        => TimeRange.MaxValue.ScalarValue;

    public double TimeRangeLength
        => TimeRange.Length.ScalarValue;

    
    public abstract bool IsValid();
}


public abstract class TemporalValue<T> :
    TemporalValue,
    ITemporalValue<T>
{
    public T ValueAtMinTime
        => GetValue(MinTime);

    public T ValueAtMidTime
        => GetValue(MidTime);

    public T ValueAtMaxTime
        => GetValue(MaxTime);


    public abstract T GetValue(double t);
}