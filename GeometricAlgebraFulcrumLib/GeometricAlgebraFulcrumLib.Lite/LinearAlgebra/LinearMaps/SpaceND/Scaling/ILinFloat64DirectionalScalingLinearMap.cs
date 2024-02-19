using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Scaling;

public interface ILinFloat64DirectionalScalingLinearMap :
    ILinFloat64UnilinearMap
{
    double ScalingFactor { get; }

    Float64Vector ScalingVector { get; }

    ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse();

    LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling();
}