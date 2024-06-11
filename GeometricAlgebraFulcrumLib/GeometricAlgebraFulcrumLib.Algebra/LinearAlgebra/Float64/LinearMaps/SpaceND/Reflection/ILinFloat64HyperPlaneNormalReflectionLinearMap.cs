using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public interface ILinFloat64HyperPlaneNormalReflectionLinearMap :
    ILinFloat64DirectionalScalingLinearMap
{
    LinFloat64Vector ReflectionNormal { get; }

    ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse();

    LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection();
}