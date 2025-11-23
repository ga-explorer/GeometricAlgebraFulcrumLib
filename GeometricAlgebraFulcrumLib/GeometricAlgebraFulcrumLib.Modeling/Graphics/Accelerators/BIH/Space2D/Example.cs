using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D
{
    // ==================== Example usage ====================
    public static class Example
    {
        public static void Demo()
        {
            var rand = new Random(123);
            var n = 5000;
            var segs = new Pair<LinFloat64Vector2D>[n];
            for (var i = 0; i < n; i++)
            {
                segs[i] = new Pair<LinFloat64Vector2D>(LinFloat64Vector2D.Create(rand.NextDouble() * 100, rand.NextDouble() * 100),
                                       LinFloat64Vector2D.Create(rand.NextDouble() * 100, rand.NextDouble() * 100));
            }

            var bih = Bih2D.BuildFromSegments(segs, leafCapacity: 8);

            // Non-alloc traversal via visitor + stackalloc buffer
            Span<int> stackBuf = stackalloc int[128];

            var visitor = new Bih2D.PrimitiveVisitor(Fn);
            bih.TraverseAll(visitor, stackBuf);

            // k-NN approximate
            var outIdx = new List<int>();
            bih.QueryKNearestToPoint(50.0, 50.0, 8, outIdx, maxNodesVisited: 500);
            Console.WriteLine($"kNN approx candidates: {outIdx.Count}");

            // exact k-NN
            var exact = new List<(int, double)>();
            bih.QueryKNearestExact(50.0, 50.0, 8, exact);
            Console.WriteLine($"kNN exact results: {exact.Count}");
            return;

            void Fn(int primIdx, ref bool stop)
            {
                // examplary fast distance test (point-to-segment squared distance)
                var s = bih.GetSegment(primIdx);
                var d2 = PointSegmentDistanceSquared(42.0, 42.0, s);
                if (d2 < 1e-6) stop = true;
                // do very cheap processing only!
            }
        }

        // helper: point-to-segment squared distance
        private static double PointSegmentDistanceSquared(double px, double py, Pair<LinFloat64Vector2D> s)
        {
            double vx = s.Item2.X - s.Item1.X, vy = s.Item2.Y - s.Item1.Y;
            double wx = px - s.Item1.X, wy = py - s.Item1.Y;
            var c1 = vx * wx + vy * wy;
            if (c1 <= 0) return (px - s.Item1.X) * (px - s.Item1.X) + (py - s.Item1.Y) * (py - s.Item1.Y);
            var c2 = vx * vx + vy * vy;
            if (c2 <= 0) return (px - s.Item1.X) * (px - s.Item1.X) + (py - s.Item1.Y) * (py - s.Item1.Y);
            var t = c1 / c2;
            if (t < 0) t = 0; else if (t > 1) t = 1;
            double projx = s.Item1.X + t * vx, projy = s.Item1.Y + t * vy;
            return (px - projx) * (px - projx) + (py - projy) * (py - projy);
        }
    }
}
