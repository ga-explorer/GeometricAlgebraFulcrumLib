using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;

/// <summary>
/// A parametric 2D curve with continuous first and second derivatives
/// </summary>
public interface IParametricC2Curve2D :
    IFloat64ParametricCurve2D
{
    LinFloat64Vector2D GetDerivative2Point(double parameterValue);
}