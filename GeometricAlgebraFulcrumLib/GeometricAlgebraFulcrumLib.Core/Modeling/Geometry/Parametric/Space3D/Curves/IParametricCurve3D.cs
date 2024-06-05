using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;

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