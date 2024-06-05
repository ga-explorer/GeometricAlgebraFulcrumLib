using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpFlatUtils
{
    public static XGaConformalFlat<T> LerpPoint2D<T>(this Scalar<T> t, XGaConformalFlat<T> point1, XGaConformalFlat<T> point2)
    {
        Debug.Assert(
            point1.IsPoint &&
            point2.IsPoint
        );

        var s = 1d - t;

        var weight =
            s * point1.Weight + t * point2.Weight;

        var position =
            s * point1.PositionToVector2D() + t * point2.PositionToVector2D();

        return point1.ConformalSpace5D.DefineFlatPoint(
            weight,
            position
        );
    }

    public static XGaConformalFlat<T> LerpPoint3D<T>(this Scalar<T> t, XGaConformalFlat<T> point1, XGaConformalFlat<T> point2)
    {
        Debug.Assert(
            point1.IsPoint &&
            point2.IsPoint
        );

        var s = 1d - t;

        var weight =
            s * point1.Weight + t * point2.Weight;

        var position =
            s * point1.PositionToVector3D() + t * point2.PositionToVector3D();

        return point1.ConformalSpace5D.DefineFlatPoint(
            weight,
            position
        );
    }

    public static XGaConformalFlat<T> LerpLine2D<T>(this Scalar<T> t, XGaConformalFlat<T> line1, XGaConformalFlat<T> line2)
    {
        Debug.Assert(
            line1.IsLine &&
            line2.IsLine
        );

        var s = 1d - t;

        var weight =
            s * line1.Weight + t * line2.Weight;

        var position =
            s * line1.PositionToVector2D() + t * line2.PositionToVector2D();

        var directionVector1 = line1.DirectionToVector2D();
        var directionVector2 = line2.DirectionToVector2D();

        var angle = t.ScalarProcessor.CreateAngle(
            directionVector1.GetAngleWithUnit(directionVector2).Radians * t
        );

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return line1.ConformalSpace4D.DefineFlatLine(
            weight,
            position,
            directionVector
        );
    }

    public static XGaConformalFlat<T> LerpLine3D<T>(this Scalar<T> t, XGaConformalFlat<T> line1, XGaConformalFlat<T> line2)
    {
        Debug.Assert(
            line1.IsLine &&
            line2.IsLine
        );

        var s = 1d - t;

        var weight =
            s * line1.Weight + t * line2.Weight;

        var position =
            s * line1.PositionToVector3D() + t * line2.PositionToVector3D();

        var directionVector1 = line1.DirectionToVector3D();
        var directionVector2 = line2.DirectionToVector3D();

        var angle = t.ScalarProcessor.CreateAngle(
            directionVector1.GetAngleWithUnit(directionVector2).Radians * t
        );

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return line1.ConformalSpace5D.DefineFlatLine(
            weight,
            position,
            directionVector
        );
    }

    public static XGaConformalFlat<T> LerpPlane3D<T>(this Scalar<T> t, XGaConformalFlat<T> plane1, XGaConformalFlat<T> plane2)
    {
        Debug.Assert(
            plane1.IsPlane &&
            plane2.IsPlane
        );

        var s = 1d - t;

        var weight =
            s * plane1.Weight + t * plane2.Weight;

        var position =
            s * plane1.PositionToVector3D() + t * plane2.PositionToVector3D();

        var normal1 = plane1.NormalDirectionToVector3D();
        var normal2 = plane2.NormalDirectionToVector3D();

        var angle = t.ScalarProcessor.CreateAngle(
            normal1.GetAngleWithUnit(normal2).Radians * t
        );

        var directionBivector =
            normal1.RotateToUnitVector(normal2, angle).NormalToUnitDirection3D();

        return plane1.ConformalSpace5D.DefineFlatPlane(
            weight,
            position,
            directionBivector
        );
    }

}