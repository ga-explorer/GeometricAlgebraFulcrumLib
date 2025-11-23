using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Filters;

public sealed class MedianFilter :
    IFloat64SignalFilter
{
    private static double GetMedianValue(double[] valueList)
    {
        var n = valueList.Length;

        Array.Sort(valueList);

        return n.IsOdd() 
            ? valueList[(n - 1) / 2] 
            : (valueList[n / 2] + valueList[n / 2 - 1]) / 2;
    }

    public static MedianFilter Create(int sampleCount)
    {
        return new MedianFilter(sampleCount);
    }

    public static IEnumerable<double> Filter(IEnumerable<double> signal, int sampleCount)
    {
        var filter = new MedianFilter(sampleCount);

        return filter.Filter([..signal]);
    }
    
    public static IEnumerable<double> Filter(IReadOnlyList<double> signal, int sampleCount)
    {
        var filter = new MedianFilter(sampleCount);

        return filter.Filter(signal);
    }
    
    public static double[] Filter(double[] signal, int sampleCount)
    {
        var filter = new MedianFilter(sampleCount);

        return filter.Filter(signal).ToArray();
    }


    public int SampleCount { get; }


    private MedianFilter(int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        SampleCount = sampleCount;
    }



    public IEnumerable<double> Filter(IEnumerable<double> signal)
    {
        return Filter(signal.ToArray());
    }

    public IEnumerable<double> Filter(IReadOnlyList<double> signal)
    {
        var m = signal.Count;
        for (var i = 0; i < m; i++)
        {
            var index1 = Math.Max(0, i - SampleCount);
            var index2 = Math.Min(m - 1, index1 + 2 * SampleCount);
            var n = index2 - index1 + 1;

            var valueList = new double[n];
            for (var j = index1; j <= index2; j++)
                valueList[j - index1] = signal[j];

            yield return GetMedianValue(valueList);
        }
    }

}