using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;

public interface IArcLengthCurve2D :
    IFloat64ParametricCurve2D
{
    Float64Scalar GetLength();

    Float64Scalar ParameterToLength(double parameterValue);

    Float64Scalar LengthToParameter(double length);
}