using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedScalar :
    GrVisualAnimatedGeometry
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar Create(TemporalFloat64Scalar temporalScalar, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualAnimatedScalar(temporalScalar, samplingSpecs);
    }
    

    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1)
    {
        return new GrVisualAnimatedScalar(
            p1.TemporalScalar.NegativeValue(), 
            p1.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator +(double p1, GrVisualAnimatedScalar p2)
    {
        return new GrVisualAnimatedScalar(
            p1 + p2.TemporalScalar, 
            p2.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator +(GrVisualAnimatedScalar p1, double p2)
    {
        return new GrVisualAnimatedScalar(
            p1.TemporalScalar + p2, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator +(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        return new GrVisualAnimatedScalar(
            p1.TemporalScalar + p2.TemporalScalar, 
            p1.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator -(double p1, GrVisualAnimatedScalar p2)
    {
        return new GrVisualAnimatedScalar(
            p1 - p2.TemporalScalar, 
            p2.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1, double p2)
    {
        return new GrVisualAnimatedScalar(
            p1.TemporalScalar - p2, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        return new GrVisualAnimatedScalar(
            p1.TemporalScalar - p2.TemporalScalar, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator *(double p1, GrVisualAnimatedScalar p2)
    {
        return new GrVisualAnimatedScalar(
            p1 * p2.TemporalScalar, 
            p2.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator *(GrVisualAnimatedScalar p1, double p2)
    {
        return new GrVisualAnimatedScalar(
            p1.TemporalScalar * p2, 
            p1.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        return new GrVisualAnimatedScalar(
            p1.TemporalScalar * p2.TemporalScalar, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator /(GrVisualAnimatedScalar p1, double p2)
    {
        p2 = 1d / p2;

        return new GrVisualAnimatedScalar(
            p1.TemporalScalar * p2, 
            p1.SamplingSpecs
        );
    }
    

    public TemporalFloat64Scalar TemporalScalar { get; }
    

    private GrVisualAnimatedScalar(TemporalFloat64Scalar temporalScalar, Float64SamplingSpecs samplingSpecs)
        : base(samplingSpecs)
    {
        TemporalScalar = temporalScalar.MapTimeRangeTo(samplingSpecs.MinTime, samplingSpecs.MaxTime);
        
        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return TimeRange.IsValid() &&
               TimeRange.IsFinite &&
               TimeRange.MinValue >= 0 &&
               TemporalScalar.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetValue(double time)
    {
        return TemporalScalar.GetValue(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double time)
    {
        return TemporalScalar.GetDerivativeValue(time);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, double>(
                    frameIndex,
                    TemporalScalar.GetValue(time)
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<double, double> positionToValueMap)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;

                return new KeyValuePair<int, double>(
                    frameIndex,
                    positionToValueMap(TemporalScalar.GetValue(time))
                );
            }
        );
    }

}