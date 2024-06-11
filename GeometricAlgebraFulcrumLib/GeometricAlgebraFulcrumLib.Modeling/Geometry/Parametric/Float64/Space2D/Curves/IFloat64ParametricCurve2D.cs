using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;

public interface IFloat64ParametricCurve2D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64Vector2D GetPoint(double t);

    LinFloat64Vector2D GetDerivative1Point(double parameterValue);

    ParametricCurveLocalFrame2D GetFrame(double parameterValue);
}