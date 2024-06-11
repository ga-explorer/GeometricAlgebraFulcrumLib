using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Statistics.Continuous;

public sealed class QuantizedHistogram :
    IReadOnlyList<ulong>
{
    public static QuantizedHistogram CreateEmpty(double domainFirstValue, double domainLastValue, int binCount)
    {
        return new QuantizedHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        );
    }

    //public static QuantizedHistogram CreateFromHistogram(SparseIrregularHistogram irregularHistogram, int binCount)
    //{
    //    var domainFirstValue = irregularHistogram.DomainMinValue;
    //    var domainLastValue = irregularHistogram.DomainMaxValue;

    //    var hist = new QuantizedHistogram(domainFirstValue, domainLastValue, binCount);

    //    var xDeltaInv = binCount / (domainLastValue - domainFirstValue);
    //    var x0 = domainFirstValue;
            
    //    for (var i = 1; i <= binCount; i++)
    //    {
    //        var t = i / (double)(binCount);

    //        var x1 = (1d - t) * domainFirstValue + t * domainLastValue;

    //        var height = irregularHistogram.GetAreaBetween(x0, x1) * xDeltaInv;

    //        hist.SetBinHeight(i - 1, height);

    //        x0 = x1;
    //    }

    //    return hist;
    //}

    public static QuantizedHistogram CreateFromHistogram(double domainFirstValue, double domainLastValue, int binCount, IReadOnlyDictionary<double, double> valueHeightDictionary)
    {
        Debug.Assert(
            binCount > 0 &&
            valueHeightDictionary.Count > 0 &&
            valueHeightDictionary.All(p => 
                (
                    (p.Key >= domainFirstValue && p.Key <= domainLastValue) ||
                    (p.Key >= domainLastValue && p.Key <= domainFirstValue)
                ) && p.Value > 0
            )
        );

        var hist = new QuantizedHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        );

        foreach (var (domainValue, height) in valueHeightDictionary)
            hist.AddHeight(domainValue, (ulong) height); // This needs correction
        
        return hist;
    }

    public static QuantizedHistogram CreateFromRandomSamples(double domainFirstValue, double domainLastValue, int binCount, Random randomGenerator, int randomSampleCount = 10000000)
    {
        var hist = new QuantizedHistogram(
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
            
        return hist;
    }

    public static QuantizedHistogram CreateUniform(double domainFirstValue, double domainLastValue, int binCount = 1, ulong binHeight = 1ul)
    {
        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        for (var i = 0; i < binCount; i++)
            indexHeightDictionary.Add(i, binHeight);

        return new QuantizedHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        ).SetDictionary(indexHeightDictionary);
    }
    
    public static QuantizedHistogram CreateNormal(double mean, double standardDeviation, int binCount, int quantizationBits = 32)
    {
        if (quantizationBits is < 1 or > 64)
            throw new ArgumentOutOfRangeException(nameof(quantizationBits));

        var quantizationLevels = 
            1ul << quantizationBits;

        var zeroEpsilon = 
            Phi(mean) / quantizationLevels;

        var variance =
            standardDeviation * standardDeviation;

        var halfSize = Math.Sqrt(
            -2 * variance * Math.Log(
                Math.Sqrt(2 * Math.PI) * standardDeviation * zeroEpsilon
            )
        );

        var domainFirstValue = mean - halfSize;
        var domainLastValue = mean + halfSize;

        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        for (var i = 0; i < binCount; i++)
        {
            var t = i / (double)(binCount - 1);
            var x = (1 - t) * domainFirstValue + t * domainLastValue;
            var p = Phi(x);
            var height = (ulong)Math.Round(p * quantizationLevels);

            indexHeightDictionary.Add(i, height);
        }

        return new QuantizedHistogram(
            domainFirstValue,
            domainLastValue,
            binCount
        ).SetDictionary(indexHeightDictionary);

        // Standard normal distribution function
        // https://en.wikipedia.org/wiki/Normal_distribution
        double Phi(double x)
        {
            var z = (x - mean) / standardDeviation;

            return Math.Exp(-z * z) / (Math.Sqrt(2d * Math.PI) * standardDeviation);
        }
    }

    public static QuantizedHistogram CreateExponential(double rate, int binCount, int quantizationBits = 32)
    {
        if (quantizationBits is < 1 or > 64)
            throw new ArgumentOutOfRangeException(nameof(quantizationBits));

        var quantizationLevels = 
            1ul << quantizationBits;

        var zeroEpsilon = 
            rate / quantizationLevels;

        var domainLastValue = 
            -Math.Log(zeroEpsilon / rate) / rate;

        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        for (var i = 0; i < binCount; i++)
        {
            var x = domainLastValue * i / (binCount - 1);
            var p = rate * Math.Exp(-rate * x);
            var height = (ulong)Math.Round(p * quantizationLevels);

            indexHeightDictionary.Add(i, height);
        }

        return new QuantizedHistogram(
            0d,
            domainLastValue,
            binCount
        ).SetDictionary(indexHeightDictionary);
    }


    public static QuantizedHistogram operator -(QuantizedHistogram pmf)
    {
        return pmf.Negative();
    }

    public static QuantizedHistogram operator +(QuantizedHistogram pmf, double value)
    {
        return pmf.Add(value);
    }

    public static QuantizedHistogram operator +(double value, QuantizedHistogram pmf)
    {
        return pmf.Add(value);
    }

    public static QuantizedHistogram operator +(QuantizedHistogram pmf1, QuantizedHistogram pmf2)
    {
        return pmf1.Add(pmf2);
    }

    public static QuantizedHistogram operator -(QuantizedHistogram pmf, double value)
    {
        return pmf.Add(-value);
    }

    public static QuantizedHistogram operator -(double value, QuantizedHistogram pmf)
    {
        return pmf.Negative().Add(value);
    }

    public static QuantizedHistogram operator -(QuantizedHistogram pmf1, QuantizedHistogram pmf2)
    {
        return pmf1.Subtract(pmf2);
    }

    public static QuantizedHistogram operator *(QuantizedHistogram pmf, double value)
    {
        return pmf.Times(value);
    }

    public static QuantizedHistogram operator *(double value, QuantizedHistogram pmf)
    {
        return pmf.Times(value);
    }

    public static QuantizedHistogram operator *(QuantizedHistogram pmf1, QuantizedHistogram pmf2)
    {
        return pmf1.Times(pmf2);
    }

    public static QuantizedHistogram operator /(QuantizedHistogram pmf, double value)
    {
        return pmf.Times(1d / value);
    }

    public static QuantizedHistogram operator /(double value, QuantizedHistogram pmf)
    {
        return pmf.Inverse().Times(value);
    }

    public static QuantizedHistogram operator /(QuantizedHistogram pmf1, QuantizedHistogram pmf2)
    {
        return pmf1.Divide(pmf2);
    }


    /// <summary>
    /// A dictionary holding sparse information about histogram bins and probabilities
    /// </summary>
    private SortedDictionary<int, ulong> IndexHeightDictionary { get; set; }

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
    /// Sum of histogram bin heights
    /// </summary>
    public ulong HistogramSum { get; private set; }

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

    public IEnumerable<KeyValuePair<double, ulong>> StoredBinValueHeightPairs
        => IndexHeightDictionary.Select(p =>
            new KeyValuePair<double, ulong>(
                GetBinMidValue(p.Key),
                p.Value
            )
        );
        
    public IEnumerable<KeyValuePair<double, double>> StoredBinValueProbabilityPairs
        => IndexHeightDictionary.Select(p =>
            new KeyValuePair<double, double>(
                GetBinMidValue(p.Key),
                p.Value / (double) HistogramSum
            )
        );

    public IEnumerable<QuantizedHistogramBinData> StoredBinData
        => IndexHeightDictionary.Select(p =>
            new QuantizedHistogramBinData(
                p.Key,
                GetBinMidValue(p.Key),
                BinWidth,
                p.Value,
                HistogramSum
            )
        );

    public int Count
        => BinCount;

    public ulong this[int index]
    {
        get => IndexHeightDictionary.GetValueOrDefault(index, 0ul);
        set
        {
            if (index < 0 || index >= BinCount)
                throw new IndexOutOfRangeException();

            if (value == 0ul)
                IndexHeightDictionary.Remove(index);
            else if (IndexHeightDictionary.ContainsKey(index))
                IndexHeightDictionary[index] = value;
            else
                IndexHeightDictionary.Add(index, value);
        }
    }


    private QuantizedHistogram(double domainFirstValue, double domainLastValue, int binCount)
    {
        if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite() || binCount < 1)
            throw new InvalidOperationException();

        if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
            throw new InvalidOperationException();

        DomainFirstValue = domainFirstValue;
        DomainLastValue = domainLastValue;
        BinCount = binCount;

        IndexHeightDictionary = new SortedDictionary<int, ulong>();
        HistogramSum = 0ul;

        Debug.Assert(IsValid());
    }
        
    //private QuantizedHistogram(double domainFirstValue, double domainLastValue, int binCount, SortedDictionary<int, ulong> indexHeightDictionary)
    //{
    //    if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite() || binCount < 1)
    //        throw new InvalidOperationException();

    //    if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
    //        throw new InvalidOperationException();

    //    DomainFirstValue = domainFirstValue;
    //    DomainLastValue = domainLastValue;
    //    BinCount = binCount;

    //    IndexHeightDictionary = indexHeightDictionary;
    //    HistogramSum = indexHeightDictionary.Values.Aggregate(0ul, (a, b) => a + b);
        
    //    Debug.Assert(IsValid());
    //}

    
    private QuantizedHistogram SetDictionary(SortedDictionary<int, ulong> indexHeightDictionary)
    {
        IndexHeightDictionary = indexHeightDictionary;
        
        HistogramSum = indexHeightDictionary.Values.Aggregate(
            0ul, 
            (a, b) => a + b
        );

        Debug.Assert(IsValid());

        return this;
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
               ) &&
               HistogramSum == IndexHeightDictionary.Values.Aggregate(
                   0ul, 
                   (a, b) => a + b
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
    
    public QuantizedHistogramBinData GetBinData(int index)
    {
        var midValue = 
            GetBinMidValue(index);

        var height = 
            IndexHeightDictionary.GetValueOrDefault(index, 0ul);

        return new QuantizedHistogramBinData(
            index, 
            midValue, 
            BinWidth, 
            height,
            HistogramSum
        );
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

        return (int) Math.Round(
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

    public ulong GetBinHeight(int index)
    {
        return IndexHeightDictionary.GetValueOrDefault(index, 0ul);
    }
    
    public double GetBinNormalizedHeight(int index)
    {
        return GetBinHeight(index) / (double)HistogramSum;
    }
    
    public double GetBinArea(int index)
    {
        return BinWidth * GetBinHeight(index);
    }

    public double GetBinNormalizedArea(int index)
    {
        return BinWidth * GetBinHeight(index) / HistogramSum;
    }

    public QuantizedHistogram SetBinHeight(int index, ulong height)
    {
        if (index < 0 || index >= BinCount)
            throw new IndexOutOfRangeException();

        if (IndexHeightDictionary.TryGetValue(index, out var oldHeight))
        {
            HistogramSum -= oldHeight;

            if (height == 0ul)
                IndexHeightDictionary.Remove(index);
            else
            {
                IndexHeightDictionary[index] = height;
                HistogramSum += height;
            }
        }
        else
        {
            IndexHeightDictionary.Add(index, height);
            HistogramSum += height;
        }

        return this;
    }
    
    public QuantizedHistogram AddBinHeight(int index, ulong heightDelta)
    {
        if (heightDelta == 0ul)
            return this;

        return SetBinHeight(
            index, 
            heightDelta + GetBinHeight(index)
        );
    }

    public QuantizedHistogram SetHeight(double domainValue, ulong height)
    {
        var index = GetBinIndexContaining(domainValue);

        return SetBinHeight(index, height);
    }

    public QuantizedHistogram AddHeight(double domainValue, ulong height)
    {
        var index = GetBinIndexContaining(domainValue);

        return AddBinHeight(index, height);
    }

    public QuantizedHistogram AddHeight(double domainValue1, double domainValue2, ulong height)
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


    public QuantizedHistogram MapHeights(Func<ulong, ulong> mappingFunc)
    {
        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        foreach (var (i, h) in IndexHeightDictionary)
        {
            var height = mappingFunc(h);

            if (height > 0)
                indexHeightDictionary.Add(i, height);
        }

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }
        
    public QuantizedHistogram MapHeights(Func<int, ulong, ulong> mappingFunc)
    {
        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        foreach (var (i, h) in IndexHeightDictionary)
        {
            var height = mappingFunc(i, h);

            if (height > 0)
                indexHeightDictionary.Add(i, height);
        }

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }
        
    public QuantizedHistogram MapHeights(Func<QuantizedHistogramBinData, ulong> mappingFunc)
    {
        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        foreach (var (index, oldHeight) in IndexHeightDictionary)
        {
            var midValue = GetBinMidValue(index);

            var binData = new QuantizedHistogramBinData(
                index, 
                midValue, 
                BinWidth, 
                oldHeight,
                HistogramSum
            );

            var height = mappingFunc(binData);

            if (height > 0)
                indexHeightDictionary.Add(index, height);
        }

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }

    public QuantizedHistogram ScaleHeights(ulong scalingFactor)
    {
        return MapHeights(h => h * scalingFactor);
    }
        
    //public QuantizedHistogram NormalizeHeights()
    //{
    //    return ScaleHeights(1d / GetArea());
    //}
        
    public QuantizedHistogram TrimHeights(double zeroHeightEpsilon)
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
        
    public QuantizedHistogram TrimHeightsByArea(double zeroAreaRatioEpsilon)
    {
        var area = IndexHeightDictionary.Values.Aggregate(0ul, (a, b) => a + b);
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

        
    public QuantizedHistogram PrependBins(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return this;

        BinCount += count;
        DomainFirstValue -= BinSignedWidth * count;

        return this;
    }

    public QuantizedHistogram AppendBins(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return this;

        BinCount += count;
        DomainLastValue += BinSignedWidth * count;

        return this;
    }

    public QuantizedHistogram TrimBins()
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

        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        foreach (var (index, height) in IndexHeightDictionary)
            indexHeightDictionary.Add(index - firstIndex, height);

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }
        
    public QuantizedHistogram TrimFirstBins()
    {
        if (IndexHeightDictionary.Count == 0)
            throw new InvalidOperationException();

        var firstIndex = IndexHeightDictionary.Keys.First();

        if (firstIndex == 0)
            return this;

        DomainFirstValue += BinSignedWidth * firstIndex;

        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        foreach (var (index, height) in IndexHeightDictionary)
            indexHeightDictionary.Add(index - firstIndex, height);

        IndexHeightDictionary = indexHeightDictionary;

        return this;
    }
        
    public QuantizedHistogram TrimLastBins()
    {
        if (IndexHeightDictionary.Count == 0)
            throw new InvalidOperationException();

        var lastIndex = IndexHeightDictionary.Keys.Last();

        if (lastIndex == BinCount - 1)
            return this;

        DomainLastValue -= BinSignedWidth * (BinCount - 1 - lastIndex);

        return this;
    }

    
    public ulong GetSum()
    {
        return IndexHeightDictionary.Values.Aggregate(
            0ul, 
            (a, b) => a + b
        );
    }
    
    public ulong GetSumBefore(double domainValue)
    {
        var binIndex = 
            GetBinIndexContaining(domainValue);

        var sum = 
            GetBinData(binIndex).GetHeightBefore(domainValue);

        if (DomainFirstValue <= DomainLastValue)
        {
            sum += IndexHeightDictionary
                .Where(p => p.Key < binIndex)
                .Select(p => p.Value)
                .Aggregate(0ul, (a, b) => a + b);
        }
        else
        {
            sum += IndexHeightDictionary
                .Where(p => p.Key > binIndex)
                .Select(p => p.Value)
                .Aggregate(0ul, (a, b) => a + b);
        }

        return sum;
    }
    
    public ulong GetSumAfter(double domainValue)
    {
        var binIndex = 
            GetBinIndexContaining(domainValue);

        var sum = 
            GetBinData(binIndex).GetHeightAfter(domainValue);

        if (DomainFirstValue <= DomainLastValue)
        {
            sum += IndexHeightDictionary
                .Where(p => p.Key > binIndex)
                .Select(p => p.Value)
                .Aggregate(0ul, (a, b) => a + b);
        }
        else
        {
            sum += IndexHeightDictionary
                .Where(p => p.Key < binIndex)
                .Select(p => p.Value)
                .Aggregate(0ul, (a, b) => a + b);
        }

        return sum;
    }
    
    public ulong GetSumBetween(double domainValue1, double domainValue2)
    {
        var sum1 = GetSumBefore(domainValue1);
        var sum2 = GetSumBefore(domainValue2);

        return sum1 <= sum2 
            ? sum2 - sum1 
            : sum1 - sum2;
    }


    public double GetArea()
    {
        return BinWidth * GetSum();
    }

    public double GetAreaBefore(double domainValue)
    {
        return BinWidth * GetSumBefore(domainValue);
    }
        
    public double GetAreaAfter(double domainValue)
    {
        return BinWidth * GetSumAfter(domainValue);
    }
        
    public double GetAreaBetween(double domainValue1, double domainValue2)
    {
        return BinWidth * GetSumBetween(domainValue1, domainValue2);
    }


    public QuantizedHistogram ResetDomainRange(double domainFirstValue, double domainLastValue)
    {
        if (domainFirstValue.IsNaNOrInfinite() || domainLastValue.IsNaNOrInfinite())
            throw new InvalidOperationException();

        if ((domainFirstValue - domainLastValue).Abs() <= 1e-12)
            throw new InvalidOperationException();

        DomainFirstValue = domainFirstValue;
        DomainLastValue = domainLastValue;

        return this;
    }
        
    public QuantizedHistogram FlipDomain()
    {
        return ResetDomainRange(
            DomainLastValue, 
            DomainFirstValue
        );
    }

    public QuantizedHistogram ShiftDomain(double delta)
    {
        return ResetDomainRange(
            DomainFirstValue + delta, 
            DomainLastValue + delta
        );
    }


    public QuantizedHistogram MapDomainUsingAffine(double scalingFactor, double offset)
    {
        var domainFirstValue = DomainFirstValue * scalingFactor + offset;
        var domainLastValue = DomainLastValue * scalingFactor + offset;

        var indexHeightDictionary = new SortedDictionary<int, ulong>();

        foreach (var (i, h) in IndexHeightDictionary)
            indexHeightDictionary.Add(i, h);

        return new QuantizedHistogram(
            domainFirstValue,
            domainLastValue,
            BinCount
        ).SetDictionary(indexHeightDictionary);
    }

    public QuantizedHistogram MapDomain(Func<double, double> scalarMap, int binCount)
    {
        //var irregularHistogram = SparseIrregularHistogram.Create();
        
        var affineFunction = 
            PiecewiseAffineFunction.CreateContinuous(
                scalarMap, 
                DomainMinValue, 
                DomainMaxValue,
                binCount, 
                0.1 * Math.PI / 180d
            );

        var domainMinValue = affineFunction.MinYValue;
        var domainMaxValue = affineFunction.MaxYValue;

        var hist = new QuantizedHistogram(
            domainMinValue,
            domainMaxValue,
            binCount
        );

        foreach (var segment in affineFunction.FiniteSegments)
        {
            if (segment.IsConstantY)
            {

            }
            else
            {
                var sum = GetSumBetween(segment.X1, segment.X2);


            }

            //irregularHistogram.AddBin(
            //    segment.YMid,
            //    Math.Abs(segment.YDelta),
            //    binData.Height
            //);
        }

        foreach (var binData in StoredBinData)
        {
            //var affineFunction = 
            //    PiecewiseAffineFunction.CreateContinuous(
            //        scalarMap, 
            //        binData.MinValue, 
            //        binData.MaxValue,
            //        binCount, 
            //        0.1 * Math.PI / 180d
            //    );

            

            foreach (var segment in affineFunction.FiniteSegments)
            {
                if (segment.IsConstantY)
                {

                }
                else
                {

                }

                //irregularHistogram.AddBin(
                //    segment.YMid,
                //    Math.Abs(segment.YDelta),
                //    binData.Height
                //);
            }
        }

        return hist;
    }

    public QuantizedHistogram MapDomain(QuantizedHistogram pmf2, Func<double, double, double> scalarMap, int binCount)
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

        var domainFirstValue = sparseDictionary.Keys.Min();
        var domainLastValue = sparseDictionary.Keys.Min();

        return CreateFromHistogram(
            domainFirstValue,
            domainLastValue,
            binCount,
            sparseDictionary
        );
    }

    public QuantizedHistogram JoinDomain(QuantizedHistogram pmf2, int binCount)
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

        var domainFirstValue = sparseDictionary.Keys.Min();
        var domainLastValue = sparseDictionary.Keys.Min();

        return CreateFromHistogram(
            domainFirstValue,
            domainLastValue,
            binCount,
            sparseDictionary
        );
    }
        
    public QuantizedHistogram Negative()
    {
        return Times(-1d);
    }

    public QuantizedHistogram Inverse()
    {
        return MapDomain(
            x => 1 / x,
            BinCount
        );
    }

    public QuantizedHistogram Add(double value)
    {
        return ShiftDomain(value);
    }

    public QuantizedHistogram Subtract(double value)
    {
        return ShiftDomain(-value);
    }

    public QuantizedHistogram Times(double value)
    {
        Debug.Assert(!value.IsNaNOrInfinite());

        return MapDomain(x => x * value, 1024);
    }

    public QuantizedHistogram Divide(double value)
    {
        return Times(1d / value);
    }


    public QuantizedHistogram Add(QuantizedHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x + y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }

    public QuantizedHistogram Subtract(QuantizedHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x - y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }

    public QuantizedHistogram Times(QuantizedHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x * y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }

    public QuantizedHistogram Divide(QuantizedHistogram pmf2)
    {
        return MapDomain(
            pmf2,
            (x, y) => x / y,
            Math.Max(BinCount, pmf2.BinCount)
        );
    }


    public double GetMean()
    {
        return StoredBinValueProbabilityPairs.Select(
            p => p.Key * p.Value
        ).Sum();
    }

    public double GetVariance()
    {
        var mean = GetMean();

        return StoredBinValueProbabilityPairs.Select(
            p => (p.Key - mean).Square() * p.Value
        ).Sum();
    }

    public double GetStandardDeviation()
    {
        return GetVariance().Sqrt();
    }

    public double GetRelativeStandardDeviation()
    {
        var mean = GetMean();

        var standardDeviation = StoredBinValueProbabilityPairs.Select(
            p => (p.Key - mean).Square() * p.Value
        ).Sum().Sqrt();

        return standardDeviation / mean;
    }

    public double GetSkewness()
    {
        return StoredBinValueProbabilityPairs.Select(
            p => Math.Pow(p.Key, 3) * p.Value
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
        return StoredBinValueProbabilityPairs.Select(
            p => Math.Pow(p.Key, n) * p.Value
        ).Sum();
    }

    public double GetExpectedValue(Func<double, double> valueMap)
    {
        return StoredBinValueProbabilityPairs.Select(
            p => valueMap(p.Key) * p.Value
        ).Sum();
    }
        

    public QuantizedHistogramPdf CreatePdf()
    {
        return new QuantizedHistogramPdf(this);
    }

    public QuantizedHistogramPdf CreatePdf(int seed)
    {
        return new QuantizedHistogramPdf(this, seed);
    }


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
            var area2 = area1 + binData2.GetNormalizedArea();

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
            var area2 = area1 + binData2.GetNormalizedArea();

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
            var area2 = area1 + binData2.GetNormalizedArea();

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

        var pairs = StoredBinData.Select(b =>
            b.ToString()
        ).Concatenate("," + Environment.NewLine);

        composer
            .AppendLine("Regular Sparse Quantized Histogram [")
            .IncreaseIndentation()
            .AppendLine(pairs)
            .DecreaseIndentation()
            .AppendLine("]");

        return composer.ToString();
    }

    public IEnumerator<ulong> GetEnumerator()
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