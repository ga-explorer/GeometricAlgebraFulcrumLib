using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling
{
    public interface ILinFloat64DirectionalScalingLinearMap :
        ILinFloat64UnilinearMap
    {
        double ScalingFactor { get; }

        Float64Vector ScalingVector { get; }

        ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse();

        LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling();
    }
}