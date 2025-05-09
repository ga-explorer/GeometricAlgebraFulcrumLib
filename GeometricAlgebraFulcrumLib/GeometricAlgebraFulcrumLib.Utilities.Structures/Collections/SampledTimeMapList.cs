using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public sealed record SampledTimeMapList<T> :
    IReadOnlyList<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledTimeMapList<T> Create(double timeMin, double timeMax, bool isPeriodic, Func<double, T> timeMap, int sampleCount)
    {
        return new SampledTimeMapList<T>(
            new Pair<double>(timeMin, timeMax), 
            isPeriodic, 
            timeMap, 
            sampleCount
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledTimeMapList<T> Finite(IPair<double> timeRange, Func<double, T> timeMap, int sampleCount)
    {
        return new SampledTimeMapList<T>(timeRange, false, timeMap, sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledTimeMapList<T> Finite(double timeMin, double timeMax, Func<double, T> timeMap, int sampleCount)
    {
        return new SampledTimeMapList<T>(
            new Pair<double>(timeMin, timeMax), 
            false, 
            timeMap, 
            sampleCount
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledTimeMapList<T> Periodic(IPair<double> timeRange, Func<double, T> timeMap, int sampleCount)
    {
        return new SampledTimeMapList<T>(timeRange, true, timeMap, sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SampledTimeMapList<T> Periodic(double timeMin, double timeMax, Func<double, T> timeMap, int sampleCount)
    {
        return new SampledTimeMapList<T>(
            new Pair<double>(timeMin, timeMax), 
            true, 
            timeMap, 
            sampleCount
        );
    }


    public bool IsPeriodic { get; }

    public bool IsFinite 
        => !IsPeriodic;

    public double TimeMin { get; }

    public double TimeMax 
        => TimeMin + TimeResolution * (Count - 1);

    public Func<double, T> TimeMap { get; }

    public double TimeLength 
        => TimeResolution * (Count - 1);

    public int Count { get; }

    public double TimeResolution { get; }

    public T this[int index]
    {
        get
        {
            if (IsFinite && (index < 0 || index >= Count))
                throw new IndexOutOfRangeException();

            return TimeMap(TimeMin + index.Mod(Count) * TimeResolution);
        }
    }


    private SampledTimeMapList(IPair<double> timeRange, bool isPeriodic, Func<double, T> timeMap, int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        if (double.IsNaN(timeRange.Item1) || double.IsInfinity(timeRange.Item1))
            throw new ArgumentOutOfRangeException(nameof(timeRange));
        
        if (double.IsNaN(timeRange.Item2) || double.IsInfinity(timeRange.Item2))
            throw new ArgumentOutOfRangeException(nameof(timeRange));

        IsPeriodic = isPeriodic;
        Count = sampleCount;
        TimeMap = timeMap;
        TimeMin = timeRange.Min();
        TimeResolution = isPeriodic 
            ? TimeLength / sampleCount 
            : TimeLength / (sampleCount - 1d);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => TimeMap(TimeMin + i * TimeResolution))
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}