using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Bivectors;

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