using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors2D.Float64;

/// <summary>
/// A parametric 2D bivector with continuous first derivative
/// </summary>
public interface IParametricBivector2D :
    IAlgebraicElement
{
    Float64ScalarRange TimeRange { get; }

    LinFloat64Bivector2D GetValue(double parameterValue);

    LinFloat64Bivector2D GetDerivative1Bivector(double parameterValue);

    Float64ScalarSignal GetDualScalarCurve();
}