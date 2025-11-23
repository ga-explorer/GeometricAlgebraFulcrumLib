using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public static class PhCurve2DGeneticFitter
{
    private static readonly Random Rand = new Random();

    public static (BernsteinPolynomial u, BernsteinPolynomial v, BernsteinPolynomial w)
        FitQuadraticPHCurve_GA(
            LinFloat64Vector2D P0, LinFloat64Vector2D P1,
            LinFloat64Vector2D V0, LinFloat64Vector2D V1,
            double lambda = 100.0,
            int populationSize = 100,
            int generations = 300,
            double mutationRate = 0.1)
    {
        var numGenes = 9; // 3 for u, 3 for v, 3 for w
        var population = InitializePopulation(populationSize, numGenes);

        for (var gen = 0; gen < generations; gen++)
        {
            // Compute fitness for all individuals
            var scored = population
                .Select(ind => (individual: ind, fitness: EvaluateFitness(ind, P0, P1, V0, V1, lambda)))
                .OrderByDescending(x => x.fitness)
                .ToList();

            // Elitism: carry top 5% forward directly
            var eliteCount = Math.Max(1, populationSize / 20);
            var newPopulation = scored.Take(eliteCount).Select(x => x.individual).ToList();

            // Breed new offspring
            while (newPopulation.Count < populationSize)
            {
                var parent1 = TournamentSelect(scored);
                var parent2 = TournamentSelect(scored);

                var child = Crossover(parent1, parent2);
                Mutate(child, mutationRate);
                newPopulation.Add(child);
            }

            population = newPopulation.ToArray();

            if (gen % 25 == 0)
            {
                Console.WriteLine($"Generation {gen}, best fitness = {scored.First().fitness:F6}");
            }
        }

        // Return best individual as Bernstein polynomials
        var best = population
            .Select(ind => (ind, fit: EvaluateFitness(ind, P0, P1, V0, V1, lambda)))
            .OrderByDescending(x => x.fit)
            .First().ind;

        var U = BernsteinPolynomial.Create(best.Take(3).ToArray());
        var V = BernsteinPolynomial.Create(best.Skip(3).Take(3).ToArray());
        var W = BernsteinPolynomial.Create(best.Skip(6).Take(3).ToArray());
        return (U, V, W);
    }

    // --- Genetic Algorithm Core Helpers ---

    private static double[][] InitializePopulation(int size, int numGenes)
    {
        var pop = new double[size][];
        for (var i = 0; i < size; i++)
        {
            pop[i] = Enumerable.Range(0, numGenes)
                .Select(_ => Rand.NextDouble() * 2.0 - 1.0) // random [-1,1]
                .ToArray();
        }
        return pop;
    }

    private static double EvaluateFitness(double[] coeffs, LinFloat64Vector2D p0, LinFloat64Vector2D p1, LinFloat64Vector2D v0, LinFloat64Vector2D v1, double lambda)
    {
        var u = BernsteinPolynomial.Create(coeffs.Take(3).ToArray());
        var v = BernsteinPolynomial.Create(coeffs.Skip(3).Take(3).ToArray());
        var w = BernsteinPolynomial.Create(coeffs.Skip(6).Take(3).ToArray());

        // Derivatives
        var xPrime = w * (u.Square() - v.Square());
        var yPrime = w * (u * v * 2.0);

        // Integrate positions
        var x = xPrime.Integral() + p0[0];
        var y = yPrime.Integral() + p0[1];

        // Evaluate endpoints
        var p0Est = (x.GetValue(0), y.GetValue(0));
        var p1Est = (x.GetValue(1), y.GetValue(1));
        var v0Est = (xPrime.GetValue(0), yPrime.GetValue(0));
        var v1Est = (xPrime.GetValue(1), yPrime.GetValue(1));

        // Compute error
        var posErr = (p0Est - p0).L2Norm() + (p1Est - p1).L2Norm();
        var tanErr = (v0Est - v0).L2Norm() + (v1Est - v1).L2Norm();
        var totalErr = posErr * posErr + lambda * tanErr * tanErr;

        return 1.0 / (1.0 + totalErr); // higher fitness = lower error
    }

    private static double[] TournamentSelect(List<(double[] individual, double fitness)> scored, int k = 3)
    {
        var candidates = Enumerable.Range(0, k)
            .Select(_ => scored[Rand.Next(scored.Count)])
            .ToList();
        return candidates.OrderByDescending(c => c.fitness).First().individual.ToArray();
    }

    private static double[] Crossover(double[] p1, double[] p2)
    {
        var len = p1.Length;
        var point = Rand.Next(1, len - 1);
        var child = new double[len];
        for (var i = 0; i < len; i++)
            child[i] = i < point ? p1[i] : p2[i];
        return child;
    }

    private static void Mutate(double[] individual, double rate)
    {
        for (var i = 0; i < individual.Length; i++)
        {
            if (Rand.NextDouble() < rate)
            {
                individual[i] += (Rand.NextDouble() * 2 - 1) * 0.1; // small random tweak
            }
        }
    }


    public static void Example()
    {
        var P0 = (0.0, 0.0);
        var P1 = (1.0, 1.0);
        var V0 = (1.0, 0.0);
        var V1 = (0.0, 1.0);

        var (u, v, w) = 
            FitQuadraticPHCurve_GA(P0, P1, V0, V1);

        Console.WriteLine($"Optimized u(t): {string.Join(", ", u.Coeffs)}");
        Console.WriteLine($"Optimized v(t): {string.Join(", ", v.Coeffs)}");
        Console.WriteLine($"Optimized w(t): {string.Join(", ", w.Coeffs)}");
    }
}