using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;

public interface IHyperPlaneNormalReflectionLinearMap :
    IDirectionalScalingLinearMap
{
    Float64Tuple ReflectionNormal { get; }

    IHyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse();

    HyperPlaneNormalReflection ToHyperPlaneNormalReflection();
}