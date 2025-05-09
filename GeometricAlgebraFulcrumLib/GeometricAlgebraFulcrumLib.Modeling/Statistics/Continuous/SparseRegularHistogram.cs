using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Statistics.Continuous;

/// <summary>
/// A histogram with regularly spaced bins, only non-zero bins are sparsely stored.
/// as a normalized histogram
/// </summary>
public sealed class SparseRegularHistogram :
    IReadOnlyList<double>
{
    public static SparseRegularHistogram CreateEmpty(double domainFirstValue, double domainLastValue, int binCount)
    {
        return new SparseRegularHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        );
    }

    public static SparseRegularHistogram CreateFromHistogram(SparseIrregularHistogram irregularHistogram, int binCount)
    {
        var domainFirstValue = irregularHistogram.DomainMinValue;
        var domainLastValue = irregularHistogram.DomainMaxValue;

        var hist = new SparseRegularHistogram(domainFirstValue, domainLastValue, binCount);

        var xDeltaInv = binCount / (domainLastValue - domainFirstValue);
        var x0 = domainFirstValue;

        for (var i = 1; i <= binCount; i++)
        {
            var t = i / (double)(binCount);

            var x1 = (1d - t) * domainFirstValue + t * domainLastValue;

            var height = irregularHistogram.GetAreaBetween(x0, x1) * xDeltaInv;

            hist.SetBinHeight(i - 1, height);

            x0 = x1;
        }

        return hist;
    }

    public static SparseRegularHistogram CreateFromHistogram(IReadOnlyDictionary<double, double> valueHeightDictionary, int binCount)
    {
        Debug.Assert(
            binCount > 0 &&
            valueHeightDictionary.Count > 0 &&
            valueHeightDictionary.Values.All(p => p > 0)
        );

        var domainFirstValue = valueHeightDictionary.Keys.Min();
        var domainLastValue = valueHeightDictionary.Keys.Max();

        var hist = new SparseRegularHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        );

        foreach (var (domainValue, height) in valueHeightDictionary)
            hist.AddHeight(domainValue, height);

        return hist;
    }

    public static SparseRegularHistogram CreateFromRandomSamples(Random randomGenerator, double domainFirstValue, double domainLastValue, int binCount, int randomSampleCount = 10000000)
    {
        var hist = new SparseRegularHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        );

        var domainValues =
            Enumerable
                .Repeat(0, randomSampleCount)
                .Select(_ => randomGenerator.NextDouble())
                .Where(v => v >= domainFirstValue && v <= domainLastValue);

        foreach (var domainValue in domainValues)
            hist.AddHeight(domainValue, 1);

        return hist.NormalizeHeights();
    }

    public static SparseRegularHistogram CreateUniform(double domainFirstValue, double domainLastValue, int binCount = 1)
    {
        var hist = new SparseRegularHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        );

        var height = 1d / Math.Abs(domainLastValue - domainFirstValue);

        for (var i = 0; i < binCount; i++)
            hist.AddBinHeight(i, height);

        return hist;
    }

    public static SparseRegularHistogram CreateNormal(double mean, double standardDeviation, int binCount, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var variance =
            standardDeviation * standardDeviation;

        var halfSize = Math.Sqrt(
            -2 * variance * Math.Log(
                Math.Sqrt(Math.Tau) * standardDeviation * zeroEpsilon
            )
        );

        var domainFirstValue = mean - halfSize;
        var domainLastValue = mean + halfSize;

        var indexHeightDictionary = new SortedDictionary<int, double>();

        for (var i = 0; i < binCount; i++)
        {
            var t = i / (double)(binCount - 1);
            var x = (1 - t) * domainFirstValue + t * domainLastValue;
            var p = Phi(x);

            indexHeightDictionary.Add(i, p);
        }

        var hist = new SparseRegularHistogram(
            domainFirstValue,
            domainLastValue,
            binCount,
            indexHeightDictionary
        );

        return hist.NormalizeHeights();

        // Standard normal distribution function
        // https://en.wikipedia.org/wiki/Normal_distribution
        double Phi(double x)
        {
            var z = (x - mean) / standardDeviation;

            return Math.Exp(-z * z) / (Math.Sqrt(Math.Tau) * standardDeviation);
        }
    }

    public static SparseRegularHistogram CreateExponential(double rate, int binCount, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var domainLastValue = -Math.Log(zeroEpsilon / rate) / rate;

        var indexHeightDictionary = new SortedDictionary<int, double>();

        for (var i = 0; i < binCount; i++)
        {
            var x = domainLastValue * i / (binCount - 1);
            var p = rate * Math.Exp(-rate * x);

            indexHeightDictionary.Add(i, p);
        }

        var hist = new SparseRegularHistogram(
            0d,
            domainLastValue,
            binCount,
            indexHeightDictionary
        );

        return hist.NormalizeHeights();
    }

    //public static SparseHistogram CreateBinomial(int trialCount, double successHeight, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    //{
    //    var indexHeightDictionary = new SortedDictionary<int, double>();

    //    var pLn = Math.Log(successHeight);
    //    var qLn = Math.Log(1d - successHeight);
    //    var histogramSum = 0d;
    //    var domainFirstValue = 0;
    //    var domainLastValue = trialCount;

    //    for (var k = 0; k <= trialCount; k++)
    //    {
    //        var p = Math.Exp(
    //            SpecialFunctions.FactorialLn(trialCount) -
    //            SpecialFunctions.FactorialLn(k) -
    //            SpecialFunctions.FactorialLn(trialCount - k) +
    //            k * pLn +
    //            (trialCount - k) * qLn
    //        );
    //        //n.GetBinomialCoefficient(k) * 
    //        //successHeight.Power(k) *
    //        //failureHeight.Power(trialCount - k);

    //        if (p <= zeroEpsilon) continue;

    //        domainFirstValue = k;

    //        histogramSum += p;

    //        indexHeightDictionary.Add(0, p);

    //        break;
    //    }

    //    for (var k = domainFirstValue + 1; k <= trialCount; k++)
    //    {
    //        var p = Math.Exp(
    //            SpecialFunctions.FactorialLn(trialCount) -
    //            SpecialFunctions.FactorialLn(k) -
    //            SpecialFunctions.FactorialLn(trialCount - k) +
    //            k * pLn +
    //            (trialCount - k) * qLn
    //        );

    //        if (p <= zeroEpsilon)
    //        {
    //            domainLastValue = k - 1;

    //            break;
    //        }

    //        histogramSum += p;

    //        indexHeightDictionary.Add(k - domainFirstValue, p);
    //    }

    //    return new SparseHistogram(
    //        domainFirstValue,
    //        domainLastValue,
    //        indexHeightDictionary.ScaleProbabilities(1d / histogramSum)
    //    );
    //}

    //public static SparseHistogram CreatePoisson(double mean, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    //{
    //    var indexHeightDictionary = new SortedDictionary<int, double>();

    //    var histogramSum = 0d;
    //    var domainFirstValue = 0;
    //    var domainLastValue = int.MaxValue - 1;

    //    var meanLn = Math.Log(mean);

    //    for (var k = 0; k < int.MaxValue; k++)
    //    {
    //        var p = Math.Exp(
    //            k * meanLn - mean - SpecialFunctions.FactorialLn(k)
    //        );
    //        //mean.Power(k) * expMean / SpecialFunctions.Factorial(k);

    //        if (p <= zeroEpsilon) continue;

    //        domainFirstValue = k;

    //        histogramSum += p;

    //        indexHeightDictionary.Add(0, p);

    //        break;
    //    }

    //    for (var k = domainFirstValue + 1; k < int.MaxValue; k++)
    //    {
    //        var p = Math.Exp(
    //            k * meanLn - mean - SpecialFunctions.FactorialLn(k)
    //        );

    //        if (p <= zeroEpsilon)
    //        {
    //            domainLastValue = k - 1;

    //            break;
    //        }

    //        histogramSum += p;

    //        indexHeightDictionary.Add(k - domainFirstValue, p);
    //    }

    //    return new SparseHistogram(
    //        domainFirstValue,
    //        domainLastValue,
    //        indexHeightDictionary.ScaleProbabilities(1d / histogramSum)
    //    );
    //}


    public static SparseRegularHistogram operator -(SparseRegularHistogram pmf)
    {
        return pmf.Negative();
    }

    public static SparseRegularHistogram operator +(SparseRegularHistogram pmf, double value)
    {
        return pmf.Add(value);
    }

    public static SparseRegularHistogram operator +(double value, SparseRegularHistogram pmf)
    {
        return pmf.Add(value);
    }

    public static SparseRegularHistogram operator +(SparseRegularHistogram pmf1, SparseRegularHistogram pmf2)
    {
        return pmf1.Add(pmf2);
    }

    public static SparseRegularHistogram operator -(SparseRegularHistogram pmf, double value)
    {
        return pmf.Add(-value);
    }

    public static SparseRegularHistogram operator -(double value, SparseRegularHistogram pmf)
    {
        return pmf.Negative().Add(value);
    }

    public static SparseRegularHistogram operator -(SparseRegularHistogram pmf1, SparseRegularHistogram pmf2)
    {
        return pmf1.Subtract(pmf2);
    }

    public static SparseRegularHistogram operator *(SparseRegularHistogram pmf, double value)
    {
        return pmf.Times(value);
    }

    public static SparseRegularHistogram operator *(double value, SparseRegularHistogram pmf)
    {
        return pmf.Times(value);
    }

    public static SparseRegularHistogram operator *(SparseRegularHistogram pmf1, SparseRegularHistogram pmf2)
    {
        return pmf1.Times(pmf2);
    }

    public static SparseRegularHistogram operator /(SparseRegularHistogram pmf, double value)
    {
        return pmf.Times(1d / value);
    }

    public static SparseRegularHistogram operator /(double value, SparseRegularHistogram pmf)
    {
        return pmf.Inverse().Times(value);
    }

    public static SparseRegularHistogram operator /(SparseRegularHistogram pmf1, SparseRegularHistogram pmf2)
    {
        return pmf1.Divide(pmf2);
    }


    /// <summary>
    /// A dictionary holding sparse information about histogram bins and probabilities
    /// </summary>
    private SortedDictionary<int, double> IndexHeightDictionary { get; set; }

    /// <summary>
    /// First value of domain range
    /// </summary>
    public double DomainFirstValue { get; private set; }

    /// <summary>
    /// Last value of domain range
    /// </summary>
    public double DomainLastValue { get; private set; }

    /// <summary>
    /// Smallest value of domain range
    /// </summary>
    public double DomainMinValue
        => Math.Min(DomainFirstValue, DomainLastValue);

    /// <summary>
    /// Largest value of domain range
    /// </summary>
    public double DomainMaxValue
        => Math.Max(DomainFirstValue, DomainLastValue);

    /// <summary>
    /// Domain range
    /// </summary>
    public Float64ScalarRange DomainRange
        => Float64ScalarRange.Create(DomainFirstValue, DomainLastValue);

    /// <summary>
    /// Number of bins in histogram
    /// </summary>
    public int BinCount { get; private set; }

    /// <summary>
    /// Signed distance between any two bins = signed width of single bin
    /// </summary>
    public double BinSignedWidth
        => (DomainLastValue - DomainFirstValue) / BinCount;

    /// <summary>
    /// Distance between any two bins = width of single bin
    /// </summary>
    public double BinWidth
        => Math.Abs(DomainLastValue - DomainFirstValue) / BinCount;

    /// <summary>
    /// Nearest bin to first domain value
    /// </summary>
    public double FirstBinMidValue
        => DomainFirstValue + BinSignedWidth * 0.5d;

    /// <summary>
    /// Nearest bin to last domain value
    /// </summary>
    public double LastBinMidValue
        => DomainLastValue - BinSignedWidth * 0.5d;

    /// <summary>
    /// Smallest bin value
    /// </summary>
    public double MinBinMidValue
        => DomainMinValue + BinWidth * 0.5d;

    /// <summary>
    /// Largest bin value
    /// </summary>
    public double MaxBinMidValue
        => DomainMaxValue - BinWidth * 0.5d;

    /// <summary>
    /// Distance between first and last bins
    /// </summary>
    public double BinRangeWidth
        => Math.Abs(DomainLastValue - DomainFirstValue) * (1d - 1d / BinCount);

    public Int32Range1D StoredBinIndexRange
        => Int32Range1D.Create(
            IndexHeightDictionary.Keys.First(),
            IndexHeightDictionary.Keys.Last()
        );

    public IEnumerable<Pair<double>> StoredBinValueHeightPairs
        => IndexHeightDictionary.Select(p =>
            new Pair<double>(
                GetBinMidValue(p.Key),
                p.Value
            )
        );

    public IEnumerable<HistogramBinData> StoredBinData
        => IndexHeightDictionary.Select(p =>
            new HistogramBinData(
                p.Key,
                GetBinMidValue(p.Key),
                BinWidth,
                p.Value
            )
        );

    public int Count
        => BinCount;

    public double this[int index]
    {
        get => IndexHeightDictionary.GetValueOrDefault(index, 0d);
        set
        {
            if (index < 0 || index >= BinCount)
                throw new IndexOutOfRangeException();

            if (value.IsNaNOrInfinite() || value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            if (value.Equals(0d))
                IndexHeightDictionary.Remove(index);
            else if (IndexHeightDictionary.ContainsKey(index))
                IndexHeightDictionary[index] = value;
            else
                IndexHeightDictionary.Add(index, value);
        }
    }


    private SparseRegularHistogram(double domainFirstValue, double domainLastValue, int binCount)
    {
        if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite() || binCount < 1)
            throw new InvalidOperationException();

        if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
            throw new InvalidOperationException();

        DomainFirstValue = domainFirstValue;
        DomainLastValue = domainLastValue;
        BinCount = binCount;

        IndexHeightDictionary = new SortedDictionary<int, double>();

        Debug.Assert(IsValid());
    }

    private SparseRegularHistogram(double domainFirstValue, double domainLastValue, int binCount, SortedDictionary<int, double> indexHeightDictionary)
    {
        if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite() || binCount < 1)
            throw new InvalidOperationException();

        if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
            throw new InvalidOperationException();

        DomainFirstValue = domainFirstValue;
        DomainLastValue = domainLastValue;
        BinCount = binCount;

        IndexHeightDictionary = indexHeightDictionary;

        Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        return !DomainFirstValue.IsNaNOrInfinite() &&
               !DomainLastValue.IsNaNOrInfinite() &&
               (DomainLastValue - DomainFirstValue).Abs() > 1e-12 &&
               BinCount > 0 &&
               IndexHeightDictionary.All(p =>
                   p.Key >= 0 && p.Key < BinCount &&
                   p.Value > 0
               );
    }

    public bool ContainsIndex(int index)
    {
        return index >= 0 && index < BinCount;
    }

    public bool ContainsValue(double domainValue)
    {
        if (DomainFirstValue <= DomainLastValue)
            return domainValue >= DomainFirstValue &&
                   domainValue <= DomainLastValue;

        return domainValue >= DomainLastValue &&
               domainValue <= DomainFirstValue;
    }

    public HistogramBinData GetBinData(int index)
    {
        var midValue =
            GetBinMidValue(index);

        var height =
            IndexHeightDictionary.GetValueOrDefault(index, 0d);

        return new HistogramBinData(index, midValue, BinWidth, height);
    }

    public int GetBinIndexContaining(double domainValue)
    {
        if (ContainsValue(domainValue))
            throw new ArgumentOutOfRangeException();

        if (BinCount == 1)
            return 0;

        var firstBin = FirstBinMidValue;
        var lastBin = LastBinMidValue;

        if (firstBin <= lastBin)
        {
            if (domainValue <= firstBin)
                return 0;

            if (domainValue >= lastBin)
                return BinCount - 1;
        }
        else if (lastBin < firstBin)
        {
            if (domainValue <= lastBin)
                return 0;

            if (domainValue >= firstBin)
                return BinCount - 1;
        }

        return (int)Math.Round(
            (BinCount - 1) * (domainValue - firstBin) / (lastBin - firstBin)
        );
    }

    public double GetBinMidValue(int index)
    {
        var lastIndex = BinCount - 1;

        if (index < 0 || index > lastIndex)
            throw new IndexOutOfRangeException();

        if (index == 0) return FirstBinMidValue;
        if (index == lastIndex) return LastBinMidValue;

        var t = index / (double)lastIndex;

        return (1 - t) * FirstBinMidValue + t * LastBinMidValue;
    }

    public double GetBinHeight(int index)
    {
        return IndexHeightDictionary.GetValueOrDefault(index, 0d);
    }

    public SparseRegularHistogram SetBinHeight(int index, double height)
    {
        if (index < 0 || index >= BinCount)
            throw new IndexOutOfRangeException();

        if (height.IsNaNOrInfinite() || height < 0)
            throw new ArgumentOutOfRangeException(nameof(height));

        if (height.Equals(0d))
            IndexHeightDictionary.Remove(index);
        else if (IndexHeightDictionary.ContainsKey(index))
            IndexHeightDictionary[index] = height;
        else
            IndexHeightDictionary.Add(index, height);

        return this;
    }

    public SparseRegularHistogram AddBinHeight(int index, double heightDelta)
    {
        return SetBinHeight(
            index,
            heightDelta + GetBinHeight(index)
        );
    }

    public SparseRegularHistogram SetHeight(double domainValue, double height)
    {
        var index = GetBinIndexContaining(domainValue);

        return SetBinHeight(index, height);
    }

    public SparseRegularHistogram AddHeight(double domainValue, double height)
    {
        var index = GetBinIndexContaining(domainValue);

        return AddBinHeight(index, height);
    }

    public SparseRegularHistogram AddHeight(double domainValue1, double domainValue2, double height)
    {
        var index1 = GetBinIndexContaining(domainValue1);
        var index2 = GetBinIndexContaining(domainValue2);

        if (index1 == index2)
            return AddBinHeight(index1, height);

        if (index1 < index2)
        {
            for (var i = index1; i <= index2; i++)
                AddBinHeight(i, height);

            return this;
        }

        for (var i = index2; i <= index1; i++)
            AddBinHeight(i, height);

        return this;
    }


    public SparseRegularHistogram MapHeights(Func<double, double> mappingFunc)
    {
        var indexHeightDictionary = new SortedDictionary<int, double>();

        foreach (var (i, h) in IndexHeightDictionary)
        {
            var height = mappingFunc(h);

            if (height.IsNaNOrInfinite() || height < 0)
                throw new InvalidOperationException();

            if (height > 0)
                indexHeightDictionary.Add(i, height);
        }

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }

    public SparseRegularHistogram MapHeights(Func<int, double, double> mappingFunc)
    {
        var indexHeightDictionary = new SortedDictionary<int, double>();

        foreach (var (i, h) in IndexHeightDictionary)
        {
            var height = mappingFunc(i, h);

            if (height.IsNaNOrInfinite() || height < 0)
                throw new InvalidOperationException();

            if (height > 0)
                indexHeightDictionary.Add(i, height);
        }

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }

    public SparseRegularHistogram MapHeights(Func<HistogramBinData, double> mappingFunc)
    {
        var indexHeightDictionary = new SortedDictionary<int, double>();

        foreach (var (index, oldHeight) in IndexHeightDictionary)
        {
            var midValue = GetBinMidValue(index);
            var binData = new HistogramBinData(index, midValue, BinWidth, oldHeight);

            var height = mappingFunc(binData);

            if (height.IsNaNOrInfinite() || height < 0)
                throw new InvalidOperationException();

            if (height > 0)
                indexHeightDictionary.Add(index, height);
        }

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }

    public SparseRegularHistogram ScaleHeights(double scalingFactor)
    {
        return MapHeights(h => h * scalingFactor);
    }

    public SparseRegularHistogram NormalizeHeights()
    {
        return ScaleHeights(1d / GetArea());
    }

    public SparseRegularHistogram TrimHeights(double zeroHeightEpsilon)
    {
        if (zeroHeightEpsilon <= 0) return this;

        var indexList =
            IndexHeightDictionary
                .Where(p => p.Value <= zeroHeightEpsilon)
                .Select(p => p.Key)
                .ToImmutableArray();

        foreach (var index in indexList)
            IndexHeightDictionary.Remove(index);

        return this;
    }

    public SparseRegularHistogram TrimHeightsByArea(double zeroAreaRatioEpsilon)
    {
        var area = IndexHeightDictionary.Values.Sum();
        var areaMax = (1d - zeroAreaRatioEpsilon) * area;
        var zeroEpsilon = 0d;

        var yValues =
            IndexHeightDictionary
                .Values
                .OrderByDescending(v => v);

        foreach (var yValue in yValues)
        {
            areaMax -= yValue;

            if (areaMax > 0) continue;

            zeroEpsilon = yValue;
            break;
        }

        return TrimHeights(zeroEpsilon);
    }


    public SparseRegularHistogram PrependBins(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return this;

        BinCount += count;
        DomainFirstValue -= BinSignedWidth * count;

        return this;
    }

    public SparseRegularHistogram AppendBins(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return this;

        BinCount += count;
        DomainLastValue += BinSignedWidth * count;

        return this;
    }

    public SparseRegularHistogram TrimBins()
    {
        if (IndexHeightDictionary.Count == 0)
            throw new InvalidOperationException();

        var firstIndex = IndexHeightDictionary.Keys.First();
        var lastIndex = IndexHeightDictionary.Keys.Last();

        if (firstIndex == 0 && lastIndex == BinCount - 1)
            return this;

        if (firstIndex > 0)
            DomainFirstValue += BinSignedWidth * firstIndex;

        if (lastIndex < BinCount - 1)
            DomainLastValue -= BinSignedWidth * (BinCount - 1 - lastIndex);

        var indexHeightDictionary = new SortedDictionary<int, double>();

        foreach (var (index, height) in IndexHeightDictionary)
            indexHeightDictionary.Add(index - firstIndex, height);

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }

    public SparseRegularHistogram TrimFirstBins()
    {
        if (IndexHeightDictionary.Count == 0)
            throw new InvalidOperationException();

        var firstIndex = IndexHeightDictionary.Keys.First();

        if (firstIndex == 0)
            return this;

        DomainFirstValue += BinSignedWidth * firstIndex;

        var indexHeightDictionary = new SortedDictionary<int, double>();

        foreach (var (index, height) in IndexHeightDictionary)
            indexHeightDictionary.Add(index - firstIndex, height);

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }

    public SparseRegularHistogram TrimLastBins()
    {
        if (IndexHeightDictionary.Count == 0)
            throw new InvalidOperationException();

        var lastIndex = IndexHeightDictionary.Keys.Last();

        if (lastIndex == BinCount - 1)
            return this;

        DomainLastValue -= BinSignedWidth * (BinCount - 1 - lastIndex);

        return this;
    }


    public double GetArea()
    {
        return BinWidth * IndexHeightDictionary.Values.Sum();
    }

    public double GetAreaBefore(double domainValue)
    {
        var binIndex =
            GetBinIndexContaining(domainValue);

        var area =
            GetBinData(binIndex).GetLengthBefore(domainValue);

        if (DomainFirstValue <= DomainLastValue)
        {
            area += IndexHeightDictionary
                .Where(p => p.Key < binIndex)
                .Select(p => p.Value)
                .Sum();
        }
        else
        {
            area += IndexHeightDictionary
                .Where(p => p.Key > binIndex)
                .Select(p => p.Value)
                .Sum();
        }

        return BinWidth * area;
    }

    public double GetAreaAfter(double domainValue)
    {
        var binIndex =
            GetBinIndexContaining(domainValue);

        var area =
            GetBinData(binIndex).GetLengthAfter(domainValue);

        if (DomainFirstValue <= DomainLastValue)
        {
            area += IndexHeightDictionary
                .Where(p => p.Key > binIndex)
                .Select(p => p.Value)
                .Sum();
        }
        else
        {
            area += IndexHeightDictionary
                .Where(p => p.Key < binIndex)
                .Select(p => p.Value)
                .Sum();
        }

        return BinWidth * area;
    }

    public double GetAreaBetween(double domainValue1, double domainValue2)
    {
        var area1 = GetAreaBefore(domainValue1);
        var area2 = GetAreaBefore(domainValue2);

        return Math.Abs(area2 - area1);
    }


    public SparseRegularHistogram ResetDomainRange(double domainFirstValue, double domainLastValue)
    {
        if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite())
            throw new InvalidOperationException();

        if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
            throw new InvalidOperationException();

        DomainFirstValue = domainFirstValue;
        DomainLastValue = domainLastValue;

        return this;
    }

    public SparseRegularHistogram FlipDomain()
    {
        return ResetDomainRange(
            DomainLastValue,
            DomainFirstValue
        );
    }

    public SparseRegularHistogram ShiftDomain(double delta)
    {
        return ResetDomainRange(
            DomainFirstValue + delta,
            DomainLastValue + delta
        );
    }


    public SparseRegularHistogram MapDomainUsingAffine(double scalingFactor, double offset)
    {
        var domainFirstValue = DomainFirstValue * scalingFactor + offset;
        var domainLastValue = DomainLastValue * scalingFactor + offset;

        var indexHeightDictionary = new SortedDictionary<int, double>();

        foreach (var (i, h) in IndexHeightDictionary)
            indexHeightDictionary.Add(i, h);

        return new SparseRegularHistogram(
            domainFirstValue,
            domainLastValue,
            BinCount,
            indexHeightDictionary
        );
    }

    public SparseRegularHistogram MapDomain(Func<double, double> scalarMap, int binCount)
    {
        var irregularHistogram = SparseIrregularHistogram.Create();

        foreach (var binData in StoredBinData)
        {
            var affineFunction =
                PiecewiseAffineFunction.CreateContinuous(
                    scalarMap,
                    binData.MinValue,
                    binData.MaxValue,
                    binCount,
                    0.1 * Math.PI / 180d
                );

            //var hist = new 

            foreach (var segment in affineFunction.FiniteSegments)
            {


                irregularHistogram.AddBin(
                    segment.YMid,
                    Math.Abs(segment.YDelta),
                    binData.Height
                );
            }
        }

        return CreateFromHistogram(irregularHistogram, binCount);
    }

    public SparseRegularHistogram MapDomain(SparseRegularHistogram pmf2, Func<double, double, double> scalarMap, int binCount)
    {
        var sparseDictionary = new Dictionary<double, double>();

        foreach (var (x1, p1) in StoredBinValueHeightPairs)
            foreach (var (x2, p2) in pmf2.StoredBinValueHeightPairs)
            {
                var x = scalarMap(x1, x2);
                var p = p1 * p2;

                if (x.IsNaNOrInfinite()) continue;

                if (sparseDictionary.TryGetValue(x, out var pOld))
                    sparseDictionary[x] = pOld + p;
                else
                    sparseDictionary.Add(x, p);
            }

        return CreateFromHistogram(sparseDictionary, binCount);
    }

    public SparseRegularHistogram JoinDomain(SparseRegularHistogram pmf2, int binCount)
    {
        var sparseDictionary = new Dictionary<double, double>();

        foreach (var (x, p) in StoredBinValueHeightPairs)
            sparseDictionary.Add(x, p);

        foreach (var (x, p) in pmf2.StoredBinValueHeightPairs)
        {
            if (sparseDictionary.TryGetValue(x, out var pOld))
                sparseDictionary[x] = pOld + p;
            else
                sparseDictionary.Add(x, p);
        }

        return CreateFromHistogram(sparseDictionary, binCount);
    }

    public SparseRegularHistogram Negative()
    {
        return Times(-1d);
    }

    public SparseRegularHistogram Inverse()
    {
        return MapDomain(
            x => 1 / x,
            BinCount
        );
    }

    public SparseRegularHistogram Add(double value)
    {
        return ShiftDomain(value);
    }

    public SparseRegularHistogram Subtract(double value)
    {
        return ShiftDomain(-value);
    }

    public SparseRegularHistogram Times(double value)
    {
        Debug.Assert(!value.IsNaNOrInfinite());

        return MapDomain(x => x * value, 1024);
    }

    public SparseRegularHistogram Divide(double value)
    {
        return Times(1d / value);
    }


    public SparseRegularHistogram Add(SparseRegularHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x + y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }

    public SparseRegularHistogram Subtract(SparseRegularHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x - y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }

    public SparseRegularHistogram Times(SparseRegularHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x * y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }

    public SparseRegularHistogram Divide(SparseRegularHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x / y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }


    public double GetMean()
    {
        return StoredBinValueHeightPairs.Select(
            p => p.Item1 * p.Item2
        ).Sum();
    }

    public double GetVariance()
    {
        var mean = GetMean();

        return StoredBinValueHeightPairs.Select(
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

        var standardDeviation = StoredBinValueHeightPairs.Select(
            p => (p.Item1 - mean).Square() * p.Item2
        ).Sum().Sqrt();

        return standardDeviation / mean;
    }

    public double GetSkewness()
    {
        return StoredBinValueHeightPairs.Select(
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
        return StoredBinValueHeightPairs.Select(
            p => Math.Pow(p.Item1, n) * p.Item2
        ).Sum();
    }

    public double GetExpectedValue(Func<double, double> valueMap)
    {
        return StoredBinValueHeightPairs.Select(
            p => valueMap(p.Item1) * p.Item2
        ).Sum();
    }


    //public PdfRandomGenerator CreateRandomGenerator()
    //{
    //    return new PdfRandomGenerator(this);
    //}

    //public PdfRandomGenerator CreateRandomGenerator(int seed)
    //{
    //    return new PdfRandomGenerator(this, seed);
    //}


    public PiecewiseAffineFunction GetProbabilityDensityFunction()
    {
        var affineFunction = new PiecewiseAffineFunction();

        var areaInv = 1d / GetArea();

        var index1 = IndexHeightDictionary.Keys.First();
        var binData1 = GetBinData(index1);
        var height1 = binData1.Height * areaInv;

        affineFunction.InsertBreakpoint(
            binData1.MinValue,
            0,
            height1,
            height1
        );

        if (IndexHeightDictionary.Count > 1)
        {
            foreach (var binData2 in StoredBinData.Skip(1))
            {
                var index2 = binData2.Index;
                var height2 = binData2.Height * areaInv;

                if (index2 > index1 + 1)
                {
                    affineFunction.InsertBreakpoint(
                        binData1.MaxValue,
                        height1,
                        0,
                        0
                    );

                    affineFunction.InsertBreakpoint(
                        binData2.MinValue,
                        0,
                        height2,
                        height2
                    );
                }
                else
                {
                    affineFunction.InsertBreakpoint(
                        binData2.MinValue,
                        height1,
                        height2,
                        height2
                    );
                }

                index1 = index2;
                binData1 = binData2;
                height1 = height2;
            }
        }

        affineFunction.InsertBreakpoint(
            binData1.MaxValue,
            height1,
            height1,
            0
        );

        return affineFunction;
    }

    public PiecewiseAffineFunction GetCumulativeDensityFunction()
    {
        var affineFunction = new PiecewiseAffineFunction();

        var areaInv = 1d / GetArea();

        var index1 = IndexHeightDictionary.Keys.First();
        var binData1 = GetBinData(index1);
        var area1 = 0d;

        foreach (var binData2 in StoredBinData)
        {
            var index2 = binData2.Index;
            var area2 = area1 + binData2.Area;

            if (index2 > index1 + 1)
            {
                affineFunction.InsertBreakpoint(
                    binData1.MaxValue,
                    area1 * areaInv
                );

                affineFunction.InsertBreakpoint(
                    binData2.MinValue,
                    area1 * areaInv
                );
            }
            else
            {
                affineFunction.InsertBreakpoint(
                    binData2.MinValue,
                    area1 * areaInv
                );
            }

            index1 = index2;
            binData1 = binData2;
            area1 = area2;
        }

        affineFunction.InsertBreakpoint(
            binData1.MaxValue,
            1
        );

        return affineFunction;
    }

    public PiecewiseAffineFunction GetSurvivorFunction()
    {
        var affineFunction = new PiecewiseAffineFunction();

        var areaInv = 1d / GetArea();

        var index1 = IndexHeightDictionary.Keys.First();
        var binData1 = GetBinData(index1);
        var area1 = 0d;

        foreach (var binData2 in StoredBinData)
        {
            var index2 = binData2.Index;
            var area2 = area1 + binData2.Area;

            if (index2 > index1 + 1)
            {
                affineFunction.InsertBreakpoint(
                    binData1.MaxValue,
                    1 - area1 * areaInv
                );

                affineFunction.InsertBreakpoint(
                    binData2.MinValue,
                    1 - area1 * areaInv
                );
            }
            else
            {
                affineFunction.InsertBreakpoint(
                    binData2.MinValue,
                    1 - area1 * areaInv
                );
            }

            index1 = index2;
            binData1 = binData2;
            area1 = area2;
        }

        affineFunction.InsertBreakpoint(
            binData1.MaxValue,
            0
        );

        return affineFunction;
    }

    public PiecewiseAffineFunction GetInverseDistributionFunction()
    {
        var affineFunction = new PiecewiseAffineFunction();

        var areaInv = 1d / GetArea();

        var index1 = IndexHeightDictionary.Keys.First();
        var binData1 = GetBinData(index1);
        var area1 = 0d;

        affineFunction.InsertBreakpoint(
            area1 * areaInv,
            0,
            binData1.MinValue,
            binData1.MinValue
        );

        foreach (var binData2 in StoredBinData)
        {
            var index2 = binData2.Index;
            var area2 = area1 + binData2.Area;

            if (index2 > index1 + 1)
            {
                affineFunction.InsertBreakpoint(
                    area1 * areaInv,
                    binData1.MaxValue,
                    binData2.MinValue,
                    binData2.MinValue
                );
            }
            else if (index2 == index1)
            {
                affineFunction.InsertBreakpoint(
                    area1 * areaInv,
                    0,
                    binData2.MinValue,
                    binData2.MinValue
                );
            }
            else
            {
                affineFunction.InsertBreakpoint(
                    area1 * areaInv,
                    binData2.MinValue
                );
            }

            index1 = index2;
            binData1 = binData2;
            area1 = area2;
        }

        affineFunction.InsertBreakpoint(
            1,
            binData1.MaxValue,
            binData1.MaxValue,
            0
        );

        return affineFunction;
    }


    public string GetPdfMatlabCode()
    {
        return GetProbabilityDensityFunction().GetMatlabCode(
            DomainMinValue,
            DomainMaxValue,
            true
        );
    }

    public string GetCdfMatlabCode()
    {
        return GetCumulativeDensityFunction().GetMatlabCode(
            DomainMinValue,
            DomainMaxValue,
            true
        );
    }

    public string GetIdfMatlabCode()
    {
        return GetInverseDistributionFunction().GetMatlabCode(
            -0.5,
            1.5,
            true
        );
    }

    public override string ToString()
    {
        var composer = new LinearTextComposer();

        var pairs = StoredBinValueHeightPairs.Select(p =>
            $"P({p.Item1:G}) = {p.Item2:G}"
        ).Concatenate("," + Environment.NewLine);

        composer
            .AppendLine("Regular Sparse Histogram [")
            .IncreaseIndentation()
            .AppendLine(pairs)
            .DecreaseIndentation()
            .AppendLine("]");

        return composer.ToString();
    }

    public IEnumerator<double> GetEnumerator()
    {
        return BinCount
            .MapRange(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

