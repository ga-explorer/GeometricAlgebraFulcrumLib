using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space4D.Curves;

/// <summary>
/// A parametric 4D curve with continuous first derivative
/// </summary>
public interface IParametricCurve4D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }
        
    LinFloat64Vector4D GetPoint(double parameterValue);

    LinFloat64Vector4D GetDerivative1Point(double parameterValue);
}