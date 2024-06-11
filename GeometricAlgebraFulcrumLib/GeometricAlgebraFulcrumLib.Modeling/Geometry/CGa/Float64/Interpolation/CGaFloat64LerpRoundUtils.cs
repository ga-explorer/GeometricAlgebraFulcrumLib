using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;

public static class CGaFloat64LerpRoundUtils
{
    public static CGaFloat64Round LerpPoint2D(this double t, CGaFloat64Round point1, CGaFloat64Round point2)
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

        return CGaFloat64GeometricSpace4D.Instance.DefineRoundPoint(
            weight,
            center
        );
    }

    public static CGaFloat64Round LerpPoint3D(this double t, CGaFloat64Round point1, CGaFloat64Round point2)
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

        return CGaFloat64GeometricSpace5D.Instance.DefineRoundPoint(
            weight,
            center
        );
    }

    public static CGaFloat64Round LerpPointPair2D(this double t, CGaFloat64Round pointPair1, CGaFloat64Round pointPair2)
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

        return CGaFloat64GeometricSpace4D.Instance.DefineRoundPointPair(
            weight,
            radiusSquared,
            center,
            directionVector
        );
    }

    public static CGaFloat64Round LerpPointPair3D(this double t, CGaFloat64Round pointPair1, CGaFloat64Round pointPair2)
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

        return CGaFloat64GeometricSpace5D.Instance.DefineRoundPointPair(
            weight,
            radiusSquared,
            center,
            directionVector
        );
    }

    public static CGaFloat64Round LerpCircle2D(this double t, CGaFloat64Round circle1, CGaFloat64Round circle2)
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

        return CGaFloat64GeometricSpace4D.Instance.DefineRoundCircle(
            weight,
            radiusSquared,
            center,
            LinFloat64Bivector2D.E12
        );
    }

    public static CGaFloat64Round LerpCircle3D(this double t, CGaFloat64Round circle1, CGaFloat64Round circle2)
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

        return CGaFloat64GeometricSpace5D.Instance.DefineRoundCircle(
            weight,
            radiusSquared,
            center,
            directionBivector
        );
    }

    public static CGaFloat64Round LerpSphere3D(this double t, CGaFloat64Round sphere1, CGaFloat64Round sphere2)
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

        return CGaFloat64GeometricSpace5D.Instance.DefineRoundSphere(
            weight,
            radiusSquared,
            center
        );
    }
}
