using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

/// <summary>
/// A parametric 1D curve with continuous first and second derivatives
/// </summary>
public interface IFloat64ParametricC2Scalar :
    IFloat64ParametricScalar
{
    Float64Scalar GetDerivative2Value(double parameterValue);
}