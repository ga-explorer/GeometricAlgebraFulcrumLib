using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Trivectors;

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