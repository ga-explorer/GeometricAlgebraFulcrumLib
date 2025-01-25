using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

public abstract class DifferentialSignalInterpolatorFunction :
    DifferentialInterpolatorFunction
{
    public int SampleIndex1 { get; }

    public int SampleIndex2 { get; }

    public Float64SamplingSpecs SamplingSpecs { get; }

    public double MinVarValue
        => SamplingSpecs.SamplingRate * SampleIndex1;

    public double MaxVarValue
        => SamplingSpecs.SamplingRate * SampleIndex2;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DifferentialSignalInterpolatorFunction(Float64SamplingSpecs samplingSpecs, int sampleIndex1, int sampleIndex2)
    {
        SamplingSpecs = samplingSpecs;
        SampleIndex1 = sampleIndex1;
        SampleIndex2 = sampleIndex2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsSampleIndex(int sampleIndex)
    {
        return sampleIndex >= SampleIndex1 && sampleIndex <= SampleIndex2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsVarValue(double t)
    {
        return t >= MinVarValue && t <= MaxVarValue;
    }
}