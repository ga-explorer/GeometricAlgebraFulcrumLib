using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;

/// <summary>
/// A parametric angle with continuous first derivative
/// </summary>
public interface IParametricPolarAngle :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64PolarAngle GetAngle(double parameterValue);

    LinFloat64PolarAngle GetDerivative1Angle(double parameterValue);

    IFloat64ParametricScalar ToRadianParametricScalar();
}