using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Trivectors;

/// <summary>
/// A parametric 3D bivector with continuous first derivative
/// </summary>
public interface IParametricTrivector3D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }
        
    LinFloat64Trivector3D GetTrivector(double parameterValue);

    LinFloat64Trivector3D GetDerivative1Trivector(double parameterValue);

    IFloat64ParametricScalar GetDualScalarCurve();
}