using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

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