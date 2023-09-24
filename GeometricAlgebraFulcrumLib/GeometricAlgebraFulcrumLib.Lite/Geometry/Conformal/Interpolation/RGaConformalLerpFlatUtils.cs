using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpFlatUtils
{
    public static RGaConformalFlat LerpPoint2D(this double t, RGaConformalFlat point1, RGaConformalFlat point2)
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

        return RGaConformalSpace5D.Instance.DefineFlatPoint(
            weight,
            position
        );
    }

    public static RGaConformalFlat LerpPoint3D(this double t, RGaConformalFlat point1, RGaConformalFlat point2)
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

        return RGaConformalSpace5D.Instance.DefineFlatPoint(
            weight,
            position
        );
    }

    public static RGaConformalFlat LerpLine2D(this double t, RGaConformalFlat line1, RGaConformalFlat line2)
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

        var angle = directionVector1.GetAngleWithUnit(directionVector2) * t;

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return RGaConformalSpace5D.Instance.DefineFlatLine(
            weight,
            position,
            directionVector
        );
    }

    public static RGaConformalFlat LerpLine3D(this double t, RGaConformalFlat line1, RGaConformalFlat line2)
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

        var angle = directionVector1.GetAngleWithUnit(directionVector2) * t;

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return RGaConformalSpace5D.Instance.DefineFlatLine(
            weight,
            position,
            directionVector
        );
    }

    public static RGaConformalFlat LerpPlane3D(this double t, RGaConformalFlat plane1, RGaConformalFlat plane2)
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

        var angle = normal1.GetAngleWithUnit(normal2) * t;

        var directionBivector =
            normal1.RotateToUnitVector(normal2, angle).UnDual3D();

        return RGaConformalSpace5D.Instance.DefineFlatPlane(
            weight,
            position,
            directionBivector
        );
    }

}