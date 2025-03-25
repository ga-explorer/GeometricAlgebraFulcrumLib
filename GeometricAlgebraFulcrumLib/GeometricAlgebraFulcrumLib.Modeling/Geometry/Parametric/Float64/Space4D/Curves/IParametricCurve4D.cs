using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;

/// <summary>
/// A parametric 4D curve with continuous first derivative
/// </summary>
public interface IParametricCurve4D :
    IAlgebraicElement
{
    Float64ScalarRange TimeRange { get; }

    LinFloat64Vector4D GetPoint(double parameterValue);

    LinFloat64Vector4D GetDerivative1Point(double parameterValue);
}