using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;

public interface IDirectionalScalingLinearMap :
    ILinearMap
{
    double ScalingFactor { get; }

    Float64Tuple ScalingVector { get; }

    IDirectionalScalingLinearMap GetDirectionalScalingInverse();

    VectorDirectionalScaling ToVectorDirectionalScaling();
}