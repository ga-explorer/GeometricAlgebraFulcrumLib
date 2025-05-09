﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Planes.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public static class Float64DistanceUtils
{
    //TODO: Implement these using GA instead of VA
    public static double GetSignedDistanceToLineVa(this ILinFloat64Vector2D point, IFloat64Line2D line)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var t1 = line.DirectionY * point.X -
                 line.DirectionX * point.Y +
                 (line.OriginX + line.DirectionX) * line.OriginY -
                 (line.OriginY + line.DirectionY) * line.OriginX;

        var t2 = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        return t1 / t2;
    }

    public static double GetSignedDistanceToLineVa(this ILinFloat64Vector2D point, ILinFloat64Vector2D linePoint1, ILinFloat64Vector2D linePoint2)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var lineDirectionX = linePoint2.X - linePoint1.X;
        var lineDirectionY = linePoint2.Y - linePoint1.Y;

        var t1 = lineDirectionY * point.X -
                 lineDirectionX * point.Y +
                 linePoint2.X * linePoint1.Y -
                 linePoint2.Y * linePoint1.X;

        var t2 = Math.Sqrt(
            lineDirectionX * lineDirectionX +
            lineDirectionY * lineDirectionY
        );

        return t1 / t2;
    }

    public static double GetSignedDistanceToLineVa(this ILinFloat64Vector2D point, IFloat64LineSegment2D lineSegment)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var lineDirectionX = lineSegment.Point2X - lineSegment.Point1X;
        var lineDirectionY = lineSegment.Point2Y - lineSegment.Point1Y;

        var t1 = lineDirectionY * point.X -
                 lineDirectionX * point.Y +
                 lineSegment.Point2X * lineSegment.Point1Y -
                 lineSegment.Point2Y * lineSegment.Point1X;

        var t2 = Math.Sqrt(
            lineDirectionX * lineDirectionX +
            lineDirectionY * lineDirectionY
        );

        return t1 / t2;
    }


    public static double GetDistanceToLineVa(this ILinFloat64Vector2D point, IFloat64Line2D line)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var t1 = line.DirectionY * point.X -
                 line.DirectionX * point.Y +
                 (line.OriginX + line.DirectionX) * line.OriginY -
                 (line.OriginY + line.DirectionY) * line.OriginX;

        var t2 = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        return Math.Abs(t1 / t2);
    }

    public static double GetDistanceToLineVa(this ILinFloat64Vector2D point, ILinFloat64Vector2D linePoint1, ILinFloat64Vector2D linePoint2)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var lineDirectionX = linePoint2.X - linePoint1.X;
        var lineDirectionY = linePoint2.Y - linePoint1.Y;

        var t1 = lineDirectionY * point.X -
                 lineDirectionX * point.Y +
                 linePoint2.X * linePoint1.Y -
                 linePoint2.Y * linePoint1.X;

        var t2 = Math.Sqrt(
            lineDirectionX * lineDirectionX +
            lineDirectionY * lineDirectionY
        );

        return Math.Abs(t1 / t2);
    }

    public static double GetDistanceToLineVa(this ILinFloat64Vector2D point, IFloat64LineSegment2D lineSegment)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var lineDirectionX = lineSegment.Point2X - lineSegment.Point1X;
        var lineDirectionY = lineSegment.Point2Y - lineSegment.Point1Y;

        var t1 = lineDirectionY * point.X -
                 lineDirectionX * point.Y +
                 lineSegment.Point2X * lineSegment.Point1Y -
                 lineSegment.Point2Y * lineSegment.Point1X;

        var t2 = Math.Sqrt(
            lineDirectionX * lineDirectionX +
            lineDirectionY * lineDirectionY
        );

        return Math.Abs(t1 / t2);
    }


    public static double GetSignedDistanceToPlaneVa(this ILinFloat64Vector3D point, IFloat64Plane3D plane)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var planeNormal = plane.GetNormal();

        var t1 = planeNormal.X * (point.X - plane.OriginX) +
                 planeNormal.Y * (point.Y - plane.OriginY) +
                 planeNormal.Z * (point.Z - plane.OriginZ);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return t1 / t2;
    }

    public static double GetSignedDistanceToPlaneVa(this ILinFloat64Vector3D point, IFloat64Triangle3D triangle)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var planeNormal = triangle.GetNormal();

        var t1 = planeNormal.X * (point.X - triangle.Point1X) +
                 planeNormal.Y * (point.Y - triangle.Point1Y) +
                 planeNormal.Z * (point.Z - triangle.Point1Z);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return t1 / t2;
    }

    public static double GetSignedDistanceToPlaneVa(this ILinFloat64Vector3D point, ILinFloat64Vector3D planePoint1, ILinFloat64Vector3D planePoint2, ILinFloat64Vector3D planePoint3)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var v1 = planePoint1.GetDirectionTo(planePoint2);
        var v2 = planePoint2.GetDirectionTo(planePoint3);

        var planeNormal = v1.VectorCross(v2);

        var t1 = planeNormal.X * (point.X - planePoint1.X) +
                 planeNormal.Y * (point.Y - planePoint1.Y) +
                 planeNormal.Z * (point.Z - planePoint1.Z);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return t1 / t2;
    }


    public static double GetDistanceToPlaneVa(this ILinFloat64Vector3D point, IFloat64Plane3D plane)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var planeNormal = plane.GetNormal();

        var t1 = planeNormal.X * (point.X - plane.OriginX) +
                 planeNormal.Y * (point.Y - plane.OriginY) +
                 planeNormal.Z * (point.Z - plane.OriginZ);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return Math.Abs(t1 / t2);
    }

    public static double GetDistanceToPlaneVa(this ILinFloat64Vector3D point, IFloat64Triangle3D triangle)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var planeNormal = triangle.GetNormal();

        var t1 = planeNormal.X * (point.X - triangle.Point1X) +
                 planeNormal.Y * (point.Y - triangle.Point1Y) +
                 planeNormal.Z * (point.Z - triangle.Point1Z);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return Math.Abs(t1 / t2);
    }

    public static double GetDistanceToPlaneVa(this ILinFloat64Vector3D point, ILinFloat64Vector3D planePoint1, ILinFloat64Vector3D planePoint2, ILinFloat64Vector3D planePoint3)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var v1 = planePoint1.GetDirectionTo(planePoint2);
        var v2 = planePoint2.GetDirectionTo(planePoint3);

        var planeNormal = v1.VectorCross(v2);

        var t1 = planeNormal.X * (point.X - planePoint1.X) +
                 planeNormal.Y * (point.Y - planePoint1.Y) +
                 planeNormal.Z * (point.Z - planePoint1.Z);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return Math.Abs(t1 / t2);
    }


    public static double GetSignedDistanceFromPointVa(this IFloat64Plane3D plane, ILinFloat64Vector3D point)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var planeNormal = plane.GetNormal();

        var t1 = planeNormal.X * (point.X - plane.OriginX) +
                 planeNormal.Y * (point.Y - plane.OriginY) +
                 planeNormal.Z * (point.Z - plane.OriginZ);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return t1 / t2;
    }

    public static double GetDistanceFromPointVa(this IFloat64Plane3D plane, ILinFloat64Vector3D point)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane

        var planeNormal = plane.GetNormal();

        var t1 = planeNormal.X * (point.X - plane.OriginX) +
                 planeNormal.Y * (point.Y - plane.OriginY) +
                 planeNormal.Z * (point.Z - plane.OriginZ);

        var t2 = Math.Sqrt(
            planeNormal.X * planeNormal.X +
            planeNormal.Y * planeNormal.Y +
            planeNormal.Z * planeNormal.Z
        );

        return Math.Abs(t1 / t2);
    }


    public static double GetSignedDistanceFromPointVa(this IFloat64Line2D line, ILinFloat64Vector2D point)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var t1 = line.DirectionY * point.X -
                 line.DirectionX * point.Y +
                 (line.OriginX + line.DirectionX) * line.OriginY -
                 (line.OriginY + line.DirectionY) * line.OriginX;

        var t2 = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        return t1 / t2;
    }

    public static double GetDistanceFromPointVa(this IFloat64Line2D line, ILinFloat64Vector2D point)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var t1 = line.DirectionY * point.X -
                 line.DirectionX * point.Y +
                 (line.OriginX + line.DirectionX) * line.OriginY -
                 (line.OriginY + line.DirectionY) * line.OriginX;

        var t2 = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        return Math.Abs(t1 / t2);
    }

    public static double GetSignedDistanceFromPointVa(this IFloat64Line2D line, double pointX, double pointY)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var t1 = line.DirectionY * pointX -
                 line.DirectionX * pointY +
                 (line.OriginX + line.DirectionX) * line.OriginY -
                 (line.OriginY + line.DirectionY) * line.OriginX;

        var t2 = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        return t1 / t2;
    }

    public static double GetDistanceFromPointVa(this IFloat64Line2D line, double pointX, double pointY)
    {
        //https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line

        var t1 = line.DirectionY * pointX -
                 line.DirectionX * pointY +
                 (line.OriginX + line.DirectionX) * line.OriginY -
                 (line.OriginY + line.DirectionY) * line.OriginX;

        var t2 = Math.Sqrt(
            line.DirectionX * line.DirectionX +
            line.DirectionY * line.DirectionY
        );

        return Math.Abs(t1 / t2);
    }


    public static double GetSignedDistanceToLineVa(this IFloat64Line3D line1, IFloat64Line3D line2)
    {
        //https://math.stackexchange.com/questions/2213165/find-shortest-distance-between-lines-in-3d

        var n = line1
            .GetDirection()
            .VectorCross(line2.GetDirection());

        return n.IsAlmostZeroVector()
            ? 0
            : n.ToUnitLinVector3D().VectorESp(line1.GetOrigin() - line2.GetOrigin());
    }

    public static double GetDistanceToLineVa(this IFloat64Line3D line1, IFloat64Line3D line2)
    {
        //https://math.stackexchange.com/questions/2213165/find-shortest-distance-between-lines-in-3d

        var n = line1
            .GetDirection()
            .VectorCross(line2.GetDirection());

        return n.IsAlmostZeroVector()
            ? 0
            : n.ToUnitLinVector3D().VectorESpAbs(line1.GetOrigin() - line2.GetOrigin());
    }
}