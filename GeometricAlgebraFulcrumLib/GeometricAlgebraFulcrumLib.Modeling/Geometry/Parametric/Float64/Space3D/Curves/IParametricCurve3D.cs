using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

/// <summary>
/// A parametric 3D curve with continuous first derivative
/// </summary>
public interface IParametricCurve3D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64Vector3D GetPoint(double parameterValue);

    LinFloat64Vector3D GetDerivative1Point(double parameterValue);

    ParametricCurveLocalFrame3D GetFrame(double parameterValue);
}