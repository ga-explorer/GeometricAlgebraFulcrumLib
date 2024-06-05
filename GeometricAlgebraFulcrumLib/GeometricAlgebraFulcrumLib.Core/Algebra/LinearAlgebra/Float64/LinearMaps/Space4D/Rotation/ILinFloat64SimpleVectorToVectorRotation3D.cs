using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public interface ILinFloat64SimpleVectorToVectorRotation4D :
    ILinFloat64UnilinearMap4D
{
    LinFloat64Vector4D SourceVector { get; }

    LinFloat64Vector4D TargetOrthogonalVector { get; }

    LinFloat64Vector4D TargetVector { get; }

    double AngleCos { get; }

    LinFloat64PolarAngle Angle { get; }

    LinFloat64Vector4D GetMiddleUnitVector();

    Pair<LinFloat64HyperPlaneNormalReflection4D> GetHyperPlaneReflectionPair();

    LinFloat64Vector4D GetRotatedSourceVector(LinFloat64DirectedAngle angle1);

    Pair<LinFloat64Vector4D> GetRotatedSourceVectorPair(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2);
}