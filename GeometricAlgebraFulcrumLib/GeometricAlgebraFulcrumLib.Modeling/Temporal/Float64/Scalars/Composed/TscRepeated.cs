using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscRepeated :
    TemporalFloat64Scalar
{
    public static TscRepeated Create(TemporalFloat64Scalar baseScalar, int count)
    {
        return new TscRepeated(
            baseScalar, count
        );
    }


    public TemporalFloat64Scalar BaseScalar { get; }

    public int Count { get; }

    public override Float64ScalarRange TimeRange { get; }
        

    private TscRepeated(TemporalFloat64Scalar baseScalar, int count)
    {
        if (count < 2)
            throw new InvalidOperationException();

        BaseScalar = baseScalar;
        Count = count;
        TimeRange = Float64ScalarRange.Create(
            BaseScalar.MinTime,
            BaseScalar.MinTime + BaseScalar.TimeRangeLength * Count
        );

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return BaseScalar.IsValid() &&
               Count >= 2;
    }
    
    protected override Float64ScalarRange FindValueRange()
    {
        return BaseScalar.ValueRange;
    }
    
    public override double GetValue(double t)
    {
        t = BaseScalar.TimeClamp(
            this.TimeClamp(t), 
            true
        );

        //t = BaseScalar.GetRelativeTime(
        //    ((this.TimeClamp(t) - MinTime) / BaseScalar.TimeRangeLength).FractionalPart()
        //);

        return BaseScalar.GetValue(t);
    }
}