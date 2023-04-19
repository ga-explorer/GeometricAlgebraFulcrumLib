using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Scaling;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection
{
    public interface ILinFloat64HyperPlaneNormalReflectionLinearMap :
        ILinFloat64DirectionalScalingLinearMap
    {
        LinFloat64Vector ReflectionNormal { get; }

        ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse();

        LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection();
    }
}