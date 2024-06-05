using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;

public interface ILinFloat64DirectionalScalingLinearMap :
    ILinFloat64UnilinearMap
{
    double ScalingFactor { get; }

    LinFloat64Vector ScalingVector { get; }

    ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse();

    LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling();
}