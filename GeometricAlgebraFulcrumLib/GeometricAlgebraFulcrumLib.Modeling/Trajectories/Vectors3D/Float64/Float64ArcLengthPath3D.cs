using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

public abstract class Float64ArcLengthPath3D(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64Path3D(timeRange, isPeriodic)
{
    public abstract Float64ArcLengthPath3D ToFiniteArcLengthPath();

    public abstract Float64ArcLengthPath3D ToPeriodicArcLengthPath();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToFinitePath()
    {
        return ToFiniteArcLengthPath();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return ToPeriodicArcLengthPath();
    }


    public abstract Float64Scalar GetLength();

    public abstract Float64Scalar TimeToLength(double t);

    public abstract Float64Scalar LengthToTime(double length);

}