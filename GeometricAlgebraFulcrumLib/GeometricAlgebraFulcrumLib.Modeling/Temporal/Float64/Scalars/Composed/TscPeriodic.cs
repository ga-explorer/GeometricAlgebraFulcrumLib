using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscPeriodic :
    TemporalFloat64Scalar
{
    public TemporalFloat64Scalar BaseScalar { get; }

    public override Float64ScalarRange TimeRange 
        => BaseScalar.TimeRange;


    internal TscPeriodic(TemporalFloat64Scalar baseScalar)
    {
        BaseScalar = baseScalar;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseScalar.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.TimeClamp(t, true);

        var value = BaseScalar.GetValue(t);

        Debug.Assert(value.IsFinite());

        return value;
    }

}