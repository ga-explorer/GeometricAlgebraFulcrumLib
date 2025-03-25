using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

public sealed class Float64MedianFilter
{
    public static IEnumerable<double> Filter(IEnumerable<double> signal, int sampleCount)
    {
        var filter = new Float64MedianFilter(sampleCount);

        return filter.Filter(signal.ToImmutableArray());
    }
    
    public static IEnumerable<double> Filter(IReadOnlyList<double> signal, int sampleCount)
    {
        var filter = new Float64MedianFilter(sampleCount);

        return filter.Filter(signal);
    }


    public int SampleCount { get; }


    public Float64MedianFilter(int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        SampleCount = sampleCount;
    }


    private double GetMedianValue(double[] valueList)
    {
        var n = valueList.Length;

        Array.Sort(valueList);

        return n.IsOdd() 
            ? valueList[(n - 1) / 2] 
            : (valueList[n / 2] + valueList[n / 2 - 1]) / 2;
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