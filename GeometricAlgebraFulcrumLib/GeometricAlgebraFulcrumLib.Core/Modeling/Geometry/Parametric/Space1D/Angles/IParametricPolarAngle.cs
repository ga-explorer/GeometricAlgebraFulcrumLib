using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;

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