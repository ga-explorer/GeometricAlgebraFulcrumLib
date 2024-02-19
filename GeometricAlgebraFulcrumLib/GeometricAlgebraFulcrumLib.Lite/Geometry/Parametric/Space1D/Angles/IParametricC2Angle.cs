using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;

/// <summary>
/// A parametric angle with continuous first and second derivatives
/// </summary>
public interface IParametricC2Angle :
    IParametricAngle
{
    Float64PlanarAngle GetDerivative2Point(double parameterValue);
}