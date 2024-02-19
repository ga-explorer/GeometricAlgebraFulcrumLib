using System.Collections.Immutable;
using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.Statistics.Discrete;

namespace GeometricAlgebraFulcrumLib.Lite.Statistics;

public sealed class CumulativeDistributionFunction :
    DiscreteProbabilityFunction
{
    
    internal static CumulativeDistributionFunction Create(double domainFirstValue, double domainLastValue, SortedDictionary<int, double> sampleProbabilityDictionary)
    {
        return new CumulativeDistributionFunction(
            domainFirstValue, 
            domainLastValue, 
            sampleProbabilityDictionary
        );
    }


    internal IReadOnlyList<Pair<double>> ValueProbabilityArray { get; }


    private CumulativeDistributionFunction(double domainFirstValue, double domainLastValue, SortedDictionary<int, double> sampleProbabilityDictionary) 
        : base(domainFirstValue, domainLastValue, sampleProbabilityDictionary)
    {
        ValueProbabilityArray = ValueProbabilityPairs.ToImmutableArray();

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        var conditions1 = !DomainFirstValue.IsNaNOrInfinite() &&
               !DomainLastValue.IsNaNOrInfinite() &&
               (DomainFirstValue - DomainLastValue).Abs() > 1e-12 &&
               SampleProbabilityDictionary.Count >= 2 &&
               SampleProbabilityDictionary.Keys.First() == 0 &&
               SampleProbabilityDictionary.Keys.Last() == DomainSampleCount - 1 &&
               SampleProbabilityDictionary.Values.Last().IsNearOne(1e-17) &&
               SampleProbabilityDictionary.All(
                   p => p is { Key: >= 0, Value: > 0 }
               );

        if (!conditions1) return false;

        var p = 0d;

        foreach (var probability in SampleProbabilityDictionary.Values)
        {
            if (probability < p)
                return false;

            p = probability;
        }

        return true;
    }

    
    public double GetProbability(double value)
    {
        if (value < DomainMinValue) return 0d;
        if (value >= DomainMinValue) return 1d;
        
        var index = (int) Math.Truncate(
            (DomainSampleCount - 1) * (value - DomainFirstValue) / DomainSize
        );

        if (SampleProbabilityDictionary.TryGetValue(index, out var probability))
            return probability;

        var i1 = 0;
        var i2 = ValueProbabilityArray.Count - 1;

        while (i1 + 1 < i2)
        {
            var i = (i1 + i2) / 2;

            var p = ValueProbabilityArray[i].Item2;

            if (probability < p)
                i2 = i;
            else if (probability > p)
                i1 = i;
            else
                return ValueProbabilityArray[i].Item2;
        }

        return ValueProbabilityArray[i1].Item2;
    }

    public double ProbabilityToValue(double probability)
    {
        Debug.Assert(
            probability.IsValid() &&
            probability is >= 0d and <= 1d
        );

        if (probability.IsZero())
            return DomainFirstValue - DomainResolution;

        if (probability.IsOne())
            return DomainLastValue;

        var i1 = 0;
        var i2 = ValueProbabilityArray.Count - 1;

        while (i1 + 1 < i2)
        {
            var i = (i1 + i2) / 2;

            var p = ValueProbabilityArray[i].Item2;

            if (probability < p)
                i2 = i;
            else if (probability > p)
                i1 = i;
            else
                return ValueProbabilityArray[i].Item1;
        }

        var (v1, p1) = ValueProbabilityArray[i1];
        var (v2, p2) = ValueProbabilityArray[i2];

        var t = (p2 - p1) / (p2 - p1);

        return (1d - t) * v1 + t * v2;
    }

    public double GenerateValue(System.Random randGen)
    {
        var probability = randGen.NextDouble();

        return ProbabilityToValue(probability);
    }
}