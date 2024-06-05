using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;

public interface ILinFloat64HyperPlaneNormalReflectionLinearMap3D :
    ILinFloat64DirectionalScalingLinearMap3D
{
    LinFloat64Vector3D ReflectionNormal { get; }

    ILinFloat64HyperPlaneNormalReflectionLinearMap3D GetHyperPlaneNormalReflectionLinearMapInverse();

    LinFloat64HyperPlaneNormalReflection3D ToHyperPlaneNormalReflection();
}