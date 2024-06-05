using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Statistics.Discrete
{
    public sealed class DiscreteProbabilityMassFunction :
        DiscreteProbabilityFunction
    {
        public static DiscreteProbabilityMassFunction CreateFromHistogram(IReadOnlyDictionary<double, double> sparseDictionary, int domainSampleCount)
        {
            Debug.Assert(
                domainSampleCount > 0 &&
                sparseDictionary.Count > 0 &&
                sparseDictionary.Values.All(p => p > 0)
            );

            var histogramSum = sparseDictionary.Values.Sum();

            if (domainSampleCount == 1)
            {
                var meanValue =
                    sparseDictionary
                        .Select(p => p.Key * p.Value)
                        .Sum() / histogramSum;

                var sampleProbabilityDictionary =
                    new SortedDictionary<int, double>() { { 0, 1d } };

                return new DiscreteProbabilityMassFunction(
                    meanValue,
                    meanValue,
                    sampleProbabilityDictionary
                );
            }
            else
            {
                var domainFirstValue = sparseDictionary.Keys.Min();
                var domainLastValue = sparseDictionary.Keys.Max();
                var domainResolution = (domainLastValue - domainFirstValue) / (domainSampleCount - 1);

                var sampleProbabilityDictionary = new SortedDictionary<int, double>();

                foreach (var (value, probability) in sparseDictionary)
                {
                    var i = (int)Math.Round((value - domainFirstValue) / domainResolution);

                    if (sampleProbabilityDictionary.TryGetValue(i, out var p))
                        sampleProbabilityDictionary[i] = probability + p;
                    else
                        sampleProbabilityDictionary.Add(i, probability);
                }

                return new DiscreteProbabilityMassFunction(
                    domainFirstValue,
                    domainLastValue,
                    sampleProbabilityDictionary.ScaleProbabilities(1d / histogramSum)
                );
            }
        }

        public static DiscreteProbabilityMassFunction CreateFromHistogram(Random randomGenerator, double domainFirstValue, double domainLastValue, int domainSampleCount, int samplesCount = 10000000)
        {
            var histogram = randomGenerator.CreateHistogram(
                domainFirstValue,
                domainLastValue,
                domainSampleCount,
                samplesCount
            );

            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var histogramSum = 0d;

            for (var i = 0; i < domainSampleCount; i++)
            {
                var count = histogram[i].Count;

                if (count == 0) continue;

                var p = count / histogram.DataCount;

                histogramSum += p;
                sampleProbabilityDictionary.Add(i, p);
            }

            return new DiscreteProbabilityMassFunction(
                domainFirstValue,
                domainLastValue,
                sampleProbabilityDictionary.ScaleProbabilities(1d / histogramSum)
            );
        }

        public static DiscreteProbabilityMassFunction CreateUniform(double domainFirstValue, double domainLastValue, int domainSampleCount)
        {
            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var p = 1d / domainSampleCount;

            for (var i = 0; i < domainSampleCount; i++)
                sampleProbabilityDictionary.Add(i, p);

            return new DiscreteProbabilityMassFunction(
                domainFirstValue,
                domainLastValue,
                sampleProbabilityDictionary
            );
        }

        public static DiscreteProbabilityMassFunction CreateBinomial(int trialCount, double successProbability, double zeroEpsilon = 1e-12)
        {
            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var pLn = Math.Log(successProbability);
            var qLn = Math.Log(1d - successProbability);
            var histogramSum = 0d;
            var domainFirstValue = 0;
            var domainLastValue = trialCount;

            for (var k = 0; k <= trialCount; k++)
            {
                var p = Math.Exp(
                    SpecialFunctions.FactorialLn(trialCount) -
                    SpecialFunctions.FactorialLn(k) -
                    SpecialFunctions.FactorialLn(trialCount - k) +
                    k * pLn +
                    (trialCount - k) * qLn
                );
                //n.GetBinomialCoefficient(k) * 
                //successProbability.Power(k) *
                //failureProbability.Power(trialCount - k);

                if (p <= zeroEpsilon) continue;

                domainFirstValue = k;

                histogramSum += p;

                sampleProbabilityDictionary.Add(0, p);

                break;
            }

            for (var k = domainFirstValue + 1; k <= trialCount; k++)
            {
                var p = Math.Exp(
                    SpecialFunctions.FactorialLn(trialCount) -
                    SpecialFunctions.FactorialLn(k) -
                    SpecialFunctions.FactorialLn(trialCount - k) +
                    k * pLn +
                    (trialCount - k) * qLn
                );

                if (p <= zeroEpsilon)
                {
                    domainLastValue = k - 1;

                    break;
                }

                histogramSum += p;

                sampleProbabilityDictionary.Add(k - domainFirstValue, p);
            }

            return new DiscreteProbabilityMassFunction(
                domainFirstValue,
                domainLastValue,
                sampleProbabilityDictionary.ScaleProbabilities(1d / histogramSum)
            );
        }

        public static DiscreteProbabilityMassFunction CreatePoisson(double mean, double zeroEpsilon = 1e-12)
        {
            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var histogramSum = 0d;
            var domainFirstValue = 0;
            var domainLastValue = int.MaxValue - 1;

            var meanLn = Math.Log(mean);

            for (var k = 0; k < int.MaxValue; k++)
            {
                var p = Math.Exp(
                    k * meanLn - mean - SpecialFunctions.FactorialLn(k)
                );
                //mean.Power(k) * expMean / SpecialFunctions.Factorial(k);

                if (p <= zeroEpsilon) continue;

                domainFirstValue = k;

                histogramSum += p;

                sampleProbabilityDictionary.Add(0, p);

                break;
            }

            for (var k = domainFirstValue + 1; k < int.MaxValue; k++)
            {
                var p = Math.Exp(
                    k * meanLn - mean - SpecialFunctions.FactorialLn(k)
                );

                if (p <= zeroEpsilon)
                {
                    domainLastValue = k - 1;

                    break;
                }

                histogramSum += p;

                sampleProbabilityDictionary.Add(k - domainFirstValue, p);
            }

            return new DiscreteProbabilityMassFunction(
                domainFirstValue,
                domainLastValue,
                sampleProbabilityDictionary.ScaleProbabilities(1d / histogramSum)
            );
        }

        public static DiscreteProbabilityMassFunction CreateNormal(double mean, double standardDeviation, int domainSampleCount, double zeroEpsilon = 1e-12)
        {
            var variance =
                standardDeviation * standardDeviation;

            var halfSize = Math.Sqrt(
                -2 * variance * Math.Log(
                    Math.Sqrt(2 * Math.PI) * standardDeviation * zeroEpsilon
                )
            );

            var domainFirstValue = mean - halfSize;
            var domainLastValue = mean + halfSize;

            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var histogramSum = 0d;
            for (var i = 0; i < domainSampleCount; i++)
            {
                var t = i / (double)(domainSampleCount - 1);
                var x = (1 - t) * domainFirstValue + t * domainLastValue;

                var p = Phi(x);
                histogramSum += p;

                sampleProbabilityDictionary.Add(i, p);
            }

            return new DiscreteProbabilityMassFunction(
                domainFirstValue,
                domainLastValue,
                sampleProbabilityDictionary.ScaleProbabilities(1d / histogramSum)
            );

            // Standard normal distribution function
            // https://en.wikipedia.org/wiki/Normal_distribution
            double Phi(double x)
            {
                var z = (x - mean) / standardDeviation;

                return Math.Exp(-z * z) / (Math.Sqrt(2d * Math.PI) * standardDeviation);
            }
        }

        public static DiscreteProbabilityMassFunction CreateExponential(double rate, int domainSampleCount, double zeroEpsilon = 1e-12)
        {
            var domainLastValue = -Math.Log(zeroEpsilon / rate) / rate;

            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var histogramSum = 0d;
            for (var i = 0; i < domainSampleCount; i++)
            {
                var x = domainLastValue * i / (domainSampleCount - 1);

                var p = rate * Math.Exp(-rate * x);
                histogramSum += p;

                sampleProbabilityDictionary.Add(i, p);
            }

            return new DiscreteProbabilityMassFunction(
                0d,
                domainLastValue,
                sampleProbabilityDictionary.ScaleProbabilities(1d / histogramSum)
            );
        }


        public static DiscreteProbabilityMassFunction operator -(DiscreteProbabilityMassFunction pmf)
        {
            return pmf.Negative();
        }

        public static DiscreteProbabilityMassFunction operator +(DiscreteProbabilityMassFunction pmf, double value)
        {
            return pmf.Add(value);
        }

        public static DiscreteProbabilityMassFunction operator +(double value, DiscreteProbabilityMassFunction pmf)
        {
            return pmf.Add(value);
        }

        public static DiscreteProbabilityMassFunction operator +(DiscreteProbabilityMassFunction pmf1, DiscreteProbabilityMassFunction pmf2)
        {
            return pmf1.Add(pmf2);
        }

        public static DiscreteProbabilityMassFunction operator -(DiscreteProbabilityMassFunction pmf, double value)
        {
            return pmf.Add(-value);
        }

        public static DiscreteProbabilityMassFunction operator -(double value, DiscreteProbabilityMassFunction pmf)
        {
            return pmf.Negative().Add(value);
        }

        public static DiscreteProbabilityMassFunction operator -(DiscreteProbabilityMassFunction pmf1, DiscreteProbabilityMassFunction pmf2)
        {
            return pmf1.Subtract(pmf2);
        }

        public static DiscreteProbabilityMassFunction operator *(DiscreteProbabilityMassFunction pmf, double value)
        {
            return pmf.Times(value);
        }

        public static DiscreteProbabilityMassFunction operator *(double value, DiscreteProbabilityMassFunction pmf)
        {
            return pmf.Times(value);
        }

        public static DiscreteProbabilityMassFunction operator *(DiscreteProbabilityMassFunction pmf1, DiscreteProbabilityMassFunction pmf2)
        {
            return pmf1.Times(pmf2);
        }

        public static DiscreteProbabilityMassFunction operator /(DiscreteProbabilityMassFunction pmf, double value)
        {
            return pmf.Times(1d / value);
        }

        public static DiscreteProbabilityMassFunction operator /(double value, DiscreteProbabilityMassFunction pmf)
        {
            return pmf.Inverse().Times(value);
        }

        public static DiscreteProbabilityMassFunction operator /(DiscreteProbabilityMassFunction pmf1, DiscreteProbabilityMassFunction pmf2)
        {
            return pmf1.Divide(pmf2);
        }


        //private IReadOnlyList<double> _inverseCdfArray = Array.Empty<double>();
        //public IReadOnlyList<double> InverseCdfArray
        //{
        //    get
        //    {
        //        if (_inverseCdfArray.Count == 0)
        //            _inverseCdfArray = GetInverseCdfArray();

        //        return _inverseCdfArray;
        //    }
        //}

        private DiscreteProbabilityMassFunction(double domainFirstValue, double domainLastValue, SortedDictionary<int, double> sampleProbabilityDictionary)
            : base(domainFirstValue, domainLastValue, sampleProbabilityDictionary)
        {
            Debug.Assert(IsValid());
        }


        public override bool IsValid()
        {
            return !DomainFirstValue.IsNaNOrInfinite() &&
                   !DomainLastValue.IsNaNOrInfinite() &&
                   (DomainFirstValue - DomainLastValue).Abs() > 1e-12 &&
                   SampleProbabilityDictionary.Count >= 2 &&
                   SampleProbabilityDictionary.Keys.First() == 0 &&
                   SampleProbabilityDictionary.Keys.Last() == DomainSampleCount - 1 &&
                   SampleProbabilityDictionary.Values.Sum().IsNearOne(1e-7) &&
                   SampleProbabilityDictionary.All(
                       p => p is { Key: >= 0, Value: > 0 }
                   );
        }

        public DiscreteProbabilityMassFunction TrimProbabilities(double zeroEpsilon = 1e-12)
        {
            var sum =
                SampleProbabilityDictionary
                    .Values
                    .Where(v => v > zeroEpsilon)
                    .Sum();

            var i1 = SampleProbabilityDictionary
                .First(p => p.Value > zeroEpsilon)
                .Key;

            var i2 = SampleProbabilityDictionary
                .Last(p => p.Value > zeroEpsilon)
                .Key;

            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var sumInv = 1d / sum;

            foreach (var (i, p) in SampleProbabilityDictionary)
            {
                if (p > zeroEpsilon)
                    sampleProbabilityDictionary.Add(i - i1, p * sumInv);
            }

            var domainFirstSample = GetDomainValue(i1);
            var domainLastSample = GetDomainValue(i2);

            return new DiscreteProbabilityMassFunction(
                domainFirstSample,
                domainLastSample,
                sampleProbabilityDictionary
            );
        }


        public double GetProbability(Func<double, bool> condition)
        {
            return ValueProbabilityPairs
                .Where(p => condition(p.Item1))
                .Select(p => p.Item2)
                .Sum();
        }

        public double GetProbability(double maxValue)
        {
            return GetProbability(x => x <= maxValue);
        }

        public double GetProbability(double minValue, double maxValue)
        {
            return GetProbability(x => x >= minValue && x <= maxValue);
        }

        public DiscreteProbabilityMassFunction ResetDomain(double domainFirstValue, double domainLastValue)
        {
            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            foreach (var (i, p) in SampleProbabilityDictionary)
                sampleProbabilityDictionary.Add(i, p);

            return new DiscreteProbabilityMassFunction(
                domainFirstValue,
                domainLastValue,
                sampleProbabilityDictionary
            );
        }

        public DiscreteProbabilityMassFunction MapDomain(Func<double, double> scalarMap, int domainSampleCount)
        {
            var sparseDictionary = new Dictionary<double, double>();

            foreach (var (x1, p1) in ValueProbabilityPairs)
            {
                var x = scalarMap(x1);

                if (x.IsNaNOrInfinite()) continue;

                if (sparseDictionary.TryGetValue(x, out var pOld))
                    sparseDictionary[x] = pOld + p1;
                else
                    sparseDictionary.Add(x, p1);
            }

            return CreateFromHistogram(sparseDictionary, domainSampleCount);
        }

        public DiscreteProbabilityMassFunction MapDomain(DiscreteProbabilityMassFunction pmf2, Func<double, double, double> scalarMap, int domainSampleCount)
        {
            var sparseDictionary = new Dictionary<double, double>();

            foreach (var (x1, p1) in ValueProbabilityPairs)
                foreach (var (x2, p2) in pmf2.ValueProbabilityPairs)
                {
                    var x = scalarMap(x1, x2);
                    var p = p1 + p2;

                    if (x.IsNaNOrInfinite()) continue;

                    if (sparseDictionary.TryGetValue(x, out var pOld))
                        sparseDictionary[x] = pOld + p;
                    else
                        sparseDictionary.Add(x, p);
                }

            return CreateFromHistogram(sparseDictionary, domainSampleCount);
        }

        public DiscreteProbabilityMassFunction JoinDomain(DiscreteProbabilityMassFunction pmf2, int domainSampleCount)
        {
            var sparseDictionary = new Dictionary<double, double>();

            foreach (var (x, p) in ValueProbabilityPairs)
                sparseDictionary.Add(x, p);

            foreach (var (x, p) in pmf2.ValueProbabilityPairs)
            {
                if (sparseDictionary.TryGetValue(x, out var pOld))
                    sparseDictionary[x] = pOld + p;
                else
                    sparseDictionary.Add(x, p);
            }

            return CreateFromHistogram(sparseDictionary, domainSampleCount);
        }

        public DiscreteProbabilityMassFunction ShiftDomain(double delta)
        {
            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            foreach (var (i, p) in SampleProbabilityDictionary)
                sampleProbabilityDictionary.Add(i, p);

            return new DiscreteProbabilityMassFunction(
                DomainFirstValue + delta,
                DomainLastValue + delta,
                sampleProbabilityDictionary
            );
        }


        public DiscreteProbabilityMassFunction Negative()
        {
            return Times(-1d);
        }

        public DiscreteProbabilityMassFunction Inverse()
        {
            return MapDomain(
                x => 1 / x,
                DomainSampleCount
            );
        }

        public DiscreteProbabilityMassFunction Add(double value)
        {
            Debug.Assert(!value.IsNaNOrInfinite());

            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            foreach (var (i, p) in SampleProbabilityDictionary)
                sampleProbabilityDictionary.Add(i, p);

            return new DiscreteProbabilityMassFunction(
                DomainFirstValue + value,
                DomainLastValue + value,
                sampleProbabilityDictionary
            );
        }

        public DiscreteProbabilityMassFunction Subtract(double value)
        {
            return Add(-value);
        }

        public DiscreteProbabilityMassFunction Times(double value)
        {
            Debug.Assert(!value.IsNaNOrInfinite());

            if (value.IsZero())
                return new DiscreteProbabilityMassFunction(
                    0d,
                    0d,
                    new SortedDictionary<int, double>() { { 0, 1d } }
                );

            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            foreach (var (i, p) in SampleProbabilityDictionary)
                sampleProbabilityDictionary.Add(i, p);

            return new DiscreteProbabilityMassFunction(
                DomainFirstValue * value,
                DomainLastValue * value,
                sampleProbabilityDictionary
            );
        }

        public DiscreteProbabilityMassFunction Divide(double value)
        {
            return Times(1d / value);
        }


        public DiscreteProbabilityMassFunction Add(DiscreteProbabilityMassFunction pmf2)
        {
            return MapDomain(
                pmf2,
                (x, y) => x + y,
                Math.Max(DomainSampleCount, pmf2.DomainSampleCount)
            );
        }

        public DiscreteProbabilityMassFunction Subtract(DiscreteProbabilityMassFunction pmf2)
        {
            return MapDomain(
                pmf2,
                (x, y) => x - y,
                Math.Max(DomainSampleCount, pmf2.DomainSampleCount)
            );
        }

        public DiscreteProbabilityMassFunction Times(DiscreteProbabilityMassFunction pmf2)
        {
            return MapDomain(
                pmf2,
                (x, y) => x * y,
                Math.Max(DomainSampleCount, pmf2.DomainSampleCount)
            );
        }

        public DiscreteProbabilityMassFunction Divide(DiscreteProbabilityMassFunction pmf2)
        {
            return MapDomain(
                pmf2,
                (x, y) => x / y,
                Math.Max(DomainSampleCount, pmf2.DomainSampleCount)
            );
        }


        public double GetMean()
        {
            return ValueProbabilityPairs.Select(
                p => p.Item1 * p.Item2
            ).Sum();
        }

        public double GetVariance()
        {
            var mean = GetMean();

            return ValueProbabilityPairs.Select(
                p => (p.Item1 - mean).Square() * p.Item2
            ).Sum();
        }

        public double GetStandardDeviation()
        {
            return GetVariance().Sqrt();
        }

        public double GetRelativeStandardDeviation()
        {
            var mean = GetMean();

            var standardDeviation = ValueProbabilityPairs.Select(
                p => (p.Item1 - mean).Square() * p.Item2
            ).Sum().Sqrt();

            return standardDeviation / mean;
        }

        public double GetSkewness()
        {
            return ValueProbabilityPairs.Select(
                p => Math.Pow(p.Item1, 3) * p.Item2
            ).Sum();
        }

        public double GetSkewnessCoefficient()
        {
            return GetMoment(3) / Math.Pow(GetVariance(), 1.5);
        }

        public double GetKurtosisCoefficient()
        {
            return GetMoment(4) / GetVariance().Square();
        }

        public double GetExcessKurtosisCoefficient()
        {
            return GetKurtosisCoefficient() - 3;
        }

        public double GetMoment(int n)
        {
            return ValueProbabilityPairs.Select(
                p => Math.Pow(p.Item1, n) * p.Item2
            ).Sum();
        }

        public double GetExpectedValue(Func<double, double> valueMap)
        {
            return ValueProbabilityPairs.Select(
                p => valueMap(p.Item1) * p.Item2
            ).Sum();
        }

        public IReadOnlyList<double> GetInverseCdfArray(int sampleCount = 2049)
        {
            var array = new double[sampleCount];

            array[0] = DomainFirstValue - DomainResolution;
            array[^1] = DomainLastValue;

            var index = 0;
            var vpPair = new Pair<double>(DomainFirstValue - DomainResolution, 0d);
            var cp = 0d;

            foreach (var (v, p) in ValueProbabilityPairs)
            {
                cp += p;
                var probability = 0d;

                while (probability < cp && index < sampleCount - 1)
                {
                    probability = index / (double)(sampleCount - 1);

                    var t = (probability - vpPair.Item2) / p;

                    array[index + 1] = (1d - t) * vpPair.Item1 + t * v;

                    index++;
                }

                vpPair = new Pair<double>(v, cp);
            }

            return array;
        }


        public CumulativeDistributionFunction GetCdf()
        {
            var sampleProbabilityDictionary = new SortedDictionary<int, double>();

            var probability = 0d;
            foreach (var (i, p) in SampleProbabilityDictionary)
            {
                probability += p;

                sampleProbabilityDictionary.Add(i, probability);
            }

            return CumulativeDistributionFunction.Create(
                DomainFirstValue,
                DomainLastValue,
                sampleProbabilityDictionary
            );
        }
    }
}
