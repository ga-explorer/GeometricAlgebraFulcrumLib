using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

public interface IArcLengthCurve3D :
    IParametricCurve3D
{
    Float64Scalar GetLength();

    Float64Scalar ParameterToLength(double parameterValue);

    Float64Scalar LengthToParameter(double length);
}