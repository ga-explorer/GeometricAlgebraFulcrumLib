using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;

public interface IArcLengthCurve3D :
    IParametricCurve3D
{
    Float64Scalar GetLength();

    Float64Scalar ParameterToLength(double parameterValue);

    Float64Scalar LengthToParameter(double length);
}