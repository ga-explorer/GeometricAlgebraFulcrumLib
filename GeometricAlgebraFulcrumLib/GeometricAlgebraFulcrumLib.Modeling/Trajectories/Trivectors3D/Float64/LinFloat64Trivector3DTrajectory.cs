using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Trivectors3D.Float64;

public abstract class LinFloat64Trivector3DTrajectory(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64Trajectory<LinFloat64Trivector3D>(timeRange, isPeriodic),
    ILinFloat64Trivector3DTrajectory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToFinite()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToPeriodic()
    {
        throw new NotImplementedException();
    }


    public abstract LinFloat64Trivector3D GetDerivative1Value(double t);

    public abstract LinFloat64Trivector3D GetDerivative2Value(double t);

    public abstract Float64ScalarSignal GetDualScalarCurve();

}