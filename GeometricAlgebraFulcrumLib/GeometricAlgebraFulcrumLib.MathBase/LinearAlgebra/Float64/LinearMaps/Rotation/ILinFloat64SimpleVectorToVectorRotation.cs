using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public interface ILinFloat64SimpleVectorToVectorRotation :
        ILinFloat64UnilinearMap
    {
        LinFloat64Vector SourceVector { get; }

        LinFloat64Vector TargetOrthogonalVector { get; }

        LinFloat64Vector TargetVector { get; }

        double AngleCos { get; }

        Float64PlanarAngle Angle { get; }

        LinFloat64Vector GetMiddleUnitVector();

        Pair<LinFloat64HyperPlaneNormalReflection> GetHyperPlaneReflectionPair();

        LinFloat64Vector GetRotatedSourceVector(Float64PlanarAngle angle1);

        Pair<LinFloat64Vector> GetRotatedSourceVectorPair(Float64PlanarAngle angle1, Float64PlanarAngle angle2);
    }
}