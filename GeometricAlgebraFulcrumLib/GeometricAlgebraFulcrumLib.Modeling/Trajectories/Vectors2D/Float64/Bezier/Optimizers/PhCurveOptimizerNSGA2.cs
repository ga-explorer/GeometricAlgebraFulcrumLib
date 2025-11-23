using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public class PhCurveOptimizerNSGA2
{
    public int PopulationSize { get; set; } = 100;
    
    public int MaxGenerations { get; set; } = 200;
    
    public double CrossoverRate { get; set; } = 0.9;
    
    public double MutationRate { get; set; } = 0.02;
    
    public double MutationStd { get; set; } = 0.05;
    
    public List<double[]> SeedGenomes { get; set; } = []; // NEW

    public bool EnableHybridInjection { get; set; } = false;
    
    public int InjectionCount { get; set; } = 5;
    
    public double InjectionStd { get; set; } = 0.05;


    private readonly int _dim;
    private readonly Func<double[], PhBernsteinPolyCurve2D> _map;
    private readonly PhOptimizerOptions _opts;
    private readonly List<LinFloat64Vector2D> _wps;
    private readonly Random _rng = new Random(1);


    public PhCurveOptimizerNSGA2(int dim, Func<double[], PhBernsteinPolyCurve2D> map, PhOptimizerOptions opts, List<LinFloat64Vector2D> wps)
    {
        _dim = dim; _map = map; _opts = opts; _wps = wps;
    }

    public List<(double[] genome, double[] objectives, PhBernsteinPolyCurve2D curve)> Run()
    {
        var popN = PopulationSize;
        var pop = new double[popN][];
        var objs = new double[popN][];

        var idx = 0;
        // Insert seeds
        foreach (var g in SeedGenomes)
        {
            if (idx < popN)
            {
                pop[idx] = (double[])g.Clone();
                objs[idx] = EvalObjectives(pop[idx]);
                idx++;
            }
        }

        // Fill rest with random
        for (; idx < popN; idx++)
        {
            pop[idx] = new double[_dim];
            for (var d=0; d<_dim; d++)
                pop[idx][d] = _rng.NextDouble()*2.0 - 1.0;
            objs[idx] = EvalObjectives(pop[idx]);
        }


        for (var gen = 0; gen < MaxGenerations; gen++)
        {
            // create offspring by crossover + mutation
            var offspring = new double[popN][];
            for (var i = 0; i < popN; i++)
            {
                double[] child;
                if (_rng.NextDouble() < CrossoverRate)
                {
                    int a = _rng.Next(popN), b = _rng.Next(popN);
                    var p1 = pop[a]; var p2 = pop[b];
                    child = new double[_dim];
                    var cp = _rng.Next(_dim);
                    for (var k = 0; k < _dim; k++) child[k] = k < cp ? p1[k] : p2[k];
                }
                else
                {
                    child = (double[])pop[_rng.Next(popN)].Clone();
                }
                // mutation
                for (var d = 0; d < _dim; d++)
                    if (_rng.NextDouble() < MutationRate) child[d] += Normal(0, MutationStd);
                offspring[i] = child;
            }

            // combine parent+offspring and select next generation by NSGA-II
            var combinedPop = pop.Concat(offspring).ToArray();
            var combinedObjs = objs.Concat(offspring.Select(o => EvalObjectives(o))).ToArray();

            var nextIdx = NSGA2Selection(combinedPop, combinedObjs, popN);
            var newPop = new double[popN][];
            var newObjs = new double[popN][];
            for (var i = 0; i < popN; i++)
            {
                newPop[i] = combinedPop[nextIdx[i]];
                newObjs[i] = combinedObjs[nextIdx[i]];
            }
            pop = newPop; 
            objs = newObjs;

            if (EnableHybridInjection && pop.Length > 0)
            {
                // pick best from first front
                var paretoFront = FastNonDominatedSort(pop, objs)[0];
                var bestIdx = paretoFront[0];
                var injected = HybridInjector.Inject(pop[bestIdx], InjectionCount, InjectionStd);

                foreach (var g in injected)
                {
                    var replaceIdx = _rng.Next(pop.Length);
                    pop[replaceIdx] = g;
                    objs[replaceIdx] = EvalObjectives(g);
                }
            }
        }

        // extract pareto front
        var pareto = FastNonDominatedSort(pop, objs)[0];
        var result = new List<(double[], double[], PhBernsteinPolyCurve2D)>();
        foreach (var i in pareto)
        {
            var curve = _map(pop[i]);
            result.Add((pop[i], objs[i], curve));
        }
        return result;
    }

    private double[] EvalObjectives(double[] genome)
    {
        var curve = _map(genome);
        var dev = PhOptimizerHelpers.WaypointProximityEnergy(curve, _wps, _opts);
        var (maxK, energy) = PhOptimizerHelpers.SampleCurvatureAndEnergy(curve, _opts);
        // note: for NSGA-II we return [deviation, energy]
        return [dev, energy];
    }

    // NSGA-II selection core: fast non-dominated sorting + crowding distance
    private int[] NSGA2Selection(double[][] pop, double[][] objs, int surviving)
    {
        var fronts = FastNonDominatedSort(pop, objs);
        var selected = new List<int>();
        foreach (var front in fronts)
        {
            if (selected.Count + front.Count <= surviving)
            {
                selected.AddRange(front);
            }
            else
            {
                // compute crowding distance for front
                var cd = CrowdingDistance(front, objs);
                var order = front.Select((idx, pos) => (idx, cd[pos])).OrderByDescending(p => p.Item2).Select(p => p.idx).ToList();
                var need = surviving - selected.Count;
                selected.AddRange(order.Take(need));
                break;
            }
        }
        return selected.ToArray();
    }

    private static List<List<int>> FastNonDominatedSort(double[][] pop, double[][] objs)
    {
        var n = pop.Length;
        var S = new List<int>[n];
        var nDom = new int[n];
        var fronts = new List<List<int>>();
        var first = new List<int>();
        for (var p = 0; p < n; p++)
        {
            S[p] = [];
            nDom[p] = 0;
            for (var q = 0; q < n; q++)
            {
                if (p == q) continue;
                var d = DominanceCompare(objs[p], objs[q]);
                if (d == -1) S[p].Add(q); // p dominates q
                else if (d == 1) nDom[p]++; // p is dominated by q
            }
            if (nDom[p] == 0) first.Add(p);
        }
        fronts.Add(first);
        var i = 0;
        while (fronts.Count > i)
        {
            var next = new List<int>();
            foreach (var p in fronts[i])
            {
                foreach (var q in S[p])
                {
                    nDom[q]--;
                    if (nDom[q] == 0) next.Add(q);
                }
            }
            if (next.Count > 0) fronts.Add(next);
            i++;
        }
        return fronts;
    }

    // returns -1 if a dominates b, 1 if b dominates a, 0 otherwise non-dominated
    private static int DominanceCompare(double[] a, double[] b)
    {
        bool aBetter = false, bBetter = false;
        for (var i = 0; i < a.Length; i++)
        {
            if (a[i] < b[i]) aBetter = true;
            else if (a[i] > b[i]) bBetter = true;
        }
        if (aBetter && !bBetter) return -1;
        if (bBetter && !aBetter) return 1;
        return 0;
    }

    private static List<double> CrowdingDistance(List<int> front, double[][] objs)
    {
        var m = objs[0].Length;
        var l = front.Count;
        var dist = Enumerable.Repeat(0.0, l).ToList();
        if (l == 0) return dist;
        for (var i = 0; i < m; i++)
        {
            var order = front.Select((idx, pos) => (idx, objs[idx][i], pos)).OrderBy(p => p.Item2).ToArray();
            dist[order[0].pos] = double.PositiveInfinity;
            dist[order[l - 1].pos] = double.PositiveInfinity;
            double minv = order[0].Item2, maxv = order[l - 1].Item2;
            if (Math.Abs(maxv - minv) < 1e-15) continue;
            for (var j = 1; j < l - 1; j++)
            {
                var prevv = order[j - 1].Item2;
                var nextv = order[j + 1].Item2;
                dist[order[j].pos] += (nextv - prevv) / (maxv - minv);
            }
        }
        return dist;
    }

    private double Normal(double mu, double sigma)
    {
        var u1 = 1.0 - _rng.NextDouble();
        var u2 = 1.0 - _rng.NextDouble();
        var r = Math.Sqrt(-2.0 * Math.Log(u1));
        var theta = 2.0 * Math.PI * u2;
        return mu + sigma * r * Math.Cos(theta);
    }
}