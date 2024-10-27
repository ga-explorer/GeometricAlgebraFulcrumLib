using System.Collections.Immutable;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Evaluators;

public abstract class CGpChromosomeEvaluator
{
    public sealed class AdamOptimizer
    {
        private static double[] AffineCombination(IReadOnlyList<double> m1, IReadOnlyList<double> m2, double t)
        {
            Debug.Assert(m1.Count == m2.Count);
            Debug.Assert(t is >= 0 and <= 1);

            var s = 1 - t;
            var result = new double[m1.Count];

            for (var i = 0; i < m1.Count; i++)
                result[i] = m1[i] * t + m2[i] * s;

            return result;
        }
        
        private static double[] AffineCombination2(IReadOnlyList<double> m1, IReadOnlyList<double> m2, double t)
        {
            Debug.Assert(m1.Count == m2.Count);
            Debug.Assert(t is >= 0 and <= 1);

            var s = 1 - t;
            var result = new double[m1.Count];

            for (var i = 0; i < m1.Count; i++)
                result[i] = m1[i] * t + m2[i] * m2[i] * s;

            return result;
        }


        private const double Beta1 = 0.9d;
        private const double Beta2 = 0.999d;
        private const double Epsilon = 0.1d;

        private double[] _m;
        private double[] _v;

        public double StepSize { get; }


        public AdamOptimizer(int size, double stepSize)
        {
            _m = new double[size];
            _v = new double[size];

            StepSize = stepSize;
        }

        public double[] UpdateWeights(double[] weights, double[] gradient, int iteration)
        {
            _m = AffineCombination(_m, gradient, Beta1);
            _v = AffineCombination2(_v, gradient, Beta2);

            var c1 = 1d / (1 - Math.Pow(Beta1, iteration));
            var c2 = 1d / (1 - Math.Pow(Beta2, iteration));

            for (var i = 0; i < weights.Length; i++)
            {
                var mHatItem = _m[i] * c1;
                var vHatItem = _v[i] * c2;

                weights[i] -= StepSize * mHatItem / (Math.Sqrt(vHatItem) + Epsilon);
            }

            return weights;
        }
    }

    public abstract double ComputeCost(CGpChromosome chromosome);

    private double[] ComputeCostGradient(CGpChromosome chromosome, IReadOnlyList<CGpNodeInputWeight> weightArray, double epsilon = 1e-7)
    {
        var gradientArray = new double[weightArray.Count];

        var f0 = ComputeCost(chromosome);
        for (var weightIndex = 0; weightIndex < weightArray.Count; weightIndex++)
        {
            var weight = weightArray[weightIndex];
            var weightValue = weight.Value;
            
            // Set new value for this active parametric weight
            weight.SetValue(weightValue + epsilon);

            // Compute cost at new weight value
            var f1 = ComputeCost(chromosome);

            // Compute gradient value for this active parametric weight 
            gradientArray[weightIndex] = (f1 - f0) / epsilon;

            // Restore original value for this active parametric weight 
            weight.SetValue(weightValue);
        }

        return gradientArray;
    }

    public void OptimizeCost(CGpChromosome chromosome, double epsilon = 1e-7)
    {
        var weightArray = 
            chromosome.ActiveNodeParametricWeights.ToImmutableArray();

        var weightValueArray = 
            weightArray
                .Select(w => w.Value)
                .ToArray();

        var adamOptimizer = new AdamOptimizer(weightValueArray.Length, 0.1);

        var iterationCount = 100;

        Console.WriteLine("Optimizing Chromosome Parameters");
        Console.WriteLine("   Cost Before: " + ComputeCost(chromosome));

        for (var iteration = 1; iteration <= iterationCount; iteration++)
        {
            var gradients = ComputeCostGradient(chromosome, weightArray, epsilon);

            weightValueArray = adamOptimizer.UpdateWeights(weightValueArray, gradients, iteration);

            for (var i = 0; i < weightValueArray.Length; i++)
                weightArray[i].SetValue(weightValueArray[i]);

            Console.WriteLine($"   Cost After Iteration{iteration}: {ComputeCost(chromosome)}");
        }

        
    }
}