namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.SpaceND
{
    // ==================== Example ====================
    public static class ExampleND
    {
        public static void Demo()
        {
            const int dim = 5;
            var rand = new Random(42);
            const int n = 1000;
            var boxes = new AABBND[n];
            for (var i = 0; i < n; i++)
            {
                var min = new double[dim];
                var max = new double[dim];
                for (var d = 0; d < dim; d++)
                {
                    var a = rand.NextDouble() * 100;
                    var b = a + rand.NextDouble() * 5;
                    min[d] = a; max[d] = b;
                }
                boxes[i] = new AABBND(min, max);
            }

            var bih = BihND.Build(boxes);

            // AABB query
            var queryMin = new double[dim]; var queryMax = new double[dim];
            for (var d = 0; d < dim; d++) { queryMin[d] = 40; queryMax[d] = 60; }
            var qBox = new AABBND(queryMin, queryMax);
            var hits = new List<int>();
            bih.QueryAABB(qBox, hits);
            Console.WriteLine($"AABB query hits: {hits.Count}");

            // kNN query
            var qPoint = new double[dim]; for (var d = 0; d < dim; d++) qPoint[d] = 50;
            var nn = new List<(int, double)>();
            bih.QueryKNearestExact(qPoint, 5, nn);
            Console.WriteLine("Nearest neighbors:");
            foreach (var (i, d2) in nn) Console.WriteLine($" idx={i}, dist2={d2:F3}");
        }
    }
}
