using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves
{
    public interface IArcLengthCurve2D :
        IParametricCurve2D
    {
        Float64Scalar GetLength();

        Float64Scalar ParameterToLength(double parameterValue);

        Float64Scalar LengthToParameter(double length);
    }
}