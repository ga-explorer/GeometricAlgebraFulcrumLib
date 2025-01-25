using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;

public abstract class TemporalFloat64ScalarNormalized :
    TemporalFloat64Scalar
{
    public override Float64ScalarRange TimeRange
        => Float64ScalarRange.NegativeOneToOne;


    protected override Float64ScalarRange FindValueRange()
    {
        return Float64ScalarRange.NegativeOneToOne;
    }
}