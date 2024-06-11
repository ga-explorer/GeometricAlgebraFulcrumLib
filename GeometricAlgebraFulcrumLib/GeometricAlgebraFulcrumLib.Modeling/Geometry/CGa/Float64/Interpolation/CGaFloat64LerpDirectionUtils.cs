using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;

public static class CGaFloat64LerpDirectionUtils
{
    public static CGaFloat64Direction LerpLine2D(this double t, CGaFloat64Direction line1, CGaFloat64Direction line2)
    {
        Debug.Assert(
            line1.IsLine &&
            line2.IsLine
        );

        var s = 1d - t;

        var weight =
            s * line1.Weight + t * line2.Weight;

        var directionVector1 = line1.DirectionToVector2D();
        var directionVector2 = line2.DirectionToVector2D();

        var angle = directionVector1.GetAngleWithUnit(directionVector2).AngleTimes(t);

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return CGaFloat64GeometricSpace5D.Instance.DefineDirectionLine(
            weight,
            directionVector
        );
    }

    public static CGaFloat64Direction LerpLine3D(this double t, CGaFloat64Direction line1, CGaFloat64Direction line2)
    {
        Debug.Assert(
            line1.IsLine &&
            line2.IsLine
        );

        var s = 1d - t;

        var weight =
            s * line1.Weight + t * line2.Weight;

        var directionVector1 = line1.DirectionToVector3D();
        var directionVector2 = line2.DirectionToVector3D();

        var angle = directionVector1.GetAngleWithUnit(directionVector2).AngleTimes(t);

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return CGaFloat64GeometricSpace5D.Instance.DefineDirectionLine(
            weight,
            directionVector
        );
    }

    public static CGaFloat64Direction LerpPlane3D(this double t, CGaFloat64Direction plane1, CGaFloat64Direction plane2)
    {
        Debug.Assert(
            plane1.IsPlane &&
            plane2.IsPlane
        );

        var s = 1d - t;

        var weight =
            s * plane1.Weight + t * plane2.Weight;

        var normal1 = plane1.NormalDirectionToVector3D();
        var normal2 = plane2.NormalDirectionToVector3D();

        var angle = normal1.GetAngleWithUnit(normal2).AngleTimes(t);

        var directionBivector =
            normal1.RotateToUnitVector(normal2, angle).NormalToUnitDirection3D();

        return CGaFloat64GeometricSpace5D.Instance.DefineDirectionPlane(
            weight,
            directionBivector
        );
    }

}