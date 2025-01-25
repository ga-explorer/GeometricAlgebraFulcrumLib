using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscDerivative :
    TemporalFloat64Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TscDerivative Create(TemporalFloat64Scalar baseScalar)
    {
        return new TscDerivative(baseScalar);
    }


    public TemporalFloat64Scalar BaseScalar { get; }

    public override Float64ScalarRange TimeRange 
        => BaseScalar.TimeRange;


    private TscDerivative(TemporalFloat64Scalar baseScalar)
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
        t = this.TimeClamp(t);

        var value = 
            MathNet.Numerics.Differentiate.Derivative(
                BaseScalar.GetValue, t, 1
            );

        Debug.Assert(value.IsFinite());

        return value;
    }
}