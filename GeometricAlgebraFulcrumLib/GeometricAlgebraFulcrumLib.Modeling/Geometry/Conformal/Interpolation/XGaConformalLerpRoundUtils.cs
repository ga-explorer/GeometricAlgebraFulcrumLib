using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpRoundUtils
{
    public static XGaConformalRound<T> LerpPoint2D<T>(this Scalar<T> t, XGaConformalRound<T> point1, XGaConformalRound<T> point2)
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
        
        return point1.ConformalSpace4D.DefineRoundPoint(
            weight,
            center
        );
    }

    public static XGaConformalRound<T> LerpPoint3D<T>(this Scalar<T> t, XGaConformalRound<T> point1, XGaConformalRound<T> point2)
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

        return point1.ConformalSpace5D.DefineRoundPoint(
            weight,
            center
        );
    }

    public static XGaConformalRound<T> LerpPointPair2D<T>(this Scalar<T> t, XGaConformalRound<T> pointPair1, XGaConformalRound<T> pointPair2)
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

        if (directionVector1.VectorIsNearOppositeToUnit(directionVector2))
        {
            radiusSquared = pointPair1.ConformalSpace.ScalarZero;
        }

        var angle = t.ScalarProcessor.CreateAngle(
            directionVector1.GetAngleWithUnit(directionVector2).Radians * t
        );

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return pointPair1.ConformalSpace4D.DefineRoundPointPair(
            weight,
            radiusSquared,
            center,
            directionVector
        );
    }

    public static XGaConformalRound<T> LerpPointPair3D<T>(this Scalar<T> t, XGaConformalRound<T> pointPair1, XGaConformalRound<T> pointPair2)
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
            radiusSquared = pointPair1.ConformalSpace.ScalarZero;
        }

        var angle = t.ScalarProcessor.CreateAngle(
            directionVector1.GetAngleWithUnit(directionVector2).Radians * t
        );

        var directionVector =
            directionVector1.RotateToUnitVector(directionVector2, angle);

        return pointPair1.ConformalSpace5D.DefineRoundPointPair(
            weight,
            radiusSquared,
            center,
            directionVector
        );
    }

    public static XGaConformalRound<T> LerpCircle2D<T>(this Scalar<T> t, XGaConformalRound<T> circle1, XGaConformalRound<T> circle2)
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

        return circle1.ConformalSpace4D.DefineRoundCircle(
            weight,
            radiusSquared,
            center,
            LinBivector2D<T>.E12(circle1.ScalarProcessor)
        );
    }

    public static XGaConformalRound<T> LerpCircle3D<T>(this Scalar<T> t, XGaConformalRound<T> circle1, XGaConformalRound<T> circle2)
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
            radiusSquared = circle1.ConformalSpace.ScalarZero;
        }

        var angle = t.ScalarProcessor.CreateAngle(
            normal1.GetAngleWithUnit(normal2).Radians * t
        );

        var directionBivector =
            normal1.RotateToUnitVector(normal2, angle).NormalToUnitDirection3D();

        return circle1.ConformalSpace5D.DefineRoundCircle(
            weight,
            radiusSquared,
            center,
            directionBivector
        );
    }

    public static XGaConformalRound<T> LerpSphere3D<T>(this Scalar<T> t, XGaConformalRound<T> sphere1, XGaConformalRound<T> sphere2)
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

        return sphere1.ConformalSpace5D.DefineRoundSphere(
            weight,
            radiusSquared,
            center
        );
    }
}
