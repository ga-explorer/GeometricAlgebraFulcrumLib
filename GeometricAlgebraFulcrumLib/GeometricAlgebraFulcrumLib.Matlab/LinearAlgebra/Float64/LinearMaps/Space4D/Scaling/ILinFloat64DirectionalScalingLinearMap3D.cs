using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;

public interface ILinFloat64DirectionalScalingLinearMap4D :
    ILinFloat64UnilinearMap4D
{
    double ScalingFactor { get; }

    LinFloat64Vector4D ScalingVector { get; }

    ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse();

    LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling();
}