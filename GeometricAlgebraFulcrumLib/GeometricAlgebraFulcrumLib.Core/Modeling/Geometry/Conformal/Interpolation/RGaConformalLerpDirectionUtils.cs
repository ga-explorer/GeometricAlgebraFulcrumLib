using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpDirectionUtils
{
    public static RGaConformalDirection LerpLine2D(this double t, RGaConformalDirection line1, RGaConformalDirection line2)
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

        return RGaConformalSpace5D.Instance.DefineDirectionLine(
            weight,
            directionVector
        );
    }

    public static RGaConformalDirection LerpLine3D(this double t, RGaConformalDirection line1, RGaConformalDirection line2)
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

        return RGaConformalSpace5D.Instance.DefineDirectionLine(
            weight,
            directionVector
        );
    }

    public static RGaConformalDirection LerpPlane3D(this double t, RGaConformalDirection plane1, RGaConformalDirection plane2)
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

        return RGaConformalSpace5D.Instance.DefineDirectionPlane(
            weight,
            directionBivector
        );
    }

}