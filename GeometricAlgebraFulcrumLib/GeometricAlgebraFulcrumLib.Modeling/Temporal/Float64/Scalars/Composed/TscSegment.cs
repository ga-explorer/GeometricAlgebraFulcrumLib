using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscSegment :
    TemporalFloat64Scalar
{
    internal static TscSegment Create(TemporalFloat64Scalar baseScalar, Float64Scalar timeMin, Float64Scalar timeMax)
    {
        if (timeMin < timeMax)
            return new TscSegment(baseScalar, timeMin, timeMax);

        return new TscSegment(
            baseScalar.FlipTimeRange(timeMax, timeMin), 
            timeMax, 
            timeMin
        );
    }

    internal static TscSegment CreateFromRelativeTime(TemporalFloat64Scalar baseScalar, Float64Scalar t1, Float64Scalar t2)
    {
        var timeMin = baseScalar.GetRelativeTime(t1);
        var timeMax = baseScalar.GetRelativeTime(t2);

        return Create(baseScalar, timeMin, timeMax);
    }


    public TemporalFloat64Scalar BaseScalar { get; }

    public override Float64ScalarRange TimeRange { get; }


    private TscSegment(TemporalFloat64Scalar baseScalar, double timeMin, double timeMax)
    {
        BaseScalar = baseScalar;
        TimeRange = Float64ScalarRange.Create(timeMin, timeMax);

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return BaseScalar.IsValid() &&
               TimeRange.IsValid();
    }
    
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return BaseScalar.GetValue(t);
    }
}