using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;

public abstract class Float64ArcLengthPath2D(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64Path2D(timeRange, isPeriodic)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return ToFiniteArcLengthPath();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return ToPeriodicArcLengthPath();
    }

    public abstract Float64ArcLengthPath2D ToFiniteArcLengthPath();

    public abstract Float64ArcLengthPath2D ToPeriodicArcLengthPath();


    public abstract Float64Scalar GetLength();

    public abstract Float64Scalar TimeToLength(double t);

    public abstract Float64Scalar LengthToTime(double length);

}