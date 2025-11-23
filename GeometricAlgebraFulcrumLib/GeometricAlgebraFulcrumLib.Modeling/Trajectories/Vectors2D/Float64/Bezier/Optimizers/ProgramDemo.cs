using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers
{
    
    // A minimal Program.cs demo that constructs a few segments and runs NSGA-II.
    public static class ProgramDemo
    {
        public static void Example()
        {
            // Configuration: 3 segments, degree=2 (quartic PH: u,v,w degree 2 produces quintic spatial)
            const int segments = 3;
            const int degree = 2;
            const int genomeLength = segments * 3 * (degree + 1);

            var waypoints = new List<LinFloat64Vector2D> { (0, 0), (5, 3), (10, 0) };

            var opts = new PhOptimizerOptions
            {
                KappaMax = 0.5, 
                SamplesPerSegment = 64
            };

            var nsga = new PhCurveOptimizerNSGA2(genomeLength, GenomeToPolyCurve, opts, waypoints)
            {
                PopulationSize = 60,
                MaxGenerations = 80,
                CrossoverRate = 0.9,
                MutationRate = 0.03
            };

            Console.WriteLine("Running NSGA-II ...");
            var pareto = nsga.Run();

            Console.WriteLine($"Pareto set size: {pareto.Count}");
            foreach (var (genome, objectives, curve) in pareto)
            {
                Console.WriteLine($"Deviation={objectives[0]:F4}, Energy={objectives[1]:F4}");
            }

            Console.WriteLine("Demo finished.");
            return;

            // Create a simple genome initializer
            PhBernsteinPolyCurve2D GenomeToPolyCurve(double[] g) 
                => PhGenomeMapper.GenomeToPolyCurve(g, segments, degree, enforceG2: true);
        }
    }


}
