using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public class PhCurveOptimizerGA
{
    public int PopulationSize { get; set; } = 100;
    
    public int Generations { get; set; } = 200;
    
    public double CrossoverRate { get; set; } = 0.7;
    
    public double MutationRate { get; set; } = 0.05;
    
    public double MutationStd { get; set; } = 0.1;

    public List<double[]> SeedGenomes { get; set; } = new List<double[]>();

    public bool EnableHybridInjection { get; set; } = false;
    
    public int InjectionCount { get; set; } = 5;
    
    public double InjectionStd { get; set; } = 0.05;
    
    
    private readonly int _genomeLength;
    private readonly Func<double[], PhBernsteinPolyCurve2D> _genomeToPolyCurve;
    private readonly PhOptimizerOptions _options;
    private readonly List<LinFloat64Vector2D> _waypoints;
    private readonly Random _rng = new Random(0);


    public PhCurveOptimizerGA(int genomeLength, Func<double[], PhBernsteinPolyCurve2D> genomeToMulti, PhOptimizerOptions opts, List<LinFloat64Vector2D> waypoints)
    {
        _genomeLength = genomeLength;
        _genomeToPolyCurve = genomeToMulti;
        _options = opts;
        _waypoints = waypoints;
    }

    public (double[] bestGenome, double bestFitness, PhBernsteinPolyCurve2D bestCurve) Run()
    {
        var pop = new List<double[]>();

        // Init population
        // Insert seed genomes first
        foreach (var g in SeedGenomes)
        {
            if (pop.Count < PopulationSize)
                pop.Add((double[])g.Clone());
        }

        // Fill remaining with random
        while (pop.Count < PopulationSize)
        {
            var g = new double[_genomeLength];
            for (var j = 0; j < _genomeLength; j++)
                g[j] = _rng.NextDouble()*2.0 - 1.0;
            pop.Add(g);
        }
            

        var bestF = double.PositiveInfinity;
        double[] bestG = null;
        PhBernsteinPolyCurve2D bestC = null;

        for (var gen = 0; gen < Generations; gen++)
        {
            // evaluate
            var fitness = new double[PopulationSize];
            for (var i = 0; i < PopulationSize; i++)
            {
                var curve = _genomeToPolyCurve(pop[i]);
                var dev = PhOptimizerHelpers.WaypointProximityEnergy(curve, _waypoints, _options);
                var (maxK, energy) = PhOptimizerHelpers.SampleCurvatureAndEnergy(curve, _options);
                var penalty = maxK > _options.KappaMax ? _options.CurvaturePenaltyWeight * (maxK - _options.KappaMax) : 0.0;
                var f = _options.WaypointWeight * dev + _options.EnergyWeight * energy + penalty + 1e-8 * GenomeNorm(pop[i]);
                fitness[i] = f;
                if (f < bestF)
                {
                    bestF = f; 
                    bestG = (double[])pop[i].Clone(); 
                    bestC = curve;
                }
            }

            // Hybrid injection step
            if (EnableHybridInjection && bestG != null)
            {
                var injected = HybridInjector.Inject(bestG, InjectionCount, InjectionStd);
                foreach (var g in injected)
                {
                    var replaceIdx = _rng.Next(PopulationSize);
                    pop[replaceIdx] = g;
                }
            }

            // selection (tournament)
            var next = new List<double[]>();
            while (next.Count < PopulationSize)
            {
                int a = _rng.Next(PopulationSize), b = _rng.Next(PopulationSize);
                var winner = fitness[a] < fitness[b] ? a : b;
                var parent1 = pop[winner];

                // crossover
                var child = (double[])parent1.Clone();
                if (_rng.NextDouble() < CrossoverRate)
                {
                    var c2 = _rng.Next(PopulationSize);
                    var parent2 = pop[c2];
                    var cp = _rng.Next(_genomeLength);
                    child = new double[_genomeLength];
                    for (var k = 0; k < _genomeLength; k++) child[k] = k < cp ? parent1[k] : parent2[k];
                }
                // mutation
                for (var k = 0; k < _genomeLength; k++)
                {
                    if (_rng.NextDouble() < MutationRate)
                        child[k] += NormalSample(0, MutationStd);
                }
                next.Add(child);
            }
            pop = next;
        }

        return (bestG, bestF, bestC);
    }

    private double GenomeNorm(double[] g)
    {
        double s = 0; foreach (var v in g) s += v * v; return Math.Sqrt(s);
    }

    private double NormalSample(double mu, double sigma)
    {
        // Box-Muller
        var u1 = 1.0 - _rng.NextDouble();
        var u2 = 1.0 - _rng.NextDouble();
        var r = Math.Sqrt(-2.0 * Math.Log(u1));
        var theta = 2.0 * Math.PI * u2;
        return mu + sigma * r * Math.Cos(theta);
    }


}