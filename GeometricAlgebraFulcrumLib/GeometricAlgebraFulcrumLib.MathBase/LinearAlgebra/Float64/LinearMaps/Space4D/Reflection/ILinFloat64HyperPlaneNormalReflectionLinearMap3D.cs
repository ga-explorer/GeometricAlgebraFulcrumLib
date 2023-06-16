using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection
{
    public interface ILinFloat64HyperPlaneNormalReflectionLinearMap4D :
        ILinFloat64DirectionalScalingLinearMap4D
    {
        Float64Vector4D ReflectionNormal { get; }

        ILinFloat64HyperPlaneNormalReflectionLinearMap4D GetHyperPlaneNormalReflectionLinearMapInverse();

        LinFloat64HyperPlaneNormalReflection4D ToHyperPlaneNormalReflection();
    }
}