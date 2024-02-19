using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection;

public interface ILinFloat64HyperPlaneNormalReflectionLinearMap3D :
    ILinFloat64DirectionalScalingLinearMap3D
{
    Float64Vector3D ReflectionNormal { get; }

    ILinFloat64HyperPlaneNormalReflectionLinearMap3D GetHyperPlaneNormalReflectionLinearMapInverse();

    LinFloat64HyperPlaneNormalReflection3D ToHyperPlaneNormalReflection();
}