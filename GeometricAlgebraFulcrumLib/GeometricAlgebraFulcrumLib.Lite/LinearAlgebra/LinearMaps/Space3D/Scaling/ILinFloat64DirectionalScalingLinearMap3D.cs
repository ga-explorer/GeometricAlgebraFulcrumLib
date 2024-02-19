using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Scaling;

public interface ILinFloat64DirectionalScalingLinearMap3D :
    ILinFloat64UnilinearMap3D
{
    double ScalingFactor { get; }

    Float64Vector3D ScalingVector { get; }

    ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse();

    LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling();
}