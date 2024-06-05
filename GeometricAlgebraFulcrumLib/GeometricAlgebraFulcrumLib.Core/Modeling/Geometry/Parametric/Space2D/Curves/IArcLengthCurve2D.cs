using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;

public interface IArcLengthCurve2D :
    IFloat64ParametricCurve2D
{
    Float64Scalar GetLength();

    Float64Scalar ParameterToLength(double parameterValue);

    Float64Scalar LengthToParameter(double length);
}