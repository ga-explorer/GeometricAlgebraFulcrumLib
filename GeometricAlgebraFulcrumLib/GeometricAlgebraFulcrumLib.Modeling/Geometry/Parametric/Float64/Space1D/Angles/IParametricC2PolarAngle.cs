using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;

/// <summary>
/// A parametric angle with continuous first and second derivatives
/// </summary>
public interface IParametricC2PolarAngle :
    IParametricPolarAngle
{
    LinFloat64PolarAngle GetDerivative2Angle(double parameterValue);
}