using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Circles;

public abstract class Float64AxisAlignedCirclePath3D(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64ArcLengthPath3D(timeRange, isPeriodic),
    IFloat64CirclePath3D
{
    public abstract double Radius { get; }

    public abstract LinFloat64Vector3D Center { get; }

    public abstract LinFloat64Vector3D UnitNormal { get; }

    public abstract int RotationCount { get; }
}