namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public static class HybridInjector
{
    private static readonly Random _rng = new Random(99);

    /// <summary>
    /// Creates perturbed copies of given genome.
    /// </summary>
    public static List<double[]> Inject(double[] parent, int count, double stdDev)
    {
        var list = new List<double[]>();

        for (var i = 0; i < count; i++)
        {
            var child = (double[])parent.Clone();

            for (var j = 0; j < child.Length; j++)
                child[j] += NormalSample(0, stdDev);

            list.Add(child);
        }

        return list;

        double NormalSample(double mu, double sigma)
        {
            // Box-Muller
            var u1 = 1.0 - _rng.NextDouble();
            var u2 = 1.0 - _rng.NextDouble();
            var r = Math.Sqrt(-2.0 * Math.Log(u1));
            var theta = 2.0 * Math.PI * u2;

            return mu + sigma * r * Math.Cos(theta);
        }
    }
}