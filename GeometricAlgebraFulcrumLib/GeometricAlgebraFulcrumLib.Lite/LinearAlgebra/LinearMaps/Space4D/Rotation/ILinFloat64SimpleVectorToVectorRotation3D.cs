using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Rotation;

public interface ILinFloat64SimpleVectorToVectorRotation4D :
    ILinFloat64UnilinearMap4D
{
    Float64Vector4D SourceVector { get; }

    Float64Vector4D TargetOrthogonalVector { get; }

    Float64Vector4D TargetVector { get; }

    double AngleCos { get; }

    Float64PlanarAngle Angle { get; }

    Float64Vector4D GetMiddleUnitVector();

    Pair<LinFloat64HyperPlaneNormalReflection4D> GetHyperPlaneReflectionPair();

    Float64Vector4D GetRotatedSourceVector(Float64PlanarAngle angle1);

    Pair<Float64Vector4D> GetRotatedSourceVectorPair(Float64PlanarAngle angle1, Float64PlanarAngle angle2);
}