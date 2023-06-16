using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection
{
    public interface ILinFloat64HyperPlaneNormalReflectionLinearMap :
        ILinFloat64DirectionalScalingLinearMap
    {
        Float64Vector ReflectionNormal { get; }

        ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse();

        LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection();
    }
}