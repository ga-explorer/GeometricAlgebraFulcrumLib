using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

public sealed class TscParametricPolarAngle :
    TemporalFloat64Scalar
{
    internal static TscParametricPolarAngle Create(IParametricPolarAngle parametricAngle)
    {
        return new TscParametricPolarAngle(parametricAngle);
    }


    public override Float64ScalarRange TimeRange
        => ParametricAngle.ParameterRange;

    public IParametricPolarAngle ParametricAngle { get; }


    private TscParametricPolarAngle(IParametricPolarAngle parametricAngle)
    {
        ParametricAngle = parametricAngle;
    }


    public override bool IsValid()
    {
        return TimeRange.IsValid();
    }

    public override double GetValue(double t)
    {
        t = this.TimeClamp(t);

        return ParametricAngle.GetAngle(t).RadiansValue;
    }

    public override double GetDerivativeValue(double t)
    {
        t = this.TimeClamp(t);

        return ParametricAngle.GetDerivative1Angle(t).RadiansValue;
    }
}