using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

/// <summary>
/// A parametric 3D curve with continuous first and second derivatives
/// </summary>
public interface IParametricC2Curve3D :
    IParametricCurve3D
{
    LinFloat64Vector3D GetDerivative2Point(double parameterValue);
}