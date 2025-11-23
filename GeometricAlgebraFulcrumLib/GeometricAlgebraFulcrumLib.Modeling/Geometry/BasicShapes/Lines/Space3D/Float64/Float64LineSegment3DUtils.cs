using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64
{
    public static class Float64LineSegment3DUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetLength(this IPair<LinFloat64Vector3D> lineSegment)
        {
            var dx = lineSegment.Item2.X - lineSegment.Item1.X;
            var dy = lineSegment.Item2.Y - lineSegment.Item1.Y;
            var dz = lineSegment.Item2.Z - lineSegment.Item1.Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetLengthSquared(this IPair<LinFloat64Vector3D> lineSegment)
        {
            var dx = lineSegment.Item2.X - lineSegment.Item1.X;
            var dy = lineSegment.Item2.Y - lineSegment.Item1.Y;
            var dz = lineSegment.Item2.Z - lineSegment.Item1.Z;

            return dx * dx + dy * dy + dz * dz;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D GetPoint1(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return LinFloat64Vector3D.Create(
                lineSegment.Item1.X,
                lineSegment.Item1.Y,
                lineSegment.Item1.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D GetPoint2(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return LinFloat64Vector3D.Create(
                lineSegment.Item2.X,
                lineSegment.Item2.Y,
                lineSegment.Item2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<LinFloat64Vector3D> GetEndPoints(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return new Pair<LinFloat64Vector3D>(
                lineSegment.Item1,
                lineSegment.Item2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinFloat64Vector3D> GetEndpoints(this IEnumerable<IPair<LinFloat64Vector3D>> lineSegmentsList)
        {
            foreach (var lineSegment in lineSegmentsList)
            {
                yield return lineSegment.Item1;
                yield return lineSegment.Item2;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D GetPointAt(this IPair<LinFloat64Vector3D> lineSegment, double t)
        {
            Debug.Assert(!double.IsNaN(t));

            var s = 1.0d - t;

            return LinFloat64Vector3D.Create(
                s * lineSegment.Item1.X + t * lineSegment.Item2.X,
                s * lineSegment.Item1.Y + t * lineSegment.Item2.Y,
                s * lineSegment.Item1.Z + t * lineSegment.Item2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinFloat64Vector3D> GetPointsAt(this IPair<LinFloat64Vector3D> lineSegment, IEnumerable<double> tList)
        {
            return tList.Select(lineSegment.GetPointAt);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D GetDirection12(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return LinFloat64Vector3D.Create(
                lineSegment.Item2.X - lineSegment.Item1.X,
                lineSegment.Item2.Y - lineSegment.Item1.Y,
                lineSegment.Item2.Z - lineSegment.Item1.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D GetDirection21(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return LinFloat64Vector3D.Create(
                lineSegment.Item1.X - lineSegment.Item2.X,
                lineSegment.Item1.Y - lineSegment.Item2.Y,
                lineSegment.Item1.Z - lineSegment.Item2.Z
            );
        }


        /// <summary>
        /// Tests if a point lies on a 3D line segment within a given tolerance.
        /// Uses cross product magnitude to check collinearity and dot product to check bounds.
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool SegmentContainsPoint(this IPair<LinFloat64Vector3D> segment, ITriplet<double> point, double tolerance = Float64Utils.DefaultTolerance)
        {
            var (start, end) = 
                segment.GetEndPoints();

            // Quick check for degenerate case
            if (Math.Abs(start.X - end.X) < tolerance && 
                Math.Abs(start.Y - end.Y) < tolerance && 
                Math.Abs(start.Z - end.Z) < tolerance)
            {
                // Segment is a point - check if point is close to it
                var dx = point.Item1 - start.Item1;
                var dy = point.Item2 - start.Item2;
                var dz = point.Item3 - start.Item3;

                return dx * dx + dy * dy + dz * dz <= tolerance * tolerance;
            }

            // Vector from start to end
            var dx1 = end.Item1 - start.Item1;
            var dy1 = end.Item2 - start.Item2;
            var dz1 = end.Item3 - start.Item3;
            
            // Vector from start to point
            var dx2 = point.Item1 - start.Item1;
            var dy2 = point.Item2 - start.Item2;
            var dz2 = point.Item3 - start.Item3;
            
            // Cross product components
            var crossX = dy1 * dz2 - dz1 * dy2;
            var crossY = dz1 * dx2 - dx1 * dz2;
            var crossZ = dx1 * dy2 - dy1 * dx2;
            
            // Magnitude of cross product (distance from line * length of segment)
            var crossMagnitude = Math.Sqrt(crossX * crossX + crossY * crossY + crossZ * crossZ);
            var segmentLength = Math.Sqrt(dx1 * dx1 + dy1 * dy1 + dz1 * dz1);
            
            // Check if point is collinear (distance from line <= tolerance)
            if (crossMagnitude > tolerance * segmentLength)
                return false;
            
            // Dot product to check if point is within segment bounds
            var dotProduct = dx1 * dx2 + dy1 * dy2 + dz1 * dz2;
            var segmentLengthSquared = dx1 * dx1 + dy1 * dy1 + dz1 * dz1;
            
            // Point is on segment if 0 <= dotProduct <= segmentLengthSquared
            return dotProduct >= -tolerance && 
                   dotProduct <= segmentLengthSquared + tolerance;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Line3D ToLine(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return new Float64Line3D(
                lineSegment.Item1.X,
                lineSegment.Item1.Y,
                lineSegment.Item1.Z,
                lineSegment.Item2.X - lineSegment.Item1.X,
                lineSegment.Item2.Y - lineSegment.Item1.Y,
                lineSegment.Item2.Z - lineSegment.Item1.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Line3D ToLineInv(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return new Float64Line3D(
                lineSegment.Item2.X,
                lineSegment.Item2.Y,
                lineSegment.Item2.Z,
                lineSegment.Item1.X - lineSegment.Item2.X,
                lineSegment.Item1.Y - lineSegment.Item2.Y,
                lineSegment.Item1.Z - lineSegment.Item2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64LimitedLine3D ToLimitedLine(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return new Float64LimitedLine3D(
                lineSegment.Item1.X,
                lineSegment.Item1.Y,
                lineSegment.Item1.Z,
                lineSegment.Item2.X - lineSegment.Item1.X,
                lineSegment.Item2.Y - lineSegment.Item1.Y,
                lineSegment.Item2.Z - lineSegment.Item1.Z,
                0,
                1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64LimitedLine3D ToLimitedLineInv(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return new Float64LimitedLine3D(
                lineSegment.Item2.X,
                lineSegment.Item2.Y,
                lineSegment.Item2.Z,
                lineSegment.Item1.X - lineSegment.Item2.X,
                lineSegment.Item1.Y - lineSegment.Item2.Y,
                lineSegment.Item1.Z - lineSegment.Item2.Z,
                0,
                1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Triangle3D ToTriangle(this IPair<LinFloat64Vector3D> lineSegment, LinFloat64Vector3D point3)
        {
            return new Float64Triangle3D(
                lineSegment.Item1.X,
                lineSegment.Item1.Y,
                lineSegment.Item1.Z,
                lineSegment.Item2.X,
                lineSegment.Item2.Y,
                lineSegment.Item2.Z,
                point3.X,
                point3.Y,
                point3.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64LineSegment3D ToLineSegment(this IPair<LinFloat64Vector3D> lineSegment, double t1, double t2)
        {
            var s1 = 1.0d - t1;
            var s2 = 1.0d - t2;

            return new Float64LineSegment3D(
                s1 * lineSegment.Item1.X + t1 * lineSegment.Item2.X,
                s1 * lineSegment.Item1.Y + t1 * lineSegment.Item2.Y,
                s1 * lineSegment.Item1.Z + t1 * lineSegment.Item2.Z,
                s2 * lineSegment.Item1.X + t2 * lineSegment.Item2.X,
                s2 * lineSegment.Item1.Y + t2 * lineSegment.Item2.Y,
                s2 * lineSegment.Item1.Z + t2 * lineSegment.Item2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64LineSegment3D ToLineSegment(this IPair<LinFloat64Vector3D> lineSegment)
        {
            return new Float64LineSegment3D(
                lineSegment.Item1.X,
                lineSegment.Item1.Y,
                lineSegment.Item1.Z,
                lineSegment.Item2.X,
                lineSegment.Item2.Y,
                lineSegment.Item2.Z
            );
        }

        public static IEnumerable<Float64LineSegment3D> ToLineSegments(this IEnumerable<ITriplet<double>> polylinePoints, bool closedShape = true)
        {
            var pointsArray = polylinePoints.ToArray();
            var point1 = pointsArray[0];

            for (var i = 1; i < pointsArray.Length; i++)
            {
                var point2 = pointsArray[i];

                yield return new Float64LineSegment3D(point1, point2);

                point1 = point2;
            }

            if (closedShape)
            {
                var point2 = pointsArray[0];

                yield return new Float64LineSegment3D(point1, point2);
            }
        }


        /// <summary>
        /// Computes the minimum Euclidean distance between two 3D line segments.
        /// Each segment is defined by two endpoints.
        /// </summary>
        /// <param name="lineSegment1">First point of segment 1</param>
        /// <param name="lineSegment2">Second point of segment 1</param>
        /// <returns>Minimum distance between the two segments</returns>
        public static double MinDistanceToSegment(this IPair<LinFloat64Vector3D> lineSegment1, IPair<LinFloat64Vector3D> lineSegment2)
        {
            var p1 = lineSegment1.Item1;
            var p2 = lineSegment1.Item2;
            var p3 = lineSegment2.Item1;
            var p4 = lineSegment2.Item2;

            // Handle degenerate cases (zero-length segments)
            if (p1.IsNearEqual(p2)) return DistanceToSegment(p1, p3, p4);
            if (p3.IsNearEqual(p4)) return DistanceToSegment(p3, p1, p2);

            // Direction vectors
            var d1 = p2.VectorSubtract(p1);  // Direction of segment 1
            var d2 = p4.VectorSubtract(p3);  // Direction of segment 2
            var d3 = p3.VectorSubtract(p1);  // Vector from p1 to p3

            // Dot products
            double dot1 = LinFloat64Vector3DUtils.L2NormSquared(d1);  // |d1|²
            double dot2 = LinFloat64Vector3DUtils.L2NormSquared(d2);  // |d2|²
            double dot12 = d1.VectorDot(d2); // d1·d2
            double dot13 = d1.VectorDot(d3); // d1·d3
            double dot23 = d2.VectorDot(d3); // d2·d3

            // Calculate parameters for closest points
            var denom = dot1 * dot2 - dot12 * dot12;

            double s, t;

            if (Math.Abs(denom) < 1e-10) // Segments are parallel or nearly parallel
            {
                // Use first segment parameter
                s = dot13 / dot1;
                t = 0.0;
            }
            else
            {
                // Solve for parameters using the standard formula
                s = (dot12 * dot23 - dot2 * dot13) / denom;
                t = (dot1 * dot23 - dot12 * dot13) / denom;
            }

            // Clamp parameters to [0,1] to stay within segments
            s = Math.Max(0.0, Math.Min(1.0, s));
            t = Math.Max(0.0, Math.Min(1.0, t));

            // Calculate the closest points on each segment
            var closest1 = p1.VectorAdd(d1.VectorTimes(s));
            var closest2 = p3.VectorAdd(d2.VectorTimes(t));

            // Return distance between the closest points
            return closest1.GetDistanceToPoint(closest2);
        }

        /// <summary>
        /// Gets the minimum distance and also returns the closest points on each segment
        /// </summary>
        /// <param name="lineSegment1">First point of segment 1</param>
        /// <param name="lineSegment2">Second point of segment 1</param>
        /// <param name="tolerance"></param>
        /// <returns>Minimum distance between the two segments</returns>
        public static (double minDistance, LinFloat64Vector3D closestPoint1, LinFloat64Vector3D closestPoint2) MinDistanceToSegmentWithPoints(this IPair<LinFloat64Vector3D> lineSegment1, IPair<LinFloat64Vector3D> lineSegment2, double tolerance = Float64Utils.DefaultTolerance)
        {
            var p1 = lineSegment1.Item1;
            var p2 = lineSegment1.Item2;
            var p3 = lineSegment2.Item1;
            var p4 = lineSegment2.Item2;

            // Handle degenerate cases
            if (p1.IsNearEqual(p2))
            {
                var closestPoint1 = p1;
                var closestPoint2 = ProjectPointToSegment(p1, p3, p4, tolerance);
                var minDistance = closestPoint1.GetDistanceToPoint(closestPoint2);

                return (minDistance, closestPoint1, closestPoint2);
            }

            if (p3.IsNearEqual(p4))
            {
                var closestPoint2 = p3;
                var closestPoint1 = ProjectPointToSegment(p3, p1, p2, tolerance);
                var minDistance = closestPoint1.GetDistanceToPoint(closestPoint2);

                return (minDistance, closestPoint1, closestPoint2);
            }

            // Direction vectors
            var d1 = p2.VectorSubtract(p1);
            var d2 = p4.VectorSubtract(p3);
            var d3 = p3.VectorSubtract(p1);

            // Dot products
            double dot1 = LinFloat64Vector3DUtils.L2NormSquared(d1);
            double dot2 = LinFloat64Vector3DUtils.L2NormSquared(d2);
            double dot12 = d1.VectorDot(d2);
            double dot13 = d1.VectorDot(d3);
            double dot23 = d2.VectorDot(d3);

            var denom = dot1 * dot2 - dot12 * dot12;
            double s, t;

            if (Math.Abs(denom) < 1e-10)
            {
                s = dot13 / dot1;
                t = 0.0;
            }
            else
            {
                s = (dot12 * dot23 - dot2 * dot13) / denom;
                t = (dot1 * dot23 - dot12 * dot13) / denom;
            }

            // Clamp to segment bounds
            s = Math.Max(0.0, Math.Min(1.0, s));
            t = Math.Max(0.0, Math.Min(1.0, t));

            {
                // Calculate closest points
                var closestPoint1 = p1.VectorAdd(d1.VectorTimes(s));
                var closestPoint2 = p3.VectorAdd(d2.VectorTimes(t));
                var minDistance = closestPoint1.GetDistanceToPoint(closestPoint2);

                return (minDistance, closestPoint1, closestPoint2);
            }
        }

        /// <summary>
        /// Distance from a point to a 3D line segment
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segStart"></param>
        /// <param name="segEnd"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceToSegment(this ITriplet<double> point, ITriplet<double> segStart, ITriplet<double> segEnd, double tolerance = Float64Utils.DefaultTolerance)
        {
            return point.GetDistanceToPoint(
                ProjectPointToSegment(point, segStart, segEnd, tolerance)
            );
        }

        /// <summary>
        /// Distance from a point to a 3D line segment
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segment"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceToSegment(this ITriplet<double> point, IPair<LinFloat64Vector3D> segment, double tolerance = Float64Utils.DefaultTolerance)
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
        /// Projects a point onto a 3D line segment
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segStart"></param>
        /// <param name="segEnd"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D ProjectPointToSegment(this ITriplet<double> point, ITriplet<double> segStart, ITriplet<double> segEnd, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (segStart.IsNearEqual(segEnd, tolerance))
                return segStart.ToLinVector3D();

            ITriplet<double> direction = segEnd.VectorSubtract(segStart);
            double lengthSquared = direction.L2NormSquared();

            // Parameter of closest point on infinite line
            double t = point.VectorSubtract(segStart).VectorDot(direction) / lengthSquared;

            // Clamp to segment [0,1]
            t = Math.Max(0.0, Math.Min(1.0, t));

            // Return projected point
            return segStart.VectorAdd(direction.VectorTimes(t));
        }

        /// <summary>
        /// Projects a point onto a 3D line segment
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segment"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D ProjectPointToSegment(this ITriplet<double> point, IPair<LinFloat64Vector3D> segment, double tolerance = Float64Utils.DefaultTolerance)
        {
            return ProjectPointToSegment(
                point,
                segment.Item1,
                segment.Item2,
                tolerance
            );
        }


        /// <summary>
        /// Create an adjacency graph from a set of separate line segments.
        /// The adjacency graph stores each unique point in the graph along
        /// with a set of neighbouring points connected by line segments from
        /// the input edge list.
        /// </summary>
        /// <param name="edgeList"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static Dictionary<LinFloat64Vector3D, IReadOnlyList<LinFloat64Vector3D>> CreateAdjacencyGraph(this IReadOnlyList<IPair<LinFloat64Vector3D>> edgeList, int decimalPlaces = 12)
        {
            var graph =
                LinFloat64Vector3D.CreateToleranceDictionary<List<LinFloat64Vector3D>>(decimalPlaces);

            var comparer = graph.Comparer;

            foreach (var edge in edgeList)
            {
                var edgePoint1 = edge.Item1;
                var edgePoint2 = edge.Item2;

                // Never add a self-edge
                if (comparer.Equals(edgePoint1, edgePoint2)) continue;

                if (!graph.TryGetValue(edgePoint1, out var neighborList1))
                    graph[edgePoint1] = neighborList1 = [];

                if (!graph.TryGetValue(edgePoint2, out var neighborList2))
                    graph[edgePoint2] = neighborList2 = [];

                neighborList1.Add(edgePoint2);
                neighborList2.Add(edgePoint1);
            }

            var finalGraph =
                LinFloat64Vector3D.CreateToleranceDictionary<IReadOnlyList<LinFloat64Vector3D>>(decimalPlaces);

            foreach (var (point, neighbors) in graph)
                finalGraph.Add(
                    point,
                    neighbors.Distinct(comparer).ToArray()
                );

            return finalGraph;
        }

        /// <summary>
        /// Construct the adjacency graph of a set of edges then divide
        /// the graph into a list of edge chains. An edge chain is a connected
        /// set of edges where each of the two end points of the chian is either
        /// a leaf (has one neighbor) or a junction (has more than two neighbors).
        /// Aside from the two end points, any other point inside a chain has
        /// exactly two neighbors.
        /// </summary>
        /// <param name="edgeList"></param>
        /// <returns></returns>
        public static IReadOnlyList<List<LinFloat64Vector3D>> GetChains(this IReadOnlyList<IPair<LinFloat64Vector3D>> edgeList)
        {
            var chainList = new List<List<LinFloat64Vector3D>>();

            // Create adjacency graph from separate edges
            var graph =
                edgeList.CreateAdjacencyGraph();

            // Get points defining chain end-points along with first internal point in chain;
            // Ignore singular points and points inside a chain
            var startNextPointPairs =
                graph.Where(
                    p =>
                        p.Value.Count is 1 or >= 3
                ).SelectMany(p =>
                    p.Value.Select(q =>
                        new Float64LineSegment3D(p.Key, q)
                    )
                );

            // Track which chains were visited so no chain gets stored
            // twice from its two end points
            var visitedChains = new HashSet<IPair<LinFloat64Vector3D>>();

            foreach (var (startPoint, nextPoint) in startNextPointPairs)
            {
                var chain = new List<LinFloat64Vector3D> { startPoint, nextPoint };

                var prevChainPoint = startPoint;
                var currChainPoint = nextPoint;

                while (true)
                {
                    var currChainPointNeighbors =
                        graph[currChainPoint];

                    // Reached the other end-point of the chain
                    if (currChainPointNeighbors.Count != 2)
                        break;

                    // pick the neighbor not equal to previous
                    var nextChainPoint =
                        currChainPointNeighbors[0].Equals(prevChainPoint)
                            ? currChainPointNeighbors[1]
                            : currChainPointNeighbors[0];

                    // Add new point to chain
                    chain.Add(nextChainPoint);

                    // Prepare for next iteration
                    prevChainPoint = currChainPoint;
                    currChainPoint = nextChainPoint;
                }

                // Only store a chain once regardless of the order of its
                // two end-points
                var chainEndPoints1 = new Float64LineSegment3D(chain[0], chain[^1]);
                var chainEndPoints2 = new Float64LineSegment3D(chain[^1], chain[0]);

                if (visitedChains.Contains(chainEndPoints1)) continue;
                if (visitedChains.Contains(chainEndPoints2)) continue;

                visitedChains.Add(chainEndPoints1);
                chainList.Add(chain);
            }

            return chainList;
        }

    }
}
