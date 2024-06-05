using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;

/// <summary>
/// A parametric angle with continuous first and second derivatives
/// </summary>
public interface IParametricC2PolarAngle :
    IParametricPolarAngle
{
    LinFloat64PolarAngle GetDerivative2Point(double parameterValue);
}