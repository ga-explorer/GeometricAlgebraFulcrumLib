using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Trivectors3D.Float64;

/// <summary>
/// A parametric 3D bivector with continuous first derivative
/// </summary>
public interface ILinFloat64Trivector3DTrajectory :
    IFloat64Trajectory<LinFloat64Trivector3D>
{
    LinFloat64Trivector3D GetDerivative1Value(double t);
    
    LinFloat64Trivector3D GetDerivative2Value(double t);

    Float64ScalarSignal GetDualScalarCurve();
}