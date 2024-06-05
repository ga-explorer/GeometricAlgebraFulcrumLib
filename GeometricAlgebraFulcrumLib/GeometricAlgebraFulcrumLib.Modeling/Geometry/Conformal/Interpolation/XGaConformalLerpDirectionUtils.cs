using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpDirectionUtils
{
    public static XGaConformalDirection<T> LerpLine2D<T>(this Scalar<T> t, XGaConformalDirection<T> line1, XGaConformalDirection<T> line2)
    {
        Debug.Assert(
            line1.IsLine &&
            line2.IsLine
        );

        var s = 1 - t;

        var weight =
            s * line1.Weight + t * line2.Weight;

        var directionVector1 = line1.DirectionToVector2D();
        var directionVector2 = line2.DirectionToVector2D();

        var angle = t.ScalarProcessor.CreateAngle(
            directionVector1.GetAngleWithUnit(directionVector2).Radians * t
        );

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return line1.ConformalSpace4D.DefineDirectionLine(
            weight,
            directionVector
        );
    }

    public static XGaConformalDirection<T> LerpLine3D<T>(this Scalar<T> t, XGaConformalDirection<T> line1, XGaConformalDirection<T> line2)
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

        var angle = t.ScalarProcessor.CreateAngle(
            directionVector1.GetAngleWithUnit(directionVector2).Radians * t
        );

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return line1.ConformalSpace5D.DefineDirectionLine(
            weight,
            directionVector
        );
    }

    public static XGaConformalDirection<T> LerpPlane3D<T>(this Scalar<T> t, XGaConformalDirection<T> plane1, XGaConformalDirection<T> plane2)
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

        var angle = t.ScalarProcessor.CreateAngle(
            normal1.GetAngleWithUnit(normal2).Radians * t
        );

        var directionBivector =
            normal1.RotateToUnitVector(normal2, angle).NormalToUnitDirection3D();

        return plane1.ConformalSpace5D.DefineDirectionPlane(
            weight,
            directionBivector
        );
    }

}