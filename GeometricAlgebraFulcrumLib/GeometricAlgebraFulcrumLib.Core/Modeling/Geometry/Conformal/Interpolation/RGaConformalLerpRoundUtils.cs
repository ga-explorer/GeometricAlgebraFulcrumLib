using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpRoundUtils
{
    public static RGaConformalRound LerpPoint2D(this double t, RGaConformalRound point1, RGaConformalRound point2)
    {
        Debug.Assert(
            point1.IsPoint &&
            point2.IsPoint
        );

        var s = 1d - t;

        var weight =
            s * point1.Weight + t * point2.Weight;

        var center =
            s * point1.CenterToVector2D() + t * point2.CenterToVector2D();

        return RGaConformalSpace4D.Instance.DefineRoundPoint(
            weight,
            center
        );
    }

    public static RGaConformalRound LerpPoint3D(this double t, RGaConformalRound point1, RGaConformalRound point2)
    {
        Debug.Assert(
            point1.IsPoint &&
            point2.IsPoint
        );

        var s = 1d - t;

        var weight =
            s * point1.Weight + t * point2.Weight;

        var center =
            s * point1.CenterToVector3D() + t * point2.CenterToVector3D();

        return RGaConformalSpace5D.Instance.DefineRoundPoint(
            weight,
            center
        );
    }

    public static RGaConformalRound LerpPointPair2D(this double t, RGaConformalRound pointPair1, RGaConformalRound pointPair2)
    {
        Debug.Assert(
            pointPair1.IsRoundPointPair &&
            pointPair2.IsRoundPointPair
        );

        var s = 1d - t;

        var weight =
            s * pointPair1.Weight + t * pointPair2.Weight;

        var radiusSquared =
            s * pointPair1.RadiusSquared + t * pointPair2.RadiusSquared;

        var center =
            s * pointPair1.CenterToVector2D() + t * pointPair2.CenterToVector2D();

        var directionVector1 = pointPair1.DirectionToVector2D();
        var directionVector2 = pointPair2.DirectionToVector2D();

        if (directionVector1.IsNearOppositeToUnit(directionVector2))
        {
            radiusSquared = 0;
        }

        var angle = directionVector1.GetAngleWithUnit(directionVector2).AngleTimes(t);

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return RGaConformalSpace4D.Instance.DefineRoundPointPair(
            weight,
            radiusSquared,
            center,
            directionVector
        );
    }

    public static RGaConformalRound LerpPointPair3D(this double t, RGaConformalRound pointPair1, RGaConformalRound pointPair2)
    {
        Debug.Assert(
            pointPair1.IsRoundPointPair &&
            pointPair2.IsRoundPointPair
        );

        var s = 1d - t;

        var weight =
            s * pointPair1.Weight + t * pointPair2.Weight;

        var radiusSquared =
            s * pointPair1.RadiusSquared + t * pointPair2.RadiusSquared;

        var center =
            s * pointPair1.CenterToVector3D() + t * pointPair2.CenterToVector3D();

        var directionVector1 = pointPair1.DirectionToVector3D();
        var directionVector2 = pointPair2.DirectionToVector3D();

        if (directionVector1.IsNearOppositeToUnit(directionVector2))
        {
            radiusSquared = 0;
        }

        var angle = directionVector1.GetAngleWithUnit(directionVector2).AngleTimes(t);

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return RGaConformalSpace5D.Instance.DefineRoundPointPair(
            weight,
            radiusSquared,
            center,
            directionVector
        );
    }

    public static RGaConformalRound LerpCircle2D(this double t, RGaConformalRound circle1, RGaConformalRound circle2)
    {
        Debug.Assert(
            circle1.IsRoundCircle &&
            circle2.IsRoundCircle
        );

        var s = 1d - t;

        var weight =
            s * circle1.Weight + t * circle2.Weight;

        var radiusSquared =
            s * circle1.RadiusSquared + t * circle2.RadiusSquared;

        var center =
            s * circle1.CenterToVector2D() + t * circle2.CenterToVector2D();

        return RGaConformalSpace4D.Instance.DefineRoundCircle(
            weight,
            radiusSquared,
            center,
            LinFloat64Bivector2D.E12
        );
    }

    public static RGaConformalRound LerpCircle3D(this double t, RGaConformalRound circle1, RGaConformalRound circle2)
    {
        Debug.Assert(
            circle1.IsRoundCircle &&
            circle2.IsRoundCircle
        );

        var s = 1d - t;

        var weight =
            s * circle1.Weight + t * circle2.Weight;

        var radiusSquared =
            s * circle1.RadiusSquared + t * circle2.RadiusSquared;

        var center =
            s * circle1.CenterToVector3D() + t * circle2.CenterToVector3D();

        var normal1 = circle1.NormalDirectionToVector3D();
        var normal2 = circle2.NormalDirectionToVector3D();

        if (normal1.IsNearOppositeToUnit(normal2))
        {
            radiusSquared = 0;
        }

        var angle = normal1.GetAngleWithUnit(normal2).AngleTimes(t);

        var directionBivector =
            normal1.RotateToUnitVector(normal2, angle).NormalToUnitDirection3D();

        return RGaConformalSpace5D.Instance.DefineRoundCircle(
            weight,
            radiusSquared,
            center,
            directionBivector
        );
    }

    public static RGaConformalRound LerpSphere3D(this double t, RGaConformalRound sphere1, RGaConformalRound sphere2)
    {
        Debug.Assert(
            sphere1.IsRoundSphere &&
            sphere2.IsRoundSphere
        );

        var s = 1d - t;

        var weight =
            s * sphere1.Weight + t * sphere2.Weight;

        var radiusSquared =
            s * sphere1.RadiusSquared + t * sphere2.RadiusSquared;

        var center =
            s * sphere1.CenterToVector3D() + t * sphere2.CenterToVector3D();

        return RGaConformalSpace5D.Instance.DefineRoundSphere(
            weight,
            radiusSquared,
            center
        );
    }
}
