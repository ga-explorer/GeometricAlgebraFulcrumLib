using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

/// <summary>
/// A parametric 1D curve with continuous first and second derivatives
/// </summary>
public interface IParametricC2Scalar :
    IParametricScalar
{
    Float64Scalar GetDerivative2Value(double parameterValue);
}