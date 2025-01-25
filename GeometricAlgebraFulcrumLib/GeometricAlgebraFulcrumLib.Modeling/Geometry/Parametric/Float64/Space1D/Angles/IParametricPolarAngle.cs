using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;

/// <summary>
/// A parametric angle with continuous first derivative
/// </summary>
public interface IParametricPolarAngle :
    IFloat64ParametricScalar
{
    LinFloat64PolarAngle GetAngle(double parameterValue);

    LinFloat64PolarAngle GetDerivative1Angle(double parameterValue);

    IFloat64ParametricScalar ToRadianParametricScalar();
}