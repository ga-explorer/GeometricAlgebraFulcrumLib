using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public static class Float64LineSegment2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetLength(this IPair<LinFloat64Vector2D> lineSegment)
    {
        var dx = lineSegment.Item2.X - lineSegment.Item1.X;
        var dy = lineSegment.Item2.Y - lineSegment.Item1.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetLengthSquared(this IPair<LinFloat64Vector2D> lineSegment)
    {
        var dx = lineSegment.Item2.X - lineSegment.Item1.X;
        var dy = lineSegment.Item2.Y - lineSegment.Item1.Y;

        return dx * dx + dy * dy;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPoint1(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return LinFloat64Vector2D.Create(
            lineSegment.Item1.X,
            lineSegment.Item1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPoint2(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return LinFloat64Vector2D.Create(
            lineSegment.Item2.X,
            lineSegment.Item2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> GetEndPoints(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return new Pair<LinFloat64Vector2D>(
            LinFloat64Vector2D.Create(
                lineSegment.Item1.X,
                lineSegment.Item1.Y
            ),
            LinFloat64Vector2D.Create(
                lineSegment.Item2.X,
                lineSegment.Item2.Y
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointAt(this IPair<LinFloat64Vector2D> lineSegment, double t)
    {
        Debug.Assert(!double.IsNaN(t));

        var s = 1.0d - t;

        return LinFloat64Vector2D.Create(
            s * lineSegment.Item1.X + t * lineSegment.Item2.X,
            s * lineSegment.Item1.Y + t * lineSegment.Item2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> GetPointsAt(this IPair<LinFloat64Vector2D> lineSegment, IEnumerable<double> tList)
    {
        return tList.Select(lineSegment.GetPointAt);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetDirection12(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return LinFloat64Vector2D.Create(
            lineSegment.Item2.X - lineSegment.Item1.X,
            lineSegment.Item2.Y - lineSegment.Item1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetDirection21(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return LinFloat64Vector2D.Create(
            lineSegment.Item1.X - lineSegment.Item2.X,
            lineSegment.Item1.Y - lineSegment.Item2.Y
        );
    }


    /// <summary>
    /// Tests if a point lies on a 2D line segment within a given tolerance.
    /// Uses cross product to check collinearity and dot product to check bounds.
    /// </summary>
    /// <param name="segment"></param>
    /// <param name="point"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static bool SegmentContainsPoint(this IPair<LinFloat64Vector2D> segment, LinFloat64Vector2D point, double tolerance = Float64Utils.DefaultTolerance)
    {
        var (start, end) =
            segment.GetEndPoints();

        // Quick check for degenerate case
        if ((start.X - end.X).IsNearZero(tolerance) &&
            (start.Y - end.Y).IsNearZero(tolerance))
        {
            // Segment is a point - check if point is close to it
            var dx = point.Item1 - start.X;
            var dy = point.Item2 - start.Y;

            return dx * dx + dy * dy <= tolerance * tolerance;
        }

        // Vector from start to end
        var dx1 = end.X - start.X;
        var dy1 = end.Y - start.Y;

        // Vector from start to point
        var dx2 = point.Item1 - start.X;
        var dy2 = point.Item2 - start.Y;

        // Cross product to check if point is collinear with the line
        // |dx1 dy1|
        // |dx2 dy2| = dx1*dy2 - dy1*dx2
        var crossProduct = dx1 * dy2 - dy1 * dx2;

        // Check if point is collinear (within tolerance)
        if (Math.Abs(crossProduct) > tolerance * Math.Sqrt(dx1 * dx1 + dy1 * dy1))
            return false;

        // Dot product to check if point is within segment bounds
        var dotProduct = dx1 * dx2 + dy1 * dy2;
        var segmentLengthSquared = dx1 * dx1 + dy1 * dy1;

        // Point is on segment if 0 <= dotProduct <= segmentLengthSquared
        return dotProduct >= -tolerance &&
               dotProduct <= segmentLengthSquared + tolerance;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Line2D ToLine(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return new Float64Line2D(
            lineSegment.Item1.X,
            lineSegment.Item1.Y,
            lineSegment.Item2.X - lineSegment.Item1.X,
            lineSegment.Item2.Y - lineSegment.Item1.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Line2D ToLineInv(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return new Float64Line2D(
            lineSegment.Item2.X,
            lineSegment.Item2.Y,
            lineSegment.Item1.X - lineSegment.Item2.X,
            lineSegment.Item1.Y - lineSegment.Item2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LimitedLine2D ToLimitedLine(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return new Float64LimitedLine2D(
            lineSegment.Item1.X,
            lineSegment.Item1.Y,
            lineSegment.Item2.X - lineSegment.Item1.X,
            lineSegment.Item2.Y - lineSegment.Item1.Y,
            0,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LimitedLine2D ToLimitedLineInv(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return new Float64LimitedLine2D(
            lineSegment.Item2.X,
            lineSegment.Item2.Y,
            lineSegment.Item1.X - lineSegment.Item2.X,
            lineSegment.Item1.Y - lineSegment.Item2.Y,
            0,
            1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Triangle2D ToTriangle(this IPair<LinFloat64Vector2D> lineSegment, LinFloat64Vector2D point3)
    {
        return new Float64Triangle2D(
            lineSegment.Item1.X,
            lineSegment.Item1.Y,
            lineSegment.Item2.X,
            lineSegment.Item2.Y,
            point3.X,
            point3.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment2D ToLineSegment(this IPair<LinFloat64Vector2D> lineSegment, double t1, double t2)
    {
        var s1 = 1.0d - t1;
        var s2 = 1.0d - t2;

        return new Float64LineSegment2D(
            s1 * lineSegment.Item1.X + t1 * lineSegment.Item2.X,
            s1 * lineSegment.Item1.Y + t1 * lineSegment.Item2.Y,
            s2 * lineSegment.Item1.X + t2 * lineSegment.Item2.X,
            s2 * lineSegment.Item1.Y + t2 * lineSegment.Item2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment2D ToLineSegment(this IPair<LinFloat64Vector2D> lineSegment)
    {
        return new Float64LineSegment2D(
            lineSegment.Item1.X,
            lineSegment.Item1.Y,
            lineSegment.Item2.X,
            lineSegment.Item2.Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64LineSegment2D> ToLineSegments(this IEnumerable<LinFloat64Vector2D> polylinePoints, bool closedShape = true)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetNormal(this IPair<LinFloat64Vector2D> lineSegment)
    {
        var direction = LinFloat64Vector2D.Create(
            lineSegment.Item2.X - lineSegment.Item1.X,
            lineSegment.Item2.Y - lineSegment.Item1.Y
        );

        return LinFloat64Vector2D.Create(-direction.Y, direction.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetUnitNormal(this IPair<LinFloat64Vector2D> lineSegment)
    {
        var direction = LinFloat64Vector2D.Create(
            lineSegment.Item2.X - lineSegment.Item1.X,
            lineSegment.Item2.Y - lineSegment.Item1.Y
        );

        var s = 1.0d / Math.Sqrt(
            direction.X * direction.X +
            direction.Y * direction.Y
        );

        return LinFloat64Vector2D.Create(-direction.Y * s, direction.X * s);
    }


    /// <summary>
    /// Computes the minimum Euclidean distance between two 2D line segments.
    /// Each segment is defined by two endpoints.
    /// </summary>
    /// <param name="lineSegment1"></param>
    /// <param name="lineSegment2"></param>
    /// <param name="tolerance"></param>
    /// <returns>Minimum distance between the two segments</returns>
    public static double MinDistanceToSegment(this IPair<LinFloat64Vector2D> lineSegment1, IPair<LinFloat64Vector2D> lineSegment2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var p1 = lineSegment1.Item1;
        var p2 = lineSegment1.Item2;

        var p3 = lineSegment2.Item1;
        var p4 = lineSegment2.Item2;

        // Handle degenerate cases (zero-length segments)
        if (p1.IsNearEqual(p2, tolerance))
            return DistanceToSegment(p1, p3, p4, tolerance);

        if (p3.IsNearEqual(p4, tolerance))
            return DistanceToSegment(p3, p1, p2, tolerance);

        // Direction vectors
        var dx1 = p2.Item1 - p1.Item1;
        var dy1 = p2.Item2 - p1.Item2;
        var dx2 = p4.Item1 - p3.Item1;
        var dy2 = p4.Item2 - p3.Item2;

        var dot1 = dx1 * dx1 + dy1 * dy1;  // |d1|²
        var dot2 = dx2 * dx2 + dy2 * dy2;  // |d2|²
        var dot12 = dx1 * dx2 + dy1 * dy2; // d1·d2

        // Vector from p1 to p3
        var dx3 = p3.Item1 - p1.Item1;
        var dy3 = p3.Item2 - p1.Item2;

        var dot13 = dx1 * dx3 + dy1 * dy3; // d1·d3
        var dot23 = dx2 * dx3 + dy2 * dy3; // d2·d3

        // Calculate parameters for closest points
        var denom = dot1 * dot2 - dot12 * dot12;

        double s, t;

        if (Math.Abs(denom) < 1e-10) // Segments are parallel
        {
            // Use first segment parameter
            s = dot13 / dot1;
            t = 0.0;
        }
        else
        {
            s = (dot12 * dot23 - dot2 * dot13) / denom;
            t = (dot1 * dot23 - dot12 * dot13) / denom;
        }

        // Clamp parameters to [0,1] to stay within segments
        s = Math.Max(0.0, Math.Min(1.0, s));
        t = Math.Max(0.0, Math.Min(1.0, t));

        // Calculate the closest points on each segment
        var closest1X = p1.Item1 + s * dx1;
        var closest1Y = p1.Item2 + s * dy1;
        var closest2X = p3.Item1 + t * dx2;
        var closest2Y = p3.Item2 + t * dy2;

        // Return distance between the closest points
        var dx = closest1X - closest2X;
        var dy = closest1Y - closest2Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// Computes the minimum Euclidean distance between two 2D line segments
    /// and returns the closest points on each segment.
    /// </summary>
    /// <param name="lineSegment1">First point of segment 1</param>
    /// <param name="lineSegment2">Second point of segment 1</param>
    /// <param name="tolerance"></param>
    /// <returns>Minimum distance between the two segments</returns>
    public static (double minDistance, LinFloat64Vector2D closestPoint1, LinFloat64Vector2D closestPoint2) MinDistanceWithPoints(this IPair<LinFloat64Vector2D> lineSegment1, IPair<LinFloat64Vector2D> lineSegment2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var p1 = lineSegment1.Item1;
        var p2 = lineSegment1.Item2;

        var p3 = lineSegment2.Item1;
        var p4 = lineSegment2.Item2;

        // Handle degenerate cases
        if (p1.IsNearEqual(p2, tolerance))
        {
            var closestPoint1 = p1;
            var closestPoint2 = ProjectPointToSegment(p1, p3, p4);
            var minDistance = closestPoint1.GetDistanceToPoint(closestPoint2);

            return (minDistance, closestPoint1, closestPoint2);
        }

        if (p3.IsNearEqual(p4, tolerance))
        {
            var closestPoint2 = p3;
            var closestPoint1 = ProjectPointToSegment(p3, p1, p2);

            var minDistance = closestPoint1.GetDistanceToPoint(closestPoint2);

            return (minDistance, closestPoint1, closestPoint2);
        }

        // Direction vectors
        var dx1 = p2.X - p1.X;
        var dy1 = p2.Y - p1.Y;
        var dx2 = p4.X - p3.X;
        var dy2 = p4.Y - p3.Y;

        var dot1 = dx1 * dx1 + dy1 * dy1;
        var dot2 = dx2 * dx2 + dy2 * dy2;
        var dot12 = dx1 * dx2 + dy1 * dy2;

        // Vector from p1 to p3
        var dx3 = p3.X - p1.X;
        var dy3 = p3.Y - p1.Y;

        var dot13 = dx1 * dx3 + dy1 * dy3;
        var dot23 = dx2 * dx3 + dy2 * dy3;

        // Calculate parameters for closest points
        var denom = dot1 * dot2 - dot12 * dot12;
        double s, t;

        if (Math.Abs(denom) < 1e-10) // Segments are parallel
        {
            s = dot13 / dot1;
            t = 0.0;
        }
        else
        {
            s = (dot12 * dot23 - dot2 * dot13) / denom;
            t = (dot1 * dot23 - dot12 * dot13) / denom;
        }

        // Clamp parameters to [0,1] to stay within segments
        s = Math.Max(0.0, Math.Min(1.0, s));
        t = Math.Max(0.0, Math.Min(1.0, t));

        {
            // Calculate the closest points on each segment
            var closestPoint1 = LinFloat64Vector2D.Create(p1.X + s * dx1, p1.Y + s * dy1);
            var closestPoint2 = LinFloat64Vector2D.Create(p3.X + t * dx2, p3.Y + t * dy2);

            // Return distance between the closest points
            var minDistance = closestPoint1.GetDistanceToPoint(closestPoint2);

            return (minDistance, closestPoint1, closestPoint2);
        }
    }

    /// <summary>
    /// Distance from a point to a 2D line segment
    /// </summary>
    /// <param name="point"></param>
    /// <param name="segStart"></param>
    /// <param name="segEnd"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DistanceToSegment(this LinFloat64Vector2D point, LinFloat64Vector2D segStart, LinFloat64Vector2D segEnd, double tolerance = Float64Utils.DefaultTolerance)
    {
        return point.GetDistanceToPoint(
            ProjectPointToSegment(point, segStart, segEnd, tolerance)
        );
    }

    /// <summary>
    /// Distance from a point to a 2D line segment
    /// </summary>
    /// <param name="point"></param>
    /// <param name="segment"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetPointToSegmentDistance(this LinFloat64Vector2D point, IPair<LinFloat64Vector2D> segment, double tolerance = Float64Utils.DefaultTolerance)
    {
        return point.GetDistanceToPoint(
            ProjectPointToSegment(
                point,
                segment.Item1,
                segment.Item2,
                tolerance
            )
        );
    }

    /// <summary>
    /// Squared distance from point p to segment (a,b).
    /// Numerical stable implementation: projects and clamps.
    /// </summary>
    /// <param name="point"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static double GetPointToSegmentDistanceSquared(this LinFloat64Vector2D point, LinFloat64Vector2D a, LinFloat64Vector2D b)
    {
        var ab = b - a;
        var ap = point - a;
        double ab2 = LinFloat64Vector2DUtils.L2NormSquared(ab);

        if (ab2 == 0.0)
            return LinFloat64Vector2DUtils.L2NormSquared(ap);

        double t = ap.VectorDot(ab) / ab2;
        if (t <= 0.0)
            return LinFloat64Vector2DUtils.L2NormSquared(ap);

        if (t >= 1.0)
        {
            var bp = point - b;
            return LinFloat64Vector2DUtils.L2NormSquared(bp);
        }

        var proj = a + ab * t;
        var diff = point - proj;

        return LinFloat64Vector2DUtils.L2NormSquared(diff);
    }

    /// <summary>
    /// Squared distance from point p to segment (a,b).
    /// Numerical stable implementation: projects and clamps.
    /// </summary>
    /// <param name="point"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static double GetPointToSegmentDistance(this LinFloat64Vector2D point, LinFloat64Vector2D a, LinFloat64Vector2D b)
    {
        return Math.Sqrt(GetPointToSegmentDistanceSquared(point, a, b));
    }

    /// <summary>
    /// Squared minimum distance between two segments [p1,p2] and [q1,q2].
    /// Robust algorithm computing the closest points on rectilinear parameter domain and clamping.
    /// </summary>
    public static double GetSegmentToSegmentDistanceSquared(LinFloat64Vector2D p1, LinFloat64Vector2D p2, LinFloat64Vector2D q1, LinFloat64Vector2D q2)
    {
        // From "Real-Time Collision Detection" by Christer Ericson (commonly used algorithm)
        var d1 = p2 - p1; // direction vector of segment S1
        var d2 = q2 - q1; // direction vector of segment S2
        var r = p1 - q1;
        double a = d1.VectorDot(d1); // squared length of S1
        double e = d2.VectorDot(d2); // squared length of S2
        double f = d2.VectorDot(r);

        double s, t;

        if (a <= Float64Utils.DefaultTolerance && e <= Float64Utils.DefaultTolerance)
        {
            // Both segments degenerate to points
            return LinFloat64Vector2DUtils.L2NormSquared(r);
        }

        if (a <= Float64Utils.DefaultTolerance)
        {
            // First segment is a point
            s = 0.0;
            t = (f / e).Clamp(0.0, 1.0);
        }
        else
        {
            double c = d1.VectorDot(r);
            if (e <= Float64Utils.DefaultTolerance)
            {
                // Second segment is a point
                t = 0.0;
                s = (-c / a).Clamp(0.0, 1.0);
            }
            else
            {
                double b = d1.VectorDot(d2);
                var denom = a * e - b * b;

                if (denom != 0.0)
                    s = ((b * f - c * e) / denom).Clamp(0.0, 1.0);
                else
                    s = 0.0; // parallel

                var tNom = b * s + f;

                if (tNom < 0.0)
                {
                    t = 0.0;
                    s = (-c / a).Clamp(0.0, 1.0);
                }
                else if (tNom > e)
                {
                    t = 1.0;
                    s = ((b - c) / a).Clamp(0.0, 1.0);
                }
                else
                {
                    t = tNom / e;
                }
            }
        }

        var c1 = p1 + d1 * s;
        var c2 = q1 + d2 * t;
        var diff = c1 - c2;

        return LinFloat64Vector2DUtils.L2NormSquared(diff);
    }

    /// <summary>
    /// Minimum distance between two segments [p1,p2] and [q1,q2].
    /// Robust algorithm computing the closest points on rectilinear parameter domain and clamping.
    /// </summary>
    public static double GetSegmentToSegmentDistance(LinFloat64Vector2D p1, LinFloat64Vector2D p2, LinFloat64Vector2D q1, LinFloat64Vector2D q2)
    {
        return Math.Sqrt(
            GetSegmentToSegmentDistanceSquared(p1, p2, q1, q2)
        );
    }

    /// <summary>
    /// Projects a point onto a 2D line segment
    /// </summary>
    /// <param name="point"></param>
    /// <param name="segStart"></param>
    /// <param name="segEnd"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectPointToSegment(this LinFloat64Vector2D point, LinFloat64Vector2D segStart, LinFloat64Vector2D segEnd, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (segStart.IsNearEqual(segEnd, tolerance))
            return segStart.ToLinVector2D();

        var direction = segEnd.VectorSubtract(segStart);
        double lengthSquared = LinFloat64Vector2DUtils.L2NormSquared(direction);

        // Parameter of closest point on infinite line
        var t = point.VectorSubtract(segStart).VectorDot(direction) / lengthSquared;

        // Clamp to segment [0,1]
        t = Math.Max(0.0, Math.Min(1.0, t));

        // Return projected point
        return segStart.VectorAdd(direction.VectorTimes(t));
    }

    /// <summary>
    /// Projects a point onto a 2D line segment
    /// </summary>
    /// <param name="point"></param>
    /// <param name="segment"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectPointToSegment(this LinFloat64Vector2D point, IPair<LinFloat64Vector2D> segment, double tolerance = Float64Utils.DefaultTolerance)
    {
        return ProjectPointToSegment(
            point,
            segment.Item1,
            segment.Item2,
            tolerance
        );
    }


    /// <summary>
    /// Get segment-segment intersection producing parameters along
    /// segment 1 (s) and along segment 2 (t). Returns true if they
    /// intersect (including endpoints)
    /// </summary>
    /// <param name="segment1"></param>
    /// <param name="segment2"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static (bool, double, double) GetSegmentIntersectionParameters(this IPair<LinFloat64Vector2D> segment1, IPair<LinFloat64Vector2D> segment2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var s = double.NaN;
        var t = double.NaN;

        var (a, b) = segment1.GetEndPoints();
        var (c, d) = segment2.GetEndPoints();

        var r = b - a;
        var sVector = d - c;
        var rxs = r.VectorCross(sVector);
        var cma = c - a;
        var qpxr = cma.VectorCross(r);

        if (rxs.IsNearZero(tolerance))
        {
            // Parallel
            if (qpxr.IsNearZero(tolerance))
            {
                // Collinear - check overlap by projecting onto axis
                var rr = LinFloat64Vector2DUtils.L2NormSquared(r);
                var t0 = (c - a).VectorDot(r) / rr;
                var t1 = (d - a).VectorDot(r) / rr;

                // check overlap intervals [t0,t1] with [0,1]
                var minT = Math.Min(t0, t1);
                var maxT = Math.Max(t0, t1);

                if (maxT < 0 || minT > 1)
                    return (false, s, t);

                // choose an overlapping parameter
                var sParam = Math.Max(0, minT);
                s = sParam;
                t = 0; // not used

                return (true, s, t);
            }

            return (false, s, t); // parallel non-intersecting
        }

        var u = cma.VectorCross(r) / rxs;
        var v = cma.VectorCross(sVector) / rxs; // v is along r

        // Solved: A + v * r = C + u * sVector
        if (v >= -tolerance && v <= 1 + tolerance && u >= -tolerance && u <= 1 + tolerance)
            return (true, v, u);

        return (false, s, t);
    }


    
    /// <summary>
    /// Serializes the line segment data into a JSON file using Newtonsoft.Json,
    /// with optional GZip compression.
    /// </summary>
    public static void SaveSegmentsToJson(this IEnumerable<Float64LineSegment2D> segmentList, string filePath, bool compress = false)
    {
        var generatorData = segmentList.Select(segment =>
        {
            var (p, q) = segment;
            
            return new[]{ p.Item1, p.Item2, q.Item1, q.Item2 };
        }).ToArray();

        var json = JsonConvert.SerializeObject(generatorData, Formatting.Indented);
        var bytes = System.Text.Encoding.UTF8.GetBytes(json);

        if (compress)
        {
            using var fileStream = File.Create(filePath);
            using var gzip = new GZipStream(fileStream, CompressionLevel.Optimal);
            gzip.Write(bytes, 0, bytes.Length);
        }
        else
        {
            File.WriteAllText(filePath, json);
        }
    }

}