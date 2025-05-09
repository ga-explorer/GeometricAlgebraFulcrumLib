using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;

public static class Float64LineUtils
{
    #region Lines in 2D
    public static LinFloat64Vector2D GetOrigin(this IFloat64Line2D line)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)line.OriginX, (Float64Scalar)line.OriginY);
    }

    public static LinFloat64Vector2D GetDirection(this IFloat64Line2D line)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)line.DirectionX, (Float64Scalar)line.DirectionY);
    }

    public static LinFloat64Vector2D GetDirectionInv(this IFloat64Line2D line)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(1 / line.DirectionX), (Float64Scalar)(1 / line.DirectionY));
    }

    public static int[] GetDirectionSign(this IFloat64Line2D line)
    {
        return new[]
        {
            line.DirectionX < 0 ? 1 : 0,
            line.DirectionY < 0 ? 1 : 0
        };
    }

    public static double GetDirectionLength(this IFloat64Line2D line)
    {
        return Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );
    }

    public static double GetDirectionLengthSquared(this IFloat64Line2D line)
    {
        return line.DirectionX * line.DirectionX +
               line.DirectionY * line.DirectionY;
    }

    public static LinFloat64Vector2D GetUnitDirection(this IFloat64Line2D line)
    {
        return LinFloat64Vector2DComposerUtils.ToUnitLinVector2D(line.DirectionX, line.DirectionY);
    }

    public static Tuple<double, LinFloat64Vector2D> ToLengthAndUnitDirection(this IFloat64Line2D line)
    {
        var length = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            LinFloat64Vector2D.Create((Float64Scalar)(line.DirectionX * lengthInv),
                (Float64Scalar)(line.DirectionY * lengthInv))
        );
    }

    public static LinFloat64Vector2D GetNormal(this IFloat64Line2D line)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(-line.DirectionY), (Float64Scalar)line.DirectionX);
    }

    public static LinFloat64Vector2D GetUnitNormal(this IFloat64Line2D line)
    {
        var s = 1.0d / Math.Sqrt(line.DirectionX * line.DirectionX + line.DirectionY * line.DirectionY);

        return LinFloat64Vector2D.Create((Float64Scalar)(-line.DirectionY * s), (Float64Scalar)(line.DirectionX * s));
    }

    public static double GetDistanceToLineDirectionLength(this IFloat64Line2D line, double distance)
    {
        return distance / line.GetDirectionLength();
    }

    public static double GetDistanceToLineDirectionLengthSquared(this IFloat64Line2D line, double distanceSquared)
    {
        return Math.Sign(distanceSquared) * Math.Sqrt(Math.Abs(distanceSquared) / line.GetDirectionLengthSquared());
    }

    public static double GetDirectionLengthToDistance(this IFloat64Line2D line, double distance)
    {
        return line.GetDirectionLength() / distance;
    }

    public static Float64Line2D GetNormalLine(this IFloat64Line2D line)
    {
        return new Float64Line2D(
            line.OriginX,
            line.OriginY,
            -line.DirectionY,
            line.DirectionX
        );
    }

    public static Float64Line2D GetUnitNormalLine(this IFloat64Line2D line)
    {
        var s = 1.0d /
                Math.Sqrt(
                    line.DirectionX * line.DirectionX +
                    line.DirectionY * line.DirectionY
                );

        return new Float64Line2D(
            line.OriginX,
            line.OriginY,
            -line.DirectionY * s,
            line.DirectionX * s
        );
    }

    public static Float64LineComposer2D ToMutableLine(this IFloat64Line2D line)
    {
        return new Float64LineComposer2D(
            line.OriginX,
            line.OriginY,
            line.DirectionX,
            line.DirectionY
        );
    }

    public static LinFloat64Vector2D GetPointAt(this IFloat64Line2D line, double t)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(line.OriginX + t * line.DirectionX),
            (Float64Scalar)(line.OriginY + t * line.DirectionY));
    }

    public static IEnumerable<LinFloat64Vector2D> GetPointsAt(this IFloat64Line2D line, IEnumerable<double> tList)
    {
        return tList.Select(t => line.GetPointAt(t));
    }

    public static Float64LineSegment2D ToLineSegment(this IFloat64Line2D line)
    {
        return new Float64LineSegment2D(
            line.OriginX,
            line.OriginY,
            line.OriginX + line.DirectionX,
            line.OriginY + line.DirectionY
        );
    }

    public static Float64LineSegment2D ToLineSegment(this IFloat64Line2D line, double t)
    {
        return new Float64LineSegment2D(
            line.OriginX,
            line.OriginY,
            line.OriginX + line.DirectionX * t,
            line.OriginY + line.DirectionY * t
        );
    }

    public static Float64LineSegment2D ToLineSegment(this IFloat64Line2D line, double t1, double t2)
    {
        return new Float64LineSegment2D(
            line.OriginX + t1 * line.DirectionX,
            line.OriginY + t1 * line.DirectionY,
            line.OriginX + t2 * line.DirectionX,
            line.OriginY + t2 * line.DirectionY
        );
    }

    public static Float64Triangle2D ToTriangle(this IFloat64Line2D line, ILinFloat64Vector2D point3)
    {
        return new Float64Triangle2D(
            line.OriginX,
            line.OriginY,
            line.OriginX + line.DirectionX,
            line.OriginY + line.DirectionY,
            point3.X,
            point3.Y
        );
    }

    public static Float64Beam2D ToBeam(this IFloat64Line2D line, ILinFloat64Vector2D direction2)
    {
        return new Float64Beam2D(
            line.OriginX,
            line.OriginY,
            line.DirectionX,
            line.DirectionY,
            direction2.X,
            direction2.Y
        );
    }

    public static Float64Line2D ToLine(this IFloat64Line2D line, double directionScalingFactor)
    {
        return new Float64Line2D(
            line.OriginX,
            line.OriginY,
            line.DirectionX * directionScalingFactor,
            line.DirectionY * directionScalingFactor
        );
    }

    public static Float64Line2D ToLineInvDirection(this IFloat64Line2D line)
    {
        return new Float64Line2D(
            line.OriginX + line.DirectionX,
            line.OriginY + line.DirectionY,
            -line.DirectionX,
            -line.DirectionY
        );
    }

    #endregion

    #region Lines in 3D
    public static LinFloat64Vector3D GetOrigin(this IFloat64Line3D line)
    {
        return LinFloat64Vector3D.Create(line.OriginX,
            line.OriginY,
            line.OriginZ);
    }

    public static LinFloat64Vector3D GetDirection(this IFloat64Line3D line)
    {
        return LinFloat64Vector3D.Create(line.DirectionX,
            line.DirectionY,
            line.DirectionZ);
    }

    public static LinFloat64Vector3D GetDirectionInv(this IFloat64Line3D line)
    {
        return LinFloat64Vector3D.Create(1 / line.DirectionX,
            1 / line.DirectionY,
            1 / line.DirectionZ);
    }

    public static LinFloat64Vector2D GetNegativeDirection(this IFloat64Line2D line)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(-line.DirectionX),
            (Float64Scalar)(-line.DirectionY));
    }

    public static LinFloat64Vector3D GetNegativeDirection(this IFloat64Line3D line)
    {
        return LinFloat64Vector3D.Create(-line.DirectionX,
            -line.DirectionY,
            -line.DirectionZ);
    }

    public static int[] GetDirectionSign(this IFloat64Line3D line)
    {
        return new[]
        {
            line.DirectionX < 0 ? 1 : 0,
            line.DirectionY < 0 ? 1 : 0,
            line.DirectionZ < 0 ? 1 : 0
        };
    }

    public static double GetDirectionLength(this IFloat64Line3D line)
    {
        return Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY +
            line.DirectionZ * line.DirectionZ
        );
    }

    public static double GetDirectionLengthSquared(this IFloat64Line3D line)
    {
        return line.DirectionX * line.DirectionX +
               line.DirectionY * line.DirectionY +
               line.DirectionZ * line.DirectionZ;
    }

    public static LinFloat64Vector3D GetUnitDirection(this IFloat64Line3D line)
    {
        return LinFloat64Vector3DComposerUtils.ToUnitLinVector3D(
            line.DirectionX,
            line.DirectionY,
            line.DirectionZ
        );
    }

    public static Tuple<double, LinFloat64Vector3D> ToLengthAndUnitDirection(this IFloat64Line3D line)
    {
        var length = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY +
            line.DirectionZ * line.DirectionZ
        );

        var lengthInv = 1 / length;

        return Tuple.Create(
            length,
            LinFloat64Vector3D.Create(line.DirectionX * lengthInv,
                line.DirectionY * lengthInv,
                line.DirectionZ * lengthInv)
        );
    }

    public static double GetDistanceToLineDirectionLength(this IFloat64Line3D line, double distance)
    {
        return distance / line.GetDirectionLength();
    }

    public static double GetDistanceToLineDirectionLengthSquared(this IFloat64Line3D line, double distanceSquared)
    {
        return Math.Sign(distanceSquared) * Math.Sqrt(Math.Abs(distanceSquared) / line.GetDirectionLengthSquared());
    }

    public static double GetDirectionLengthToDistance(this IFloat64Line3D line, double distance)
    {
        return line.GetDirectionLength() / distance;
    }

    public static Float64LineComposer3D ToMutableLine(this IFloat64Line3D line)
    {
        return Float64LineComposer3D.Create(
            line.OriginX,
            line.OriginY,
            line.OriginZ,
            line.DirectionX,
            line.DirectionY,
            line.DirectionZ
        );
    }

    public static LinFloat64Vector3D GetPointAt(this IFloat64Line3D line, double t)
    {
        return LinFloat64Vector3D.Create(line.OriginX + t * line.DirectionX,
            line.OriginY + t * line.DirectionY,
            line.OriginZ + t * line.DirectionZ);
    }

    public static IEnumerable<LinFloat64Vector3D> GetPointsAt(this IFloat64Line3D line, IEnumerable<double> tList)
    {
        return tList.Select(line.GetPointAt);
    }

    public static LinFloat64Vector2D GetPointAtDistance(this IFloat64Line2D line, double distance)
    {
        var t = distance / line.GetDirectionLength();

        return LinFloat64Vector2D.Create((Float64Scalar)(line.OriginX + t * line.DirectionX),
            (Float64Scalar)(line.OriginY + t * line.DirectionY));
    }

    public static LinFloat64Vector3D GetPointAtDistance(this IFloat64Line3D line, double distance)
    {
        var t = distance / line.GetDirectionLength();

        return LinFloat64Vector3D.Create(line.OriginX + t * line.DirectionX,
            line.OriginY + t * line.DirectionY,
            line.OriginZ + t * line.DirectionZ);
    }

    public static IEnumerable<LinFloat64Vector3D> GetPointsAtDistances(this IFloat64Line3D line, IEnumerable<double> distancesList)
    {
        var g = 1 / line.GetDirectionLength();

        return distancesList.Select(d => line.GetPointAt(d * g));
    }

    public static Float64LineSegment3D ToLineSegment(this IFloat64Line3D line)
    {
        return new Float64LineSegment3D(
            line.OriginX,
            line.OriginY,
            line.OriginZ,
            line.OriginX + line.DirectionX,
            line.OriginY + line.DirectionY,
            line.OriginZ + line.DirectionZ
        );
    }

    public static Float64LineSegment3D ToLineSegment(this IFloat64Line3D line, double t)
    {
        return new Float64LineSegment3D(
            line.OriginX,
            line.OriginY,
            line.OriginZ,
            line.OriginX + t * line.DirectionX,
            line.OriginY + t * line.DirectionY,
            line.OriginZ + t * line.DirectionZ
        );
    }

    public static Float64LineSegment3D ToLineSegment(this IFloat64Line3D line, double t1, double t2)
    {
        return new Float64LineSegment3D(
            line.OriginX + t1 * line.DirectionX,
            line.OriginY + t1 * line.DirectionY,
            line.OriginZ + t1 * line.DirectionZ,
            line.OriginX + t2 * line.DirectionX,
            line.OriginY + t2 * line.DirectionY,
            line.OriginZ + t2 * line.DirectionZ
        );
    }

    public static Float64Triangle3D ToTriangle(this IFloat64Line3D line, ILinFloat64Vector3D point3)
    {
        return new Float64Triangle3D(
            line.OriginX,
            line.OriginY,
            line.OriginZ,
            line.OriginX + line.DirectionX,
            line.OriginY + line.DirectionY,
            line.OriginZ + line.DirectionZ,
            point3.X,
            point3.Y,
            point3.Z
        );
    }

    public static Float64Line3D ToLine(this IFloat64Line3D line, double directionScalingFactor)
    {
        return new Float64Line3D(
            line.OriginX,
            line.OriginY,
            line.OriginZ,
            line.DirectionX * directionScalingFactor,
            line.DirectionY * directionScalingFactor,
            line.DirectionZ * directionScalingFactor
        );
    }

    public static Float64Line3D ToLineInvDirection(this IFloat64Line3D line)
    {
        return new Float64Line3D(
            line.OriginX + line.DirectionX,
            line.OriginY + line.DirectionY,
            line.OriginZ + line.DirectionZ,
            -line.DirectionX,
            -line.DirectionY,
            -line.DirectionZ
        );
    }

    #endregion

    #region Line Segments in 2D
    public static double GetLength(this IFloat64LineSegment2D lineSegment)
    {
        var dx = lineSegment.Point2X - lineSegment.Point1X;
        var dy = lineSegment.Point2Y - lineSegment.Point1Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    public static double GetLengthSquared(this IFloat64LineSegment2D lineSegment)
    {
        var dx = lineSegment.Point2X - lineSegment.Point1X;
        var dy = lineSegment.Point2Y - lineSegment.Point1Y;

        return dx * dx + dy * dy;
    }

    public static LinFloat64Vector2D GetDirection12(this IFloat64LineSegment2D lineSegment)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(lineSegment.Point2X - lineSegment.Point1X),
            (Float64Scalar)(lineSegment.Point2Y - lineSegment.Point1Y));
    }

    public static LinFloat64Vector2D GetDirection21(this IFloat64LineSegment2D lineSegment)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(lineSegment.Point1X - lineSegment.Point2X),
            (Float64Scalar)(lineSegment.Point1Y - lineSegment.Point2Y));
    }

    public static Float64Line2D ToLine(this IFloat64LineSegment2D lineSegment)
    {
        return new Float64Line2D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y
        );
    }

    public static Float64Line2D ToLineInv(this IFloat64LineSegment2D lineSegment)
    {
        return new Float64Line2D(
            lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point1X - lineSegment.Point2X,
            lineSegment.Point1Y - lineSegment.Point2Y
        );
    }

    public static Float64LimitedLine2D ToLimitedLine(this IFloat64LineSegment2D lineSegment)
    {
        return new Float64LimitedLine2D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y,
            0, 1
        );
    }

    public static Float64LimitedLine2D ToLimitedLineInv(this IFloat64LineSegment2D lineSegment)
    {
        return new Float64LimitedLine2D(
            lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point1X - lineSegment.Point2X,
            lineSegment.Point1Y - lineSegment.Point2Y,
            0, 1
        );
    }

    public static LinFloat64Vector2D GetPoint1(this IFloat64LineSegment2D lineSegment)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)lineSegment.Point1X, (Float64Scalar)lineSegment.Point1Y);
    }

    public static LinFloat64Vector2D GetPoint2(this IFloat64LineSegment2D lineSegment)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)lineSegment.Point2X, (Float64Scalar)lineSegment.Point2Y);
    }

    public static Pair<LinFloat64Vector2D> GetEndPoints(this IFloat64LineSegment2D lineSegment)
    {
        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(
                (Float64Scalar)lineSegment.Point1X,
                (Float64Scalar)lineSegment.Point1Y
            ),
            LinFloat64Vector2D.Create(
                (Float64Scalar)lineSegment.Point2X,
                (Float64Scalar)lineSegment.Point2Y
            )
        );
    }

    public static LinFloat64Vector2D GetPointAt(this IFloat64LineSegment2D lineSegment, double t)
    {
        Debug.Assert(!double.IsNaN(t));

        var s = 1.0d - t;

        return LinFloat64Vector2D.Create((Float64Scalar)(s * lineSegment.Point1X + t * lineSegment.Point2X),
            (Float64Scalar)(s * lineSegment.Point1Y + t * lineSegment.Point2Y));
    }

    public static IEnumerable<LinFloat64Vector2D> GetPointsAt(this IFloat64LineSegment2D lineSegment, IEnumerable<double> tList)
    {
        return tList.Select(lineSegment.GetPointAt);
    }

    public static Float64Triangle2D ToTriangle(this IFloat64LineSegment2D lineSegment, LinFloat64Vector2D point3)
    {
        return new Float64Triangle2D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point2X,
            lineSegment.Point2Y,
            point3.X,
            point3.Y
        );
    }

    public static Float64LineSegment2D ToLineSegment(this IFloat64LineSegment2D lineSegment, double t1, double t2)
    {
        var s1 = 1.0d - t1;
        var s2 = 1.0d - t2;

        return new Float64LineSegment2D(
            s1 * lineSegment.Point1X + t1 * lineSegment.Point2X,
            s1 * lineSegment.Point1Y + t1 * lineSegment.Point2Y,
            s2 * lineSegment.Point1X + t2 * lineSegment.Point2X,
            s2 * lineSegment.Point1Y + t2 * lineSegment.Point2Y
        );
    }

    public static Float64LineSegment2D ToLineSegment(this IFloat64LineSegment2D lineSegment)
    {
        return new Float64LineSegment2D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point2X,
            lineSegment.Point2Y
        );
    }

    public static IEnumerable<Float64LineSegment2D> ToLineSegments(this IEnumerable<ILinFloat64Vector2D> polylinePoints, bool closedShape = true)
    {
        var pointsArray = polylinePoints.ToArray();
        var point1 = pointsArray[0];

        for (var i = 1; i < pointsArray.Length; i++)
        {
            var point2 = pointsArray[i];

            yield return new Float64LineSegment2D(
                point1.X,
                point1.Y,
                point2.X,
                point2.Y
            );

            point1 = point2;
        }

        if (closedShape)
        {
            var point2 = pointsArray[0];

            yield return new Float64LineSegment2D(
                point1.X,
                point1.Y,
                point2.X,
                point2.Y
            );
        }
    }

    public static LinFloat64Vector2D GetNormal(this IFloat64LineSegment2D lineSegment)
    {
        var direction = LinFloat64Vector2D.Create((Float64Scalar)(lineSegment.Point2X - lineSegment.Point1X),
            (Float64Scalar)(lineSegment.Point2Y - lineSegment.Point1Y));

        return LinFloat64Vector2D.Create(-direction.Y, direction.X);
    }

    public static LinFloat64Vector2D GetUnitNormal(this IFloat64LineSegment2D lineSegment)
    {
        var direction = LinFloat64Vector2D.Create((Float64Scalar)(lineSegment.Point2X - lineSegment.Point1X),
            (Float64Scalar)(lineSegment.Point2Y - lineSegment.Point1Y));

        var s = 1.0d / Math.Sqrt(
            direction.X * direction.X +
            direction.Y * direction.Y
        );

        return LinFloat64Vector2D.Create(-direction.Y * s, direction.X * s);
    }

    #endregion

    #region Line Segments in 3D
    public static double GetLength(this IFloat64LineSegment3D lineSegment)
    {
        var dx = lineSegment.Point2X - lineSegment.Point1X;
        var dy = lineSegment.Point2Y - lineSegment.Point1Y;
        var dz = lineSegment.Point2Z - lineSegment.Point1Z;

        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    public static double GetLengthSquared(this IFloat64LineSegment3D lineSegment)
    {
        var dx = lineSegment.Point2X - lineSegment.Point1X;
        var dy = lineSegment.Point2Y - lineSegment.Point1Y;
        var dz = lineSegment.Point2Z - lineSegment.Point1Z;

        return dx * dx + dy * dy + dz * dz;
    }

    public static LinFloat64Vector3D GetPoint1(this IFloat64LineSegment3D lineSegment)
    {
        return LinFloat64Vector3D.Create(lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point1Z);
    }

    public static LinFloat64Vector3D GetPoint2(this IFloat64LineSegment3D lineSegment)
    {
        return LinFloat64Vector3D.Create(lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point2Z);
    }

    public static LinFloat64Vector3D[] GetEndPoints(this IFloat64LineSegment3D lineSegment)
    {
        return new[]
        {
            LinFloat64Vector3D.Create(lineSegment.Point1X,
                lineSegment.Point1Y,
                lineSegment.Point1Z),
            LinFloat64Vector3D.Create(lineSegment.Point2X,
                lineSegment.Point2Y,
                lineSegment.Point2Z)
        };
    }

    public static IEnumerable<LinFloat64Vector3D> GetEndpoints(this IEnumerable<IFloat64LineSegment3D> lineSegmentsList)
    {
        return lineSegmentsList
            .SelectMany(s => s.GetEndPoints());
    }

    public static LinFloat64Vector3D GetPointAt(this IFloat64LineSegment3D lineSegment, double t)
    {
        Debug.Assert(!double.IsNaN(t));

        var s = 1.0d - t;

        return LinFloat64Vector3D.Create(s * lineSegment.Point1X + t * lineSegment.Point2X,
            s * lineSegment.Point1Y + t * lineSegment.Point2Y,
            s * lineSegment.Point1Z + t * lineSegment.Point2Z);
    }

    public static IEnumerable<LinFloat64Vector3D> GetPointsAt(this IFloat64LineSegment3D lineSegment, IEnumerable<double> tList)
    {
        return tList.Select(lineSegment.GetPointAt);
    }

    public static LinFloat64Vector3D GetDirection12(this IFloat64LineSegment3D lineSegment)
    {
        return LinFloat64Vector3D.Create(lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y,
            lineSegment.Point2Z - lineSegment.Point1Z);
    }

    public static LinFloat64Vector3D GetDirection21(this IFloat64LineSegment3D lineSegment)
    {
        return LinFloat64Vector3D.Create(lineSegment.Point1X - lineSegment.Point2X,
            lineSegment.Point1Y - lineSegment.Point2Y,
            lineSegment.Point1Z - lineSegment.Point2Z);
    }

    public static Float64Line3D ToLine(this IFloat64LineSegment3D lineSegment)
    {
        return new Float64Line3D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point1Z,
            lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y,
            lineSegment.Point2Z - lineSegment.Point1Z
        );
    }

    public static Float64Line3D ToLineInv(this IFloat64LineSegment3D lineSegment)
    {
        return new Float64Line3D(
            lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point2Z,
            lineSegment.Point1X - lineSegment.Point2X,
            lineSegment.Point1Y - lineSegment.Point2Y,
            lineSegment.Point1Z - lineSegment.Point2Z
        );
    }

    public static Float64LimitedLine3D ToLimitedLine(this IFloat64LineSegment3D lineSegment)
    {
        return new Float64LimitedLine3D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point1Z,
            lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y,
            lineSegment.Point2Z - lineSegment.Point1Z,
            0, 1
        );
    }

    public static Float64LimitedLine3D ToLimitedLineInv(this IFloat64LineSegment3D lineSegment)
    {
        return new Float64LimitedLine3D(
            lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point2Z,
            lineSegment.Point1X - lineSegment.Point2X,
            lineSegment.Point1Y - lineSegment.Point2Y,
            lineSegment.Point1Z - lineSegment.Point2Z,
            0, 1
        );
    }

    public static Float64Triangle3D ToTriangle(this IFloat64LineSegment3D lineSegment, LinFloat64Vector3D point3)
    {
        return new Float64Triangle3D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point1Z,
            lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point2Z,
            point3.X,
            point3.Y,
            point3.Z
        );
    }

    public static Float64LineSegment3D ToLineSegment(this IFloat64LineSegment3D lineSegment, double t1, double t2)
    {
        var s1 = 1.0d - t1;
        var s2 = 1.0d - t2;

        return new Float64LineSegment3D(
            s1 * lineSegment.Point1X + t1 * lineSegment.Point2X,
            s1 * lineSegment.Point1Y + t1 * lineSegment.Point2Y,
            s1 * lineSegment.Point1Z + t1 * lineSegment.Point2Z,
            s2 * lineSegment.Point1X + t2 * lineSegment.Point2X,
            s2 * lineSegment.Point1Y + t2 * lineSegment.Point2Y,
            s2 * lineSegment.Point1Z + t2 * lineSegment.Point2Z
        );
    }

    public static Float64LineSegment3D ToLineSegment(this IFloat64LineSegment3D lineSegment)
    {
        return new Float64LineSegment3D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point1Z,
            lineSegment.Point2X,
            lineSegment.Point2Y,
            lineSegment.Point2Z
        );
    }

    public static IEnumerable<Float64LineSegment3D> ToLineSegments(this IEnumerable<ILinFloat64Vector3D> polylinePoints, bool closedShape = true)
    {
        var pointsArray = polylinePoints.ToArray();
        var point1 = pointsArray[0];

        for (var i = 1; i < pointsArray.Length; i++)
        {
            var point2 = pointsArray[i];

            yield return new Float64LineSegment3D(
                point1.X,
                point1.Y,
                point1.Z,
                point2.X,
                point2.Y,
                point2.Z
            );

            point1 = point2;
        }

        if (closedShape)
        {
            var point2 = pointsArray[0];

            yield return new Float64LineSegment3D(
                point1.X,
                point1.Y,
                point1.Z,
                point2.X,
                point2.Y,
                point2.Z
            );
        }
    }
    #endregion


    #region Operations on Beams

    public static LinFloat64Vector2D GetOrigin(this IFloat64Beam2D beam)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)beam.OriginX,
            (Float64Scalar)beam.OriginY);
    }

    public static LinFloat64Vector2D GetDirection1(this IFloat64Beam2D beam)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)beam.Direction1X,
            (Float64Scalar)beam.Direction1Y);
    }

    public static LinFloat64Vector2D GetDirection2(this IFloat64Beam2D beam)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)beam.Direction2X,
            (Float64Scalar)beam.Direction2Y);
    }

    public static Float64Line2D GetRay1(this IFloat64Beam2D beam)
    {
        return new Float64Line2D(
            beam.OriginX,
            beam.OriginY,
            beam.Direction1X,
            beam.Direction1Y
        );
    }

    public static Float64Line2D GetRay2(this IFloat64Beam2D beam)
    {
        return new Float64Line2D(
            beam.OriginX,
            beam.OriginY,
            beam.Direction2X,
            beam.Direction2Y
        );
    }


    public static LinFloat64Vector2D GetNormal1(this IFloat64Beam2D beam)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(-beam.Direction1Y),
            (Float64Scalar)beam.Direction1X);
    }

    public static LinFloat64Vector2D GetUnitNormal1(this IFloat64Beam2D beam)
    {
        var s = 1.0d / Math.Sqrt(
            beam.Direction1X * beam.Direction1X +
            beam.Direction1Y * beam.Direction1Y
        );

        return LinFloat64Vector2D.Create((Float64Scalar)(-beam.Direction1Y * s),
            (Float64Scalar)(beam.Direction1X * s));
    }

    public static LinFloat64Vector2D GetNormal2(this IFloat64Beam2D beam)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(-beam.Direction2Y),
            (Float64Scalar)beam.Direction2X);
    }

    public static LinFloat64Vector2D GetUnitNormal2(this IFloat64Beam2D beam)
    {
        var s = 1.0d / Math.Sqrt(
            beam.Direction2X * beam.Direction2X +
            beam.Direction2Y * beam.Direction2Y
        );

        return LinFloat64Vector2D.Create((Float64Scalar)(-beam.Direction2Y * s),
            (Float64Scalar)(beam.Direction2X * s));
    }

    public static Float64Line2D GetNormalRay1(this IFloat64Beam2D beam)
    {
        return Float64Line2D.Create(
            beam.GetOrigin(),
            beam.GetNormal1()
        );
    }

    public static Float64Line2D GetUnitNormalRay1(this IFloat64Beam2D beam)
    {
        return Float64Line2D.Create(
            beam.GetOrigin(),
            beam.GetUnitNormal1()
        );
    }

    public static Float64Line2D GetNormalRay2(this IFloat64Beam2D beam)
    {
        return Float64Line2D.Create(
            beam.GetOrigin(),
            beam.GetNormal2()
        );
    }

    public static Float64Line2D GetUnitNormalRay2(this IFloat64Beam2D beam)
    {
        return Float64Line2D.Create(
            beam.GetOrigin(),
            beam.GetUnitNormal2()
        );
    }

    public static LinFloat64Vector2D GetPoint(this IFloat64Beam2D beam, double tPointX, double tPointY)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(beam.OriginX + tPointX * beam.Direction1X + tPointY * beam.Direction2X),
            (Float64Scalar)(beam.OriginY + tPointX * beam.Direction1Y + tPointY * beam.Direction2Y));
    }

    public static IEnumerable<LinFloat64Vector2D> GetPoints(this IFloat64Beam2D beam, IEnumerable<LinFloat64Vector2D> tPointsList)
    {
        return tPointsList.Select(
            tPoint => beam.GetPoint(tPoint.X, tPoint.Y)
        );
    }

    public static Float64LineSegment2D GetLineSegment(this IFloat64Beam2D beam, double tPoint1, double tPoint2)
    {
        return new Float64LineSegment2D(
            beam.OriginX + beam.Direction1X * tPoint1,
            beam.OriginY + beam.Direction1Y * tPoint1,
            beam.OriginX + beam.Direction2X * tPoint2,
            beam.OriginY + beam.Direction2Y * tPoint2
        );
    }

    public static Float64LineSegment2D GetLineSegment(this IFloat64Beam2D beam, LinFloat64Vector2D tPoint1, LinFloat64Vector2D tPoint2)
    {
        var point1 = beam.GetPoint(tPoint1.X, tPoint1.Y);
        var point2 = beam.GetPoint(tPoint2.X, tPoint2.Y);

        return new Float64LineSegment2D(
            point1.X, point1.Y,
            point2.X, point2.Y
        );
    }

    public static Float64Line2D GetRay(this IFloat64Beam2D beam, double tOrigin, double tDirection)
    {
        var s = 1.0d - tDirection;
        var directionX = s * beam.Direction1X + tDirection * beam.Direction2X;
        var directionY = s * beam.Direction1Y + tDirection * beam.Direction2Y;

        return new Float64Line2D(
            beam.OriginX + directionX * tOrigin,
            beam.OriginY + directionY * tOrigin,
            directionX,
            directionY
        );
    }

    public static Float64Line2D GetRay(this IFloat64Beam2D beam, LinFloat64Vector2D tPoint1, LinFloat64Vector2D tPoint2)
    {
        var point1 = beam.GetPoint(tPoint1.X, tPoint1.Y);
        var point2 = beam.GetPoint(tPoint2.X, tPoint2.Y);

        return new Float64Line2D(
            point1.X, point1.Y,
            point2.X - point1.X, point2.Y - point1.Y
        );
    }

    public static Float64Beam2D GetBeam(this IFloat64Beam2D beam, double tDirection1, double tDirection2)
    {
        var sDirection1 = 1.0d - tDirection1;
        var sDirection2 = 1.0d - tDirection2;

        return new Float64Beam2D(
            beam.OriginX,
            beam.OriginY,
            sDirection1 * beam.Direction1X + tDirection1 * beam.Direction2X,
            sDirection1 * beam.Direction1Y + tDirection1 * beam.Direction2Y,
            sDirection2 * beam.Direction1X + tDirection2 * beam.Direction2X,
            sDirection2 * beam.Direction1Y + tDirection2 * beam.Direction2Y
        );
    }

    public static Float64Triangle2D GetTriangle(this IFloat64Beam2D beam)
    {
        return new Float64Triangle2D(
            beam.OriginX,
            beam.OriginY,
            beam.OriginX + beam.Direction1X,
            beam.OriginY + beam.Direction1Y,
            beam.OriginX + beam.Direction2X,
            beam.OriginY + beam.Direction2Y
        );
    }

    public static Float64Triangle2D GetTriangle(this IFloat64Beam2D beam, double tDirection1, double tDirection2)
    {
        return new Float64Triangle2D(
            beam.OriginX,
            beam.OriginY,
            beam.OriginX + beam.Direction1X * tDirection1,
            beam.OriginY + beam.Direction1Y * tDirection1,
            beam.OriginX + beam.Direction2X * tDirection2,
            beam.OriginY + beam.Direction2Y * tDirection2
        );
    }

    public static Float64Triangle2D GetTriangle(this IFloat64Beam2D beam, LinFloat64Vector2D tPoint1, LinFloat64Vector2D tPoint2, LinFloat64Vector2D tPoint3)
    {
        var point1 = beam.GetPoint(tPoint1.X, tPoint1.Y);
        var point2 = beam.GetPoint(tPoint2.X, tPoint2.Y);
        var point3 = beam.GetPoint(tPoint3.X, tPoint3.Y);

        return new Float64Triangle2D(
            point1.X,
            point1.Y,
            point2.X,
            point2.Y,
            point3.X,
            point3.Y
        );
    }

    #endregion

    #region Operaions on LinePairs

    public static LinFloat64Vector2D GetOrigin1(this IFloat64LinePair2D linePair)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)linePair.Origin1X,
            (Float64Scalar)linePair.Origin1Y);
    }

    public static LinFloat64Vector2D GetOrigin2(this IFloat64LinePair2D linePair)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)linePair.Origin2X,
            (Float64Scalar)linePair.Origin2Y);
    }


    public static LinFloat64Vector2D GetDirection1(this IFloat64LinePair2D linePair)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)linePair.Direction1X,
            (Float64Scalar)linePair.Direction1Y);
    }

    public static LinFloat64Vector2D GetDirection2(this IFloat64LinePair2D linePair)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)linePair.Direction2X,
            (Float64Scalar)linePair.Direction2Y);
    }

    public static Float64Line2D GetRay1(this IFloat64LinePair2D linePair)
    {
        return new Float64Line2D(
            linePair.Origin1X,
            linePair.Origin1Y,
            linePair.Direction1X,
            linePair.Direction1Y
        );
    }

    public static Float64Line2D GetRay2(this IFloat64LinePair2D linePair)
    {
        return new Float64Line2D(
            linePair.Origin2X,
            linePair.Origin2Y,
            linePair.Direction2X,
            linePair.Direction2Y
        );
    }

    public static LinFloat64Vector3D GetOrigin1(this IFloat64LinePair3D linePair)
    {
        return LinFloat64Vector3D.Create(linePair.Origin1X,
            linePair.Origin1Y,
            linePair.Origin1Z);
    }

    public static LinFloat64Vector3D GetOrigin2(this IFloat64LinePair3D linePair)
    {
        return LinFloat64Vector3D.Create(linePair.Origin2X,
            linePair.Origin2Y,
            linePair.Origin2Z);
    }

    public static LinFloat64Vector3D GetDirection1(this IFloat64LinePair3D linePair)
    {
        return LinFloat64Vector3D.Create(linePair.Direction1X,
            linePair.Direction1Y,
            linePair.Direction1Z);
    }

    public static LinFloat64Vector3D GetDirection2(this IFloat64LinePair3D linePair)
    {
        return LinFloat64Vector3D.Create(linePair.Direction2X,
            linePair.Direction2Y,
            linePair.Direction2Z);
    }

    public static Float64Line3D GetRay1(this IFloat64LinePair3D linePair)
    {
        return new Float64Line3D(
            linePair.Origin1X,
            linePair.Origin1Y,
            linePair.Origin1Z,
            linePair.Direction1X,
            linePair.Direction1Y,
            linePair.Direction1Z
        );
    }

    public static Float64Line3D GetRay2(this IFloat64LinePair3D linePair)
    {
        return new Float64Line3D(
            linePair.Origin2X,
            linePair.Origin2Y,
            linePair.Origin2Z,
            linePair.Direction2X,
            linePair.Direction2Y,
            linePair.Direction2Z
        );
    }

    public static Float64Line3D GetRay(this IFloat64LinePair3D linePair, double tOrigin, double tDirection)
    {
        var sOrigin = 1.0d - tOrigin;
        var sDirection = 1.0d - tDirection;

        return new Float64Line3D(
            sOrigin * linePair.Origin1X + tOrigin * linePair.Origin2X,
            sOrigin * linePair.Origin1Y + tOrigin * linePair.Origin2Y,
            sOrigin * linePair.Origin1Z + tOrigin * linePair.Origin2Z,

            sDirection * linePair.Direction1X + tDirection * linePair.Direction2X,
            sDirection * linePair.Direction1Y + tDirection * linePair.Direction2Y,
            sDirection * linePair.Direction1Z + tDirection * linePair.Direction2Z
        );
    }

    #endregion
}