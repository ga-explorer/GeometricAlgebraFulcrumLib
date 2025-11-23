using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64
{
    public static class Float64Polyline2DUtils
    {
        /// Create an adjacency graph from a set of separate line segments.
        /// The adjacency graph stores each unique point in the graph along
        /// with a set of neighbouring points connected by line segments from
        /// the input edge list.
        /// <param name="edgeList"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static UndirectedAdjacencyGraph<LinFloat64Vector2D> CreateAdjacencyGraph(this IReadOnlyList<IPair<LinFloat64Vector2D>> edgeList, double tolerance = Float64Utils.DefaultTolerance)
        {
            return UndirectedAdjacencyGraph<LinFloat64Vector2D>.Create(
                new LinFloat64Vector2D.ToleranceComparer(tolerance),
                edgeList
            );
        }

        /// <summary>
        /// Construct the adjacency graph of a set of edges then divide
        /// the graph into a list of polylines. Each polyline is a connected
        /// set of edges where each of the two end points of the chian is either
        /// a leaf (has one neighbor) or a junction (has more than two neighbors).
        /// Aside from the two end points, any other point inside a polyline chain
        /// has exactly two neighbors.
        /// </summary>
        /// <param name="edgeList"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IReadOnlyList<List<LinFloat64Vector2D>> GetPolylines(this IReadOnlyList<IPair<LinFloat64Vector2D>> edgeList, double tolerance = Float64Utils.DefaultTolerance)
        {
            return edgeList.CreateAdjacencyGraph(tolerance).GetEdgeChains();
        }

        /// <summary>
        /// Get the total length of a polyline defined by its points
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetPolylineLength(this IReadOnlyList<LinFloat64Vector2D> chain)
        {
            var length = 0d;

            for (var i = 0; i < chain.Count - 1; i++)
                length += chain[i].GetDistanceToPoint(chain[i + 1]);

            return length;
        }

        /// <summary>
        /// Simplify a polyline path given as list of points
        /// </summary>
        /// <param name="polyLine">Path to be simplified</param>
        /// <param name="obstacles">Obstacles</param>
        /// <param name="epsilon">Allowed max distance from original path points to simplified path points</param>
        /// <param name="obstacleClearance">Minimal allowed distance between simplified segments and any obstacle segment</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IReadOnlyList<LinFloat64Vector2D> SimplifyPolyline(this IReadOnlyList<LinFloat64Vector2D> polyLine, IReadOnlyList<Float64LineSegment2D> obstacles, double epsilon, double obstacleClearance)
        {
            if (polyLine == null)
                throw new ArgumentNullException(nameof(polyLine));

            if (polyLine.Count < 2)
                throw new ArgumentException("P must contain at least two points.");

            var n = polyLine.Count;

            // Build candidate vertex list V:
            // v0 = P[0], then midpoints m0..m_{n-2}, then vLast = P[n-1]
            var v = new List<LinFloat64Vector2D>(n + 1);

            // origIdx[k] maps V[k] to an original-segment index or endpoints
            var origIdx = new List<int>(n + 1);

            v.Add(polyLine[0]);
            origIdx.Add(0); // v0 -> index 0

            for (var i = 0; i <= n - 2; ++i)
            {
                var m = LinFloat64Vector2D.Create(
                    (polyLine[i].X + polyLine[i + 1].X) * 0.5,
                    (polyLine[i].Y + polyLine[i + 1].Y) * 0.5
                );

                v.Add(m);

                // midpoint corresponds to original segment i..i+1
                origIdx.Add(i);
            }

            v.Add(polyLine[n - 1]);

            // last -> index n-1
            origIdx.Add(n - 1);

            var vCount = v.Count;

            // Precompute midpoints array for quick access and for
            // "segment must pass through some midpoint" check
            var midpoints = new LinFloat64Vector2D[n - 1];
            for (var i = 0; i < n - 1; ++i)
                midpoints[i] = LinFloat64Vector2D.Create(
                    (polyLine[i].X + polyLine[i + 1].X) * 0.5,
                    (polyLine[i].Y + polyLine[i + 1].Y) * 0.5
                );

            // Cache validity of edges from a to b
            // 0 unknown, 1 invalid, 2 valid
            var edgeValid = new byte[vCount * vCount];

            // Dynamic Programming to minimize number of segments from
            // V[0] to V[VCount-1]
            const int inf = 1_000_000_000;

            // dp[k] = minimal segments needed from k to end
            var dp = new int[vCount];

            // next[k] = next chosen vertex index
            var next = new int[vCount];

            for (var i = 0; i < vCount; ++i)
            {
                dp[i] = inf;
                next[i] = -1;
            }

            dp[vCount - 1] = 0;
            next[vCount - 1] = -1;

            // iterate backwards
            for (var k = vCount - 2; k >= 0; --k)
            {
                // try all b > k
                for (var b = k + 1; b < vCount; ++b)
                {
                    if (!IsEdgeValid(k, b)) continue;

                    // 1 segment + dp[b] segments from b to end
                    var candidateIndex = 1 + dp[b];

                    if (candidateIndex >= dp[k]) continue;

                    dp[k] = candidateIndex;
                    next[k] = b;
                }
            }

            if (dp[0] >= inf)
            {
                // No valid simplification found under constraints;
                // return original path.
                return polyLine;
            }

            // Reconstruct Q vertices in V index space
            var result = new List<LinFloat64Vector2D>();
            var cur = 0;

            result.Add(v[cur]); // should be P[0]

            while (next[cur] != -1)
            {
                cur = next[cur];
                result.Add(v[cur]);
            }

            // Ensure last equals original end point
            if (result.Count == 0 || Math.Abs(result[^1].X - polyLine[n - 1].X) > 1e-9 || Math.Abs(result[^1].Y - polyLine[n - 1].Y) > 1e-9)
            {
                // append end if missing
                result.Add(polyLine[n - 1]);
            }

            return result;

            bool IsEdgeValid(int a, int b)
            {
                var idx = a * vCount + b;

                if (edgeValid[idx] == 2)
                    return true;

                if (edgeValid[idx] == 1)
                    return false;

                if (b <= a)
                {
                    edgeValid[idx] = 1;
                    return false;
                }

                var va = v[a];
                var vb = v[b];
                var vSeg = new Float64LineSegment2D(va, vb);
                var ia = origIdx[a];
                var ib = origIdx[b];

                // Coverage indices in original point list: from jStart..jEnd
                var jStart = ia;
                var jEnd = Math.Min(ib + 1, n - 1);

                // 1) Check max distance from original points
                // in [jStart..jEnd] to segment(va,vb)
                var maxDist = 0.0;
                for (var j = jStart; j <= jEnd; ++j)
                {
                    var d = polyLine[j].DistanceToSegment(va, vb);

                    if (d > epsilon)
                    {
                        edgeValid[idx] = 1; // invalid
                        return false;
                    }

                    if (d > maxDist) maxDist = d;
                }

                // 2) Check that segment(va,vb) passes through at least
                // one midpoint among indices in [ia..ib]
                var passesMidpoint = false;
                var midStart = ia;
                var midEnd = Math.Min(ib, n - 2); // midpoints indices range 0..n-2
                var midTol = 1e-9; // small tolerance

                if (midStart <= midEnd)
                {
                    for (var mIdx = midStart; mIdx <= midEnd; ++mIdx)
                    {
                        if (!vSeg.SegmentContainsPoint(midpoints[mIdx], midTol))
                            continue;

                        passesMidpoint = true;
                        break;
                    }
                }

                // Edge special-case: if a endpoint is one of the midpoints,
                // that was included above. If no midpoint found, invalid.
                if (!passesMidpoint)
                {
                    edgeValid[idx] = 1;
                    return false;
                }

                // 3) Check obstacle clearance: ensure distance between
                // simplified segment and every obstacle >= obstacleClearance
                if (obstacles != null! && obstacles.Count > 0)
                {
                    // quick bounding box + early-out: compute bounding box of segment expanded by obstacleClearance
                    var minX = Math.Min(va.X, vb.X) - obstacleClearance;
                    var maxX = Math.Max(va.X, vb.X) + obstacleClearance;
                    var minY = Math.Min(va.Y, vb.Y) - obstacleClearance;
                    var maxY = Math.Max(va.Y, vb.Y) + obstacleClearance;

                    foreach (var obs in obstacles)
                    {
                        // A cheap bounding box check between boxes first:
                        var oMinX = Math.Min(obs.Item1.X, obs.Item2.X);
                        var oMaxX = Math.Max(obs.Item1.X, obs.Item2.X);
                        var oMinY = Math.Min(obs.Item1.Y, obs.Item2.Y);
                        var oMaxY = Math.Max(obs.Item1.Y, obs.Item2.Y);

                        if (oMaxX < minX || oMinX > maxX || oMaxY < minY || oMinY > maxY)
                            continue;

                        var dist = vSeg.MinDistanceToSegment(obs);

                        if (dist < obstacleClearance - 1e-12)
                        {
                            edgeValid[idx] = 1;
                            return false;
                        }
                    }
                }

                edgeValid[idx] = 2;
                return true;
            }
        }


        private static IEnumerable<Pair<int>> GetPolylineZigzagIndices(this IReadOnlyList<LinFloat64Vector2D> polyLine)
        {
            var startIndex = 0;
            for (var i = 0; i < polyLine.Count - 3; i++)
            {
                if (IsZigzag(i)) continue;

                yield return new Pair<int>(
                    startIndex, 
                    i + 2
                );

                i += 3;
                startIndex = i;
            }

            yield return new Pair<int>(
                startIndex, 
                polyLine.Count - 1
            );

            yield break;
            
            bool IsZigzag(int i1)
            {
                var p1 = polyLine[i1];
                var p2 = polyLine[i1 + 1];
                var p3 = polyLine[i1 + 2];
                var p4 = polyLine[i1 + 3];

                var u = p2 - p1;
                var v = p3 - p2;
                var w = p4 - p3;

                var s1 = u.VectorCrossSign(v);
                var s2 = v.VectorCrossSign(w);

                return (s1 * s2).IsNegative;
            }
        }
        
        private static IEnumerable<int> GetPolylineNearStraightIndices(this IReadOnlyList<LinFloat64Vector2D> polyLine, double angleCosTolerance, double distanceTolerance)
        {
            yield return 0;

            var startIndex = 0;
            for (var i = 1; i < polyLine.Count - 1; i++)
            {
                if (IsNearStraight(i)) 
                    continue;

                yield return i;

                startIndex = i;
            }

            yield return polyLine.Count - 1;

            yield break;
            
            bool IsNearStraight(int i1)
            {
                var p1 = polyLine[startIndex];
                var p2 = polyLine[i1];
                var p3 = polyLine[i1 + 1];

                if ((p2 - p1).L2Norm().IsNearZero(distanceTolerance))
                    return true;

                var u = (p2 - p1).VectorDivideByENorm();
                var v = (p3 - p2).VectorDivideByENorm();

                var uvDot = u.VectorDot(v);
                
                return uvDot.IsNearOne(angleCosTolerance);
            }
        }

        public static IReadOnlyList<LinFloat64Vector2D> SimplifyPolyline(this IReadOnlyList<LinFloat64Vector2D> polyLine, Float64LineSegment2DBvh obstacles, double obstacleClearance)
        {
            //obstacleClearance = 
            //    0.9 * polyLine
            //        .ToLineSegments(false)
            //        .Select(obstacles.GetShortestDistanceToSegment)
            //        .Min();

            var result = new List<LinFloat64Vector2D>(polyLine.Count);

            var pIndex = 0;
            var p = polyLine[pIndex];
            result.Add(p);

            for (var qIndex = 1; qIndex < polyLine.Count; qIndex++)
            {
                if (obstacles.GetShortestDistanceToSegment(p, polyLine[qIndex]) < obstacleClearance)
                {
                    Debug.Assert(qIndex > pIndex + 1);
                    
                    pIndex = qIndex - 1;
                    p = polyLine[pIndex];
                    result.Add(p);
                }
            }

            result.Add(polyLine[^1]);

            return result;
        }

        public static IReadOnlyList<LinFloat64Vector2D> SimplifyZigzagPolyline(this IReadOnlyList<LinFloat64Vector2D> polyLine, IReadOnlyList<Float64LineSegment2D> obstacles, double distanceTolerance, double obstacleClearance)
        {
            //return polyLine;

            // Pass 1: isolate and smooth zigzag segments
            var pass1Result = new List<LinFloat64Vector2D>();

            var zigzagIndexPairList = 
                polyLine.GetPolylineZigzagIndices().ToArray();

            {
                var (i1, i2) = zigzagIndexPairList[0];

                if (i2 - i1 + 1 >= 4)
                    pass1Result.Add(polyLine[0]);
            }
            
            Debug.Assert(
                zigzagIndexPairList[0].Item1 == 0
            );

            Debug.Assert(
                zigzagIndexPairList[^1].Item2 == polyLine.Count - 1
            );

            foreach (var (i1, i2) in zigzagIndexPairList)
            {
                Debug.Assert(
                    i2 >= i1
                );

                if (i2 - i1 + 1 < 4)
                {
                    for (var i = i1; i <= i2; i++)
                        pass1Result.Add(polyLine[i]);
                }
                else
                {
                    for (var i = i1; i < i2; i++)
                        pass1Result.Add((polyLine[i] + polyLine[i + 1]) / 2);
                }
            }
            
            {
                var (i1, i2) = zigzagIndexPairList[^1];

                if (i2 - i1 + 1 >= 4)
                    pass1Result.Add(polyLine[^1]);
            }

            //return pass1Result;

            //Pass 2: reduce number of segments
            var angleCosTolerance = 1 - Math.Cos(15 * Math.PI / 180);
            return pass1Result
                .GetPolylineNearStraightIndices(angleCosTolerance, distanceTolerance)
                .Select(i => pass1Result[i])
                .ToArray();
        }


        public static Float64Polyline2D ToPolyline(this IEnumerable<LinFloat64Vector2D> pointList)
        {
            return new Float64Polyline2D(pointList);
        }


    }
}
