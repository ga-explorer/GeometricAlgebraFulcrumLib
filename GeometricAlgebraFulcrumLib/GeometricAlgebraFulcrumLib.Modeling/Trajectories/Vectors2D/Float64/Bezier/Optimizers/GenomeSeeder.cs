//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

//public static class GenomeSeeder
//{
//    private static readonly Random _rng = new Random(42);

//    /// <summary>
//    /// Expands each seed genome into a cluster of variants.
//    /// Each coefficient is perturbed with Gaussian noise.
//    /// </summary>
//    public static List<double[]> ExpandSeeds(List<double[]> seeds, int countPerSeed, double stdDev = 0.05)
//    {
//        var expanded = new List<double[]>();

//        foreach (var g in seeds)
//        {
//            for (var i = 0; i < countPerSeed; i++)
//            {
//                var copy = (double[])g.Clone();
//                for (var j = 0; j < copy.Length; j++)
//                    copy[j] += NormalSample(0, stdDev);
//                expanded.Add(copy);
//            }
//        }

//        return expanded;

//        double NormalSample(double mu, double sigma)
//        {
//            // Box-Muller
//            var u1 = 1.0 - _rng.NextDouble();
//            var u2 = 1.0 - _rng.NextDouble();
//            var r = Math.Sqrt(-2.0 * Math.Log(u1));
//            var theta = 2.0 * Math.PI * u2;

//            return mu + sigma * r * Math.Cos(theta);
//        }
//    }
//}