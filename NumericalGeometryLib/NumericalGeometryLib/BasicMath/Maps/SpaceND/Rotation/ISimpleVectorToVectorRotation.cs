using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

public interface ISimpleVectorToVectorRotation :
    ILinearMap
{
    Float64Tuple SourceVector { get; }

    Float64Tuple TargetOrthogonalVector { get; }

    Float64Tuple TargetVector { get; }

    double AngleCos { get; }

    PlanarAngle Angle { get; }

    Float64Tuple GetMiddleUnitVector();

    Pair<HyperPlaneNormalReflection> GetHyperPlaneReflectionPair();

    Float64Tuple GetRotatedSourceVector(PlanarAngle angle1);

    Pair<Float64Tuple> GetRotatedSourceVectorPair(PlanarAngle angle1, PlanarAngle angle2);
}