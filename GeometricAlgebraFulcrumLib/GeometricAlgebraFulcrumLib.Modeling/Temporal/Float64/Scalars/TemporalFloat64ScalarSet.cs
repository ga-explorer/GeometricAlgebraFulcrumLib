using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

public sealed class TemporalFloat64ScalarSet :
    IReadOnlyDictionary<string, TemporalFloat64Scalar>
{
    private readonly TemporalFloat64Scalar _zeroScalar;

    private readonly Dictionary<string, TemporalFloat64Scalar> _nameScalarDictionary
        = new Dictionary<string, TemporalFloat64Scalar>();


    public Float64SamplingSpecs SamplingSpecs { get; }

    public double SceneTimeLength
        => SamplingSpecs.TimeLength;

    public int FrameCount
        => SamplingSpecs.SampleCount;

    public bool IsPeriodic
        => false;

    public double FrameTime
        => SamplingSpecs.TimeResolution;

    public double FrameRate
        => SamplingSpecs.SamplingRate;

    public int Count
        => _nameScalarDictionary.Count;

    public IEnumerable<string> Keys
        => _nameScalarDictionary.Keys;

    public IEnumerable<TemporalFloat64Scalar> Values
        => _nameScalarDictionary.Values;

    public TemporalFloat64Scalar this[string key]
    {
        get => GetScalar(key);
        set => SetScalar(key, value);
    }

    public double this[string key, int frameIndex]
        => GetScalarAtFrame(key, frameIndex);


    internal TemporalFloat64ScalarSet(Float64SamplingSpecs samplingSpecs)
    {
        SamplingSpecs = samplingSpecs;

        _zeroScalar = TemporalFloat64Scalar.Zero(
            samplingSpecs.MinTime,
            samplingSpecs.MaxTime
        );
    }

    internal TemporalFloat64ScalarSet(double sceneTime, int frameCount)
    {
        if (!sceneTime.IsFinite() || sceneTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(sceneTime));

        if (frameCount < 2)
            throw new ArgumentOutOfRangeException(nameof(frameCount));

        SamplingSpecs = Float64SamplingSpecs.CreateFromTimeLength(frameCount, sceneTime);

        _zeroScalar = TemporalFloat64Scalar.Zero(0, sceneTime);
    }


    public TemporalFloat64ScalarSet Clear()
    {
        _nameScalarDictionary.Clear();

        return this;
    }

    public TemporalFloat64ScalarSet Remove(string key)
    {
        _nameScalarDictionary.Remove(key);

        return this;
    }
    
    public bool ContainsKey(string key)
    {
        return _nameScalarDictionary.ContainsKey(key);
    }
    
    public TemporalFloat64ScalarSet SetScalar(string key, TemporalFloat64Scalar value)
    {
        var scalar = value.MapTimeRangeTo(0, SceneTimeLength);

        if (_nameScalarDictionary.ContainsKey(key))
            _nameScalarDictionary[key] = scalar;
        else
            _nameScalarDictionary.Add(key, scalar);

        return this;
    }


    public bool TryGetValue(string key, out TemporalFloat64Scalar value)
    {
        return _nameScalarDictionary.TryGetValue(key, out value);
    }
    
    public Tuple<bool, TemporalFloat64Scalar> TryGetScalar(string key)
    {
        return _nameScalarDictionary.TryGetValue(key, out var scalar)
            ? new Tuple<bool, TemporalFloat64Scalar>(true, scalar)
            : new Tuple<bool, TemporalFloat64Scalar>(false, TemporalFloat64Scalar.Zero());
    }

    public TemporalFloat64Scalar GetScalar(string key)
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
    

    public Float64Signal GetTimeSignal()
    {
        return SamplingSpecs.GetSampledTimeSignal();
    }

    public double IndexToTime(int frameIndex)
    {
        return SamplingSpecs.GetSampledTimeValue(frameIndex);
    }

    public int RoundTimeToIndex(double time)
    {
        return (int)SamplingSpecs.GetSampleIndex(time).Round().Clamp(0, FrameCount);
    }

    public int CeilingTimeToIndex(double time)
    {
        return (int)SamplingSpecs.GetSampleIndex(time).Ceiling().Clamp(0, FrameCount);
    }

    public int FloorTimeToIndex(double time)
    {
        return (int)SamplingSpecs.GetSampleIndex(time).Floor().Clamp(0, FrameCount);
    }

    public int PeriodicTimeToIndex(double time)
    {
        return SamplingSpecs.GetSampleIndex(time).RoundToInt32().Mod(FrameCount);
    }
    

    public IEnumerator<KeyValuePair<string, TemporalFloat64Scalar>> GetEnumerator()
    {
        return _nameScalarDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}