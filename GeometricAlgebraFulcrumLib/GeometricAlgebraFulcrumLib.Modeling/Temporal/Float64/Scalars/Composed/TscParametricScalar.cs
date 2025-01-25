using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscParametricScalar :
    TemporalFloat64Scalar
{
    internal static TscParametricScalar Create(IFloat64ParametricScalar parametricScalar)
    {
        return new TscParametricScalar(parametricScalar);
    }


    public override Float64ScalarRange TimeRange
        => ParametricScalar.ParameterRange;

    public IFloat64ParametricScalar ParametricScalar { get; }


    private TscParametricScalar(IFloat64ParametricScalar parametricScalar)
    {
        ParametricScalar = parametricScalar;
    }


    public override bool IsValid()
    {
        return TimeRange.IsValid();
    }

    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return ParametricScalar.GetValue(t).ScalarValue;
    }

    public override double GetDerivativeValue(double t)
    {
        t = this.TimeClamp(t);

        return ParametricScalar.GetDerivative1Value(t).ScalarValue;
    }
}