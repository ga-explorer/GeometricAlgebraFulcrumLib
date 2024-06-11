using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public sealed class Float64SignalLog2Histogram
{
    private static int DataToPin(double dataValue)
    {
        if (double.IsNaN(dataValue) || double.IsInfinity(dataValue))
            return 0;

        return dataValue == 0
            ? 0
            : dataValue.Abs().Log2().RoundToInt32();
    }

    public static Float64SignalLog2Histogram Create(IEnumerable<double> dataValueList)
    {
        var histSum = 0d;
        var histogramData1 = new SortedDictionary<int, double>();

        foreach (var dataValue in dataValueList)
        {
            var pinValue = DataToPin(dataValue);

            if (histogramData1.TryGetValue(pinValue, out var hist))
                histogramData1[pinValue] = hist + 1;
            else
                histogramData1.Add(pinValue, 1);

            histSum++;
        }

        var histSumInv = 1d / histSum;
        var histogramData =
            histogramData1.ToImmutableSortedDictionary(
                p => p.Key,
                p => p.Value * histSumInv
            );

        return new Float64SignalLog2Histogram(histogramData);
    }


    private readonly ImmutableSortedDictionary<int, double> _histogramData;


    public int SparsePinCount
        => _histogramData.Count;

    public int DensePinCount
        => MaxPinValue - MinPinValue + 1;

    public int MinPinValue { get; }

    public int MaxPinValue { get; }


    public double this[int pinValue]
        => _histogramData.TryGetValue(pinValue, out var hist) ? hist : 0d;


    private Float64SignalLog2Histogram(ImmutableSortedDictionary<int, double> histogramData)
    {
        _histogramData = histogramData;

        var minPinValue = 1000;
        var maxPinValue = -1000;

        foreach (var pinValue in histogramData.Keys)
        {
            if (minPinValue > pinValue) minPinValue = pinValue;
            if (maxPinValue < pinValue) maxPinValue = pinValue;
        }

        MinPinValue = minPinValue;
        MaxPinValue = maxPinValue;
    }


    public Float64SignalLog2Histogram Trim(double trimPercentage)
    {
        if (trimPercentage < 0d)
            throw new ArgumentException(nameof(trimPercentage));

        var pinHistList =
            _histogramData
                .OrderBy(p => p.Value)
                .ThenBy(p => p.Key);

        var histogramData1 = new SortedDictionary<int, double>();

        var histSum = 0d;
        foreach (var (pin, hist) in pinHistList)
        {
            if (trimPercentage <= 0)
            {
                histogramData1.Add(pin, hist);
                histSum += hist;

                continue;
            }

            trimPercentage -= hist;
        }

        var histSumInv = 1d / histSum;
        var histogramData =
            histogramData1.ToImmutableSortedDictionary(
                p => p.Key,
                p => p.Value * histSumInv
            );

        return new Float64SignalLog2Histogram(histogramData);
    }

    public double[] ToArray(int pinValue1, int pinValue2)
    {
        return (pinValue2 - pinValue1 + 1)
            .GetRange(pinValue1)
            .Select(pinValue => this[pinValue])
            .ToArray();
    }

    private double InterpolateNearestDataValue(double dataValue)
    {
        dataValue = dataValue.NaNInfinityToZero();

        var dataPinValue = DataToPin(dataValue);

        if (dataPinValue < MinPinValue)
            return Math.Sign(dataValue) * Math.Pow(2, MinPinValue);

        if (dataPinValue > MaxPinValue)
            return Math.Sign(dataValue) * Math.Pow(2, MaxPinValue);

        if (_histogramData.ContainsKey(dataPinValue))
            return dataValue;

        var pinValue1 = MinPinValue;
        var pinValue2 = MaxPinValue;
        var hist1 = 0.5d;
        var hist2 = 0.5d;

        var pinValue1Found = false;
        foreach (var (pinValue, hist) in _histogramData)
        {
            if (pinValue1Found)
            {
                pinValue2 = pinValue;
                hist2 = hist;
                break;
            }

            if (dataPinValue <= pinValue) continue;

            pinValue1 = pinValue;
            hist1 = hist;
            pinValue1Found = true;
        }

        var pinDataValue1 = Math.Sign(dataValue) * Math.Pow(2, pinValue1);
        var pinDataValue2 = Math.Sign(dataValue) * Math.Pow(2, pinValue2);

        return (hist1 * pinDataValue1 + hist2 * pinDataValue2) / (hist1 + hist2);
    }

    public Float64Signal FilterSignal(Float64Signal signal)
    {
        return signal.MapSamples(InterpolateNearestDataValue);
    }
}