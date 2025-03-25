using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedScalar :
    GrVisualAnimatedGeometry
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar Create(Float64ScalarSignal baseScalar, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualAnimatedScalar(baseScalar, samplingSpecs);
    }
    

    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1)
    {
        return new GrVisualAnimatedScalar(
            p1.BaseScalar.NegativeValue(), 
            p1.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator +(double p1, GrVisualAnimatedScalar p2)
    {
        return new GrVisualAnimatedScalar(
            p1 + p2.BaseScalar, 
            p2.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator +(GrVisualAnimatedScalar p1, double p2)
    {
        return new GrVisualAnimatedScalar(
            p1.BaseScalar + p2, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator +(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        return new GrVisualAnimatedScalar(
            p1.BaseScalar + p2.BaseScalar, 
            p1.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator -(double p1, GrVisualAnimatedScalar p2)
    {
        return new GrVisualAnimatedScalar(
            p1 - p2.BaseScalar, 
            p2.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1, double p2)
    {
        return new GrVisualAnimatedScalar(
            p1.BaseScalar - p2, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        return new GrVisualAnimatedScalar(
            p1.BaseScalar - p2.BaseScalar, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator *(double p1, GrVisualAnimatedScalar p2)
    {
        return new GrVisualAnimatedScalar(
            p1 * p2.BaseScalar, 
            p2.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator *(GrVisualAnimatedScalar p1, double p2)
    {
        return new GrVisualAnimatedScalar(
            p1.BaseScalar * p2, 
            p1.SamplingSpecs
        );
    }
    
    public static GrVisualAnimatedScalar operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        return new GrVisualAnimatedScalar(
            p1.BaseScalar * p2.BaseScalar, 
            p1.SamplingSpecs
        );
    }

    public static GrVisualAnimatedScalar operator /(GrVisualAnimatedScalar p1, double p2)
    {
        p2 = 1d / p2;

        return new GrVisualAnimatedScalar(
            p1.BaseScalar * p2, 
            p1.SamplingSpecs
        );
    }
    

    public Float64ScalarSignal BaseScalar { get; }
    

    private GrVisualAnimatedScalar(Float64ScalarSignal baseScalar, Float64SamplingSpecs samplingSpecs)
        : base(samplingSpecs)
    {
        BaseScalar = baseScalar.MapTimeRangeTo(
            samplingSpecs.MinTime, 
            samplingSpecs.MaxTime
        );
        
        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return TimeRange.IsValid() &&
               TimeRange is { IsFinite: true, MinValue: >= 0 } &&
               BaseScalar.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double time)
    {
        return BaseScalar.GetValue(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivative1Value(double time)
    {
        return BaseScalar.GetDerivative1Value(time);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivative2Value(double time)
    {
        return BaseScalar.GetDerivative2Value(time);
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
                    BaseScalar.GetValue(time)
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
                    positionToValueMap(BaseScalar.GetValue(time))
                );
            }
        );
    }

}