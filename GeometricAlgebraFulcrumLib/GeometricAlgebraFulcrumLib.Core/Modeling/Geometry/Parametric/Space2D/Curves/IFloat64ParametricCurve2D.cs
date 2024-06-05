using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;

public interface IFloat64ParametricCurve2D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64Vector2D GetPoint(double t);

    LinFloat64Vector2D GetDerivative1Point(double parameterValue);

    ParametricCurveLocalFrame2D GetFrame(double parameterValue);
}