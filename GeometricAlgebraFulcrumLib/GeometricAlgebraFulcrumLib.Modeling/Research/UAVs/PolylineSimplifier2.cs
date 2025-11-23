//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

//namespace GeometricAlgebraFulcrumLib.Modeling.Research.UAVs
//{
//    public static class PolylineSimplifier2
//    {
//        /// <summary>
//        /// Simplify a polyline (list of points). Keeps start and end points.
//        /// The simplified polyline's vertices are a subset of original vertices' indices (start and end kept),
//        /// except intermediate points are chosen from original vertices (greedy).
//        /// Every original vertex must be within geomTolerance of the simplified polyline.
//        /// Each simplified segment must be at least minClearance away from all obstacle segments.
//        /// </summary>
//        /// <param name="polyline">Original polyline (n &gt; 1): consecutive points.</param>
//        /// <param name="obstacles">List of obstacle segments (each as IPair<LinFloat64Vector2D>).</param>
//        /// <param name="geomTolerance">Maximum allowed distance from original points to simplified segments.</param>
//        /// <param name="minClearance">Minimum allowed distance from simplified segments to obstacles.</param>
//        /// <returns>Simplified polyline (list of points). Start and end are the same as original.</returns>
//        public static List<LinFloat64Vector2D> Simplify(IReadOnlyList<LinFloat64Vector2D> polyline, Float64LineSegment2DBvh obstacles, double geomTolerance, double minClearance)
//        {
//            if (polyline == null) 
//                throw new ArgumentNullException(nameof(polyline));

//            if (polyline.Count < 2) 
//                throw new ArgumentException("polyline must contain at least 2 points.");

//            if (geomTolerance < 0) 
//                throw new ArgumentException("geomTolerance must be non-negative.");

//            if (minClearance < 0) 
//                throw new ArgumentException("minClearance must be non-negative.");

//            var n = polyline.Count;
//            var result = new List<LinFloat64Vector2D>();
//            var i = 0;
//            result.Add(polyline[0]);

//            // Pre-check: ensure original endpoints obey clearance (they should by problem statement)
//            if (!PointClearance(polyline[0], obstacles, minClearance))
//                throw new InvalidOperationException("Start point violates clearance constraint.");

//            if (!PointClearance(polyline[n - 1], obstacles, minClearance))
//                throw new InvalidOperationException("End point violates clearance constraint.");

//            while (i < n - 1)
//            {
//                // We will find the farthest j > i such that segment (polyline[i], polyline[j]) is acceptable.
//                var bestJ = i + 1; // at least next point
                
//                // Try to expand j as far as possible
//                for (var j = i + 1; j < n; ++j)
//                {
//                    // Quick bounding check: if we can't have that segment because distance to one of
//                    // intermediate points is > geomTolerance OR clearance fails, break (we can't use larger j either?).
//                    // Careful: if a j fails, larger j may still fail or sometimes pass (rare), but greedy approach
//                    // commonly tries sequentially and picks the farthest passing j. So we don't break — we just skip failing j.
//                    var geomOk = MaxDistanceToSegment(polyline, i, j) <= geomTolerance + 1e-12;
//                    if (!geomOk) continue;

//                    var clearanceOk = ValidateSegmentClearance(new Float64LineSegment2D(polyline[i], polyline[j]), obstacles, minClearance);
//                    if (!clearanceOk) continue;

//                    // both ok: accept this j as current best
//                    bestJ = j;
//                }

//                // If bestJ == i, something is wrong (can't progress), but we ensure bestJ >= i+1
//                if (bestJ == i)
//                {
//                    // Fallback: add next point to avoid infinite loop
//                    bestJ = i + 1;
//                }

//                // Append chosen vertex
//                result.Add(polyline[bestJ]);
//                i = bestJ;
//            }

//            return result;
//        }

//        /// <summary>
//        /// Compute the maximum perpendicular distance from original points with indices (i+1 .. j-1)
//        /// to the candidate segment (polyline[i] -> polyline[j]).
//        /// If j == i+1, there are no intermediate points and this returns 0.
//        /// </summary>
//        private static double MaxDistanceToSegment(IReadOnlyList<LinFloat64Vector2D> polyline, int i, int j)
//        {
//            if (j <= i + 1) return 0.0;
//            var a = polyline[i];
//            var b = polyline[j];

//            var max = 0.0;
//            for (var k = i + 1; k < j; ++k)
//            {
//                var d = GetPointToSegmentDistance(polyline[k], a, b);
//                if (d > max) max = d;
//            }

//            return max;
//        }

//        /// <summary>
//        /// Check that every point on segment s is at least minClearance away from every obstacle segment.
//        /// Uses the exact segment-segment closest-point calculation (distance >= minClearance).
//        /// </summary>
//        public static bool ValidateSegmentClearance(IPair<LinFloat64Vector2D> s, Float64LineSegment2DBvh obstacles, double minClearance, double tolerance = Float64Utils.DefaultTolerance)
//        {
//            if (obstacles.Count == 0) return true;
//            var minClearanceSq = minClearance * minClearance;

//            foreach (var obs in obstacles)
//            {
//                var d2 = 
//                    GetSegmentToSegmentDistanceSquared(s.Item1, s.Item2, obs.Item1, obs.Item2);
                
//                if (d2 < minClearanceSq - tolerance)
//                    return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// Check whether a point is at least minClearance away from all obstacles.
//        /// </summary>
//        private static bool PointClearance(LinFloat64Vector2D p, IReadOnlyList<IPair<LinFloat64Vector2D>> obstacles, double minClearance)
//        {
//            if (obstacles.Count == 0) return true;

//            var minClearanceSq = minClearance * minClearance;
//            foreach (var obs in obstacles)
//            {
//                var d2 = 
//                    GetPointToSegmentDistanceSquared(p, obs.Item1, obs.Item2);
                
//                if (d2 < minClearanceSq - 1e-12) 
//                    return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// Squared distance from point p to segment (a,b).
//        /// Numerical stable implementation: projects and clamps.
//        /// </summary>
//        /// <param name="point"></param>
//        /// <param name="a"></param>
//        /// <param name="b"></param>
//        /// <returns></returns>
//        public static double GetPointToSegmentDistanceSquared(this LinFloat64Vector2D point, LinFloat64Vector2D a, LinFloat64Vector2D b)
//        {
//            var ab = b - a;
//            var ap = point - a;
//            double ab2 = LinFloat64Vector2DUtils.L2NormSquared(ab);
            
//            if (ab2 == 0.0) 
//                return LinFloat64Vector2DUtils.L2NormSquared(ap);

//            double t = ap.VectorDot(ab) / ab2;
//            if (t <= 0.0) 
//                return LinFloat64Vector2DUtils.L2NormSquared(ap);

//            if (t >= 1.0)
//            {
//                var bp = point - b; 
//                return LinFloat64Vector2DUtils.L2NormSquared(bp);
//            }
            
//            var proj = a + ab * t;
//            var diff = point - proj;

//            return LinFloat64Vector2DUtils.L2NormSquared(diff);
//        }

//        /// <summary>
//        /// Squared distance from point p to segment (a,b).
//        /// Numerical stable implementation: projects and clamps.
//        /// </summary>
//        /// <param name="point"></param>
//        /// <param name="a"></param>
//        /// <param name="b"></param>
//        /// <returns></returns>
//        public static double GetPointToSegmentDistance(this LinFloat64Vector2D point, LinFloat64Vector2D a, LinFloat64Vector2D b)
//        {
//            return Math.Sqrt(GetPointToSegmentDistanceSquared(point, a, b));
//        }

//        /// <summary>
//        /// Squared minimum distance between two segments [p1,p2] and [q1,q2].
//        /// Robust algorithm computing the closest points on rectilinear parameter domain and clamping.
//        /// </summary>
//        public static double GetSegmentToSegmentDistanceSquared(LinFloat64Vector2D p1, LinFloat64Vector2D p2, LinFloat64Vector2D q1, LinFloat64Vector2D q2)
//        {
//            // From "Real-Time Collision Detection" by Christer Ericson (commonly used algorithm)
//            var d1 = p2 - p1; // direction vector of segment S1
//            var d2 = q2 - q1; // direction vector of segment S2
//            var r = p1 - q1;
//            double a = d1.VectorDot(d1); // squared length of S1
//            double e = d2.VectorDot(d2); // squared length of S2
//            double f = d2.VectorDot(r);

//            double s, t;

//            if (a <= Float64Utils.DefaultTolerance && e <= Float64Utils.DefaultTolerance)
//            {
//                // Both segments degenerate to points
//                return LinFloat64Vector2DUtils.L2NormSquared(r);
//            }

//            if (a <= Float64Utils.DefaultTolerance)
//            {
//                // First segment is a point
//                s = 0.0;
//                t = (f / e).Clamp(0.0, 1.0);
//            }
//            else
//            {
//                double c = d1.VectorDot(r);
//                if (e <= Float64Utils.DefaultTolerance)
//                {
//                    // Second segment is a point
//                    t = 0.0;
//                    s = (-c / a).Clamp(0.0, 1.0);
//                }
//                else
//                {
//                    double b = d1.VectorDot(d2);
//                    var denom = a * e - b * b;

//                    if (denom != 0.0)
//                        s = ((b * f - c * e) / denom).Clamp(0.0, 1.0);
//                    else
//                        s = 0.0; // parallel

//                    var tNom = b * s + f;

//                    if (tNom < 0.0)
//                    {
//                        t = 0.0;
//                        s = (-c / a).Clamp(0.0, 1.0);
//                    }
//                    else if (tNom > e)
//                    {
//                        t = 1.0;
//                        s = ((b - c) / a).Clamp(0.0, 1.0);
//                    }
//                    else
//                    {
//                        t = tNom / e;
//                    }
//                }
//            }

//            var c1 = p1 + d1 * s;
//            var c2 = q1 + d2 * t;
//            var diff = c1 - c2;

//            return LinFloat64Vector2DUtils.L2NormSquared(diff);
//        }

//        /// <summary>
//        /// Minimum distance between two segments [p1,p2] and [q1,q2].
//        /// Robust algorithm computing the closest points on rectilinear parameter domain and clamping.
//        /// </summary>
//        public static double GetSegmentToSegmentDistance(LinFloat64Vector2D p1, LinFloat64Vector2D p2, LinFloat64Vector2D q1, LinFloat64Vector2D q2)
//        {
//            return Math.Sqrt(
//                GetSegmentToSegmentDistanceSquared(p1, p2, q1, q2)
//            );
//        }


//        public static void Example()
//        {
//            // Example noisy polyline (a noisy straight-ish path)
//            var polyline = new List<LinFloat64Vector2D>
//            {
//                LinFloat64Vector2D.Create(0,0),
//                LinFloat64Vector2D.Create(1,0.1),
//                LinFloat64Vector2D.Create(2,-0.05),
//                LinFloat64Vector2D.Create(3,0.02),
//                LinFloat64Vector2D.Create(4,0.0),
//                LinFloat64Vector2D.Create(5,0.5), // a bump near end
//                LinFloat64Vector2D.Create(6,0.05),
//                LinFloat64Vector2D.Create(7,0.0)
//            };

//            // Example obstacles (segments)
//            var obstacles = new List<IPair<LinFloat64Vector2D>>
//            {
//                new Float64LineSegment2D(LinFloat64Vector2D.Create(2.5, 1.0), LinFloat64Vector2D.Create(2.5, 2.0)), // vertical obstacle up above path
//                new Float64LineSegment2D(LinFloat64Vector2D.Create(4.5, -1.0), LinFloat64Vector2D.Create(4.5, -0.2)) // below path
//            };

//            const double geomTol = 0.25;   // max allowed distance from original points to simplified polyline
//            const double clearance = 0.15; // required clearance from obstacles

//            var simplified = Simplify(polyline, obstacles, geomTol, clearance);

//            Console.WriteLine("Original points: " + polyline.Count);
//            Console.WriteLine("Simplified points: " + simplified.Count);
//            Console.WriteLine("Simplified polyline:");
//            for (var i = 0; i < simplified.Count; ++i)
//            {
//                Console.WriteLine($"  {i}: ({simplified[i].X:0.###}, {simplified[i].Y:0.###})");
//            }

//            // Sanity checks: ensure each original point is within geomTol of simplified polyline
//            var ok = CheckGeomError(polyline, simplified, geomTol);
//            Console.WriteLine("Geom tolerance check: " + ok);

//            // Check clearance for each simplified segment
//            var clear = CheckClearance(simplified, obstacles, clearance);
//            Console.WriteLine("Clearance check: " + clear);
//        }

//        static bool CheckGeomError(IReadOnlyList<LinFloat64Vector2D> original, IReadOnlyList<LinFloat64Vector2D> simplified, double geomTol)
//        {
//            var tol2 = geomTol * geomTol;
//            // For each original point, find the closest simplified segment (they are in order)
//            for (var i = 0; i < original.Count; ++i)
//            {
//                // Move sIdx so that original[i] lies between simplified[sIdx] and simplified[sIdx+1] in param along original sequence
//                // Simpler: just check all simplified segments (small) and pick min distance — simpler and safe.
//                var minD2 = double.MaxValue;
//                for (var k = 0; k < simplified.Count - 1; ++k)
//                {
//                    var d2 = GetPointToSegmentDistanceSquared(original[i], simplified[k], simplified[k + 1]);
//                    if (d2 < minD2) minD2 = d2;
//                }
//                if (minD2 > tol2 + 1e-10) return false;
//            }
//            return true;
//        }

//        static bool CheckClearance(IReadOnlyList<LinFloat64Vector2D> simplified, IReadOnlyList<IPair<LinFloat64Vector2D>> obstacles, double clearance)
//        {
//            var c2 = clearance * clearance;
//            for (var k = 0; k < simplified.Count - 1; ++k)
//            {
//                var seg = new Float64LineSegment2D(simplified[k], simplified[k + 1]);
//                foreach (var obs in obstacles)
//                {
//                    var d2 = GetSegmentToSegmentDistanceSquared(seg.Item1, seg.Item2, obs.Item1, obs.Item2);
//                    if (d2 < c2 - 1e-10) return false;
//                }
//            }
//            return true;
//        }
//    }
//}
