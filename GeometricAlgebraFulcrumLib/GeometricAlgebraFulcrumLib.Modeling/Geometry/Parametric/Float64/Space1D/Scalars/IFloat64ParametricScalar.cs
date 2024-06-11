using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

/// <summary>
/// A parametric 1D curve with continuous first derivative
/// </summary>
public interface IFloat64ParametricScalar :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    Float64Scalar GetValue(double parameterValue);

    Float64Scalar GetDerivative1Value(double parameterValue);
}