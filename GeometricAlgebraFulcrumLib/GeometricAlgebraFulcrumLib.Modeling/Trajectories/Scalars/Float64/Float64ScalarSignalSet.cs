using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

public sealed class Float64ScalarSignalSet :
    IReadOnlyDictionary<string, Float64ScalarSignal>
{
    private readonly Float64ScalarSignal _zeroScalar;

    private readonly Dictionary<string, Float64ScalarSignal> _nameScalarDictionary
        = new Dictionary<string, Float64ScalarSignal>();


    public Float64SamplingSpecs SamplingSpecs { get; }

    public double SceneTimeLength
        => SamplingSpecs.TimeLength;

    public int SampleCount
        => SamplingSpecs.SampleCount;

    public bool IsPeriodic
        => false;

    public double TimeResolution
        => SamplingSpecs.TimeResolution;

    public double SamplingRate
        => SamplingSpecs.SamplingRate;

    public int Count
        => _nameScalarDictionary.Count;

    public IEnumerable<string> Keys
        => _nameScalarDictionary.Keys;

    public IEnumerable<Float64ScalarSignal> Values
        => _nameScalarDictionary.Values;

    public Float64ScalarSignal this[string key]
    {
        get => GetScalar(key);
        set => SetScalar(key, value);
    }

    public double this[string key, int frameIndex]
        => GetScalarAtFrame(key, frameIndex);


    internal Float64ScalarSignalSet(Float64SamplingSpecs samplingSpecs)
    {
        SamplingSpecs = samplingSpecs;

        _zeroScalar = Float64ScalarSignal.FiniteZero(
            samplingSpecs.MinTime,
            samplingSpecs.MaxTime
        );
    }

    internal Float64ScalarSignalSet(double sceneTime, int frameCount)
    {
        if (!sceneTime.IsFinite() || sceneTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(sceneTime));

        if (frameCount < 2)
            throw new ArgumentOutOfRangeException(nameof(frameCount));

        SamplingSpecs = Float64SamplingSpecs.CreateFromTimeLength(frameCount, sceneTime);

        _zeroScalar = Float64ScalarSignal.FiniteZero(0, sceneTime);
    }


    public Float64ScalarSignalSet Clear()
    {
        _nameScalarDictionary.Clear();

        return this;
    }

    public Float64ScalarSignalSet Remove(string key)
    {
        _nameScalarDictionary.Remove(key);

        return this;
    }
    
    public bool ContainsKey(string key)
    {
        return _nameScalarDictionary.ContainsKey(key);
    }
    
    public Float64ScalarSignalSet SetScalar(string key, Float64ScalarSignal value)
    {
        var scalar = value.MapTimeRangeTo(0, SceneTimeLength);

        if (_nameScalarDictionary.ContainsKey(key))
            _nameScalarDictionary[key] = scalar;
        else
            _nameScalarDictionary.Add(key, scalar);

        return this;
    }


    public bool TryGetValue(string key, out Float64ScalarSignal value)
    {
        return _nameScalarDictionary.TryGetValue(key, out value);
    }
    
    public Tuple<bool, Float64ScalarSignal> TryGetScalar(string key)
    {
        return _nameScalarDictionary.TryGetValue(key, out var scalar)
            ? new Tuple<bool, Float64ScalarSignal>(true, scalar)
            : new Tuple<bool, Float64ScalarSignal>(false, Float64ScalarSignal.FiniteZero());
    }

    public Float64ScalarSignal GetScalar(string key)
    {
        return _nameScalarDictionary.GetValueOrDefault(key, _zeroScalar);
    }
    

    public double GetScalarAtFrame(string key, int frameIndex)
    {
        return _nameScalarDictionary.TryGetValue(key, out var value)
            ? value.GetValue(IndexToTime(frameIndex)) : 0d;
    }

    public double GetScalarAtTime(string key, double time)
    {
        return _nameScalarDictionary.TryGetValue(key, out var value)
            ? value.GetValue(time) : 0d;
    }
    

    public Float64SampledTimeSignal GetTimeSignal()
    {
        return SamplingSpecs.GetSampleTimeSignal();
    }

    public double IndexToTime(int frameIndex)
    {
        return SamplingSpecs.GetSampleTime(frameIndex);
    }

    public int RoundTimeToIndex(double time)
    {
        return (int)SamplingSpecs.GetSampleIndex(time).Round().Clamp(0, SampleCount);
    }

    public int CeilingTimeToIndex(double time)
    {
        return (int)SamplingSpecs.GetSampleIndex(time).Ceiling().Clamp(0, SampleCount);
    }

    public int FloorTimeToIndex(double time)
    {
        return (int)SamplingSpecs.GetSampleIndex(time).Floor().Clamp(0, SampleCount);
    }

    public int PeriodicTimeToIndex(double time)
    {
        return SamplingSpecs.GetSampleIndex(time).RoundToInt32().Mod(SampleCount);
    }
    

    public IEnumerator<KeyValuePair<string, Float64ScalarSignal>> GetEnumerator()
    {
        return _nameScalarDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}