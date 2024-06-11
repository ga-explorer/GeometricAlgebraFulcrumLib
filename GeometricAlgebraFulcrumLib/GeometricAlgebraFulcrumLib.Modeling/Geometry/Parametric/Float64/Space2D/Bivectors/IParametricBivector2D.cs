using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Bivectors;

/// <summary>
/// A parametric 2D bivector with continuous first derivative
/// </summary>
public interface IParametricBivector2D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64Bivector2D GetBivector(double parameterValue);

    LinFloat64Bivector2D GetDerivative1Bivector(double parameterValue);

    IFloat64ParametricScalar GetDualScalarCurve();
}