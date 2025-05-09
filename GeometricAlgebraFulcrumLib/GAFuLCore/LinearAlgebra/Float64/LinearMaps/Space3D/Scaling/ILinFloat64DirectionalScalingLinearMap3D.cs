using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;

public interface ILinFloat64DirectionalScalingLinearMap3D :
    ILinFloat64UnilinearMap3D
{
    double ScalingFactor { get; }

    LinFloat64Vector3D ScalingVector { get; }

    ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse();

    LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling();
}