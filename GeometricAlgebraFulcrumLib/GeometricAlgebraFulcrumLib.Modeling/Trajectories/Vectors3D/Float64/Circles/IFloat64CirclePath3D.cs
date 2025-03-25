using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Circles;

public interface IFloat64CirclePath3D : 
    IFloat64Trajectory<LinFloat64Vector3D>
{
    double Radius { get; }

    LinFloat64Vector3D Center { get; }

    LinFloat64Vector3D UnitNormal { get; }

    int RotationCount { get; }
}