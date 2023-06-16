using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection
{
    public interface ILinFloat64HyperPlaneNormalReflectionLinearMap3D :
        ILinFloat64DirectionalScalingLinearMap3D
    {
        Float64Vector3D ReflectionNormal { get; }

        ILinFloat64HyperPlaneNormalReflectionLinearMap3D GetHyperPlaneNormalReflectionLinearMapInverse();

        LinFloat64HyperPlaneNormalReflection3D ToHyperPlaneNormalReflection();
    }
}