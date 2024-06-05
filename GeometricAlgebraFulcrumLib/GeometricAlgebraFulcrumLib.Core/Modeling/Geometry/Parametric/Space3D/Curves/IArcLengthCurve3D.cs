using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;

public interface IArcLengthCurve3D :
    IParametricCurve3D
{
    Float64Scalar GetLength();

    Float64Scalar ParameterToLength(double parameterValue);

    Float64Scalar LengthToParameter(double length);
}