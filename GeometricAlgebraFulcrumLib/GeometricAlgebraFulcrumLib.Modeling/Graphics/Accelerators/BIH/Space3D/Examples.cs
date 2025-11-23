using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D
{
    // ==================== BIH3DAdvanced ====================

    // ==================== Example usage ====================
    public static class Examples
    {
        public static void Example()
        {
            // Build from segments
            var n = 10000;
            var rand = new Random(123);
            var segs = new Pair<LinFloat64Vector3D>[n];
            for (var i = 0; i < n; i++)
            {
                segs[i] = new Pair<LinFloat64Vector3D>(
                    LinFloat64Vector3D.Create(rand.NextDouble() * 100, rand.NextDouble() * 100, rand.NextDouble() * 100),
                    LinFloat64Vector3D.Create(rand.NextDouble() * 100, rand.NextDouble() * 100, rand.NextDouble() * 100)
                );
            }

            var bih = Bih3DAdvanced.BuildFromSegments(segs, leafCapacity: 8);

            // Non-alloc traversal example (count primitives)
            var count = 0;
            bih.Traverse((primIdx) => { count++; });
            Console.WriteLine($"Primitives counted via traversal: {count} (expected {n})");

            // Query K nearest primitives to a point (approximate)
            var outList = new List<int>();
            bih.QueryKNearestToPoint(50.0, 50.0, 50.0, k: 10, outList, maxNodesVisited: 500);
            Console.WriteLine($"Found {outList.Count} nearest primitives (approx).");

            // Query AABB candidates
            var aabbQ = new AABB3(40, 40, 40, 60, 60, 60);
            var candidates = new List<int>();
            bih.QueryAABB(aabbQ, candidates);
            Console.WriteLine($"AABB candidates: {candidates.Count}");
        }
    }
}
