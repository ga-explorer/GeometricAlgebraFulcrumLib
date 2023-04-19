namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Scaling
{
    public interface ILinFloat64DirectionalScalingLinearMap :
        ILinFloat64UnilinearMap
    {
        double ScalingFactor { get; }

        LinFloat64Vector ScalingVector { get; }

        ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse();

        LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling();
    }
}