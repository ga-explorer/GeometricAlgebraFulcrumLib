using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public class PhCurveOptimizerPSO
{
    public int SwarmSize { get; set; } = 100;
    
    public int MaxIter { get; set; } = 200;
    
    public double Inertia { get; set; } = 0.7;
    
    public double Cognitive { get; set; } = 1.4;
    
    public double Social { get; set; } = 1.4;
    
    public List<double[]> SeedGenomes { get; set; } = new List<double[]>(); // NEW

    public bool EnableHybridInjection { get; set; } = false;
    
    public int InjectionCount { get; set; } = 5;
    
    public double InjectionStd { get; set; } = 0.05;


    private readonly int _dim;
    private readonly Func<double[], PhBernsteinPolyCurve2D> _map;
    private readonly PhOptimizerOptions _opts;
    private readonly List<LinFloat64Vector2D> _wps;
    private readonly Random _rng = new Random(0);


    public PhCurveOptimizerPSO(int dim, Func<double[], PhBernsteinPolyCurve2D> map, PhOptimizerOptions opts, List<LinFloat64Vector2D> wps)
    {
        _dim = dim; _map = map; _opts = opts; _wps = wps;
    }

    public (double[] best, double bestF, PhBernsteinPolyCurve2D bestC) Run()
    {
        var n = SwarmSize;
        var pos = new double[n][];
        var vel = new double[n][];
        var pbest = new double[n][];
        var pbestF = new double[n];
        double[] gbest = null; 
        var gbestF = double.PositiveInfinity;

        // Insert seed genomes
        var idx = 0;
        foreach (var g in SeedGenomes)
        {
            if (idx < n)
            {
                pos[idx] = (double[])g.Clone();
                vel[idx] = new double[_dim];
                
                pbest[idx] = (double[])pos[idx].Clone();
                pbestF[idx] = EvalFitness(pos[idx]);
                if (pbestF[idx] < gbestF)
                {
                    gbestF = pbestF[idx]; 
                    gbest = (double[])pbest[idx].Clone();
                }

                idx++;
            }
        }

        // Fill remainder with random
        for (; idx < n; idx++)
        {
            pos[idx] = new double[_dim];
            vel[idx] = new double[_dim];
            for (var d=0; d<_dim; d++)
                pos[idx][d] = _rng.NextDouble()*2.0 - 1.0;
                
            pbest[idx] = (double[])pos[idx].Clone();
            pbestF[idx] = EvalFitness(pos[idx]);
            if (pbestF[idx] < gbestF)
            {
                gbestF = pbestF[idx]; 
                gbest = (double[])pbest[idx].Clone();
            }
        }
            
            
        for (var iter = 0; iter < MaxIter; iter++)
        {
            for (var i = 0; i < n; i++)
            {
                for (var d = 0; d < _dim; d++)
                {
                    double r1 = _rng.NextDouble(), r2 = _rng.NextDouble();
                    vel[i][d] = Inertia * vel[i][d] + Cognitive * r1 * (pbest[i][d] - pos[i][d]) + Social * r2 * (gbest[d] - pos[i][d]);
                    pos[i][d] += vel[i][d];
                }
                var f = EvalFitness(pos[i]);
                if (f < pbestF[i])
                {
                    pbestF[i] = f; 
                    pbest[i] = (double[])pos[i].Clone();
                }

                if (f < gbestF)
                {
                    gbestF = f; 
                    gbest = (double[])pos[i].Clone();
                }

                if (EnableHybridInjection && gbest != null)
                {
                    var injected = HybridInjector.Inject(gbest, InjectionCount, InjectionStd);
                    foreach (var g in injected)
                    {
                        var replaceIdx = _rng.Next(n);
                        pos[replaceIdx] = g;
                        vel[replaceIdx] = new double[_dim]; // reset velocity
                    }
                }
            }
        }

        

        var bestCurve = _map(gbest);

        return (gbest, gbestF, bestCurve);
    }

    private double EvalFitness(double[] genome)
    {
        var curve = _map(genome);
        var dev = PhOptimizerHelpers.WaypointProximityEnergy(curve, _wps, _opts);
        var (maxK, energy) = PhOptimizerHelpers.SampleCurvatureAndEnergy(curve, _opts);
        var penalty = maxK > _opts.KappaMax ? _opts.CurvaturePenaltyWeight * (maxK - _opts.KappaMax) : 0.0;
        return _opts.WaypointWeight * dev + _opts.EnergyWeight * energy + penalty + 1e-8 * GenomeNorm(genome);
    }

    private double GenomeNorm(double[] g) { double s = 0; foreach (var v in g) s += v * v; return Math.Sqrt(s); }
}