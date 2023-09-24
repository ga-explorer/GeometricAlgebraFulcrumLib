using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection
{
    public interface ILinFloat64HyperPlaneNormalReflectionLinearMap :
        ILinFloat64DirectionalScalingLinearMap
    {
        Float64Vector ReflectionNormal { get; }

        ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse();

        LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection();
    }
}