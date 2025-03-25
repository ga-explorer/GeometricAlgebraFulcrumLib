using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector2D :
    GrVisualAnimatedGeometry
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D Create(Float64Path2D baseCurve, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualAnimatedVector2D(
            samplingSpecs,
            baseCurve
        );
    }
    

    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange,
            time => -p1.GetValue(time),
            time => -p1.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator +(GrVisualAnimatedVector2D p1, ILinFloat64Vector2D p2)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange,
            time => p1.GetValue(time) + p2,
            p1.GetDerivative1Value
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator +(ILinFloat64Vector2D p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p2.TimeRange,
            time => p1 + p2.GetValue(time),
            p2.GetDerivative1Value
        );

        return new GrVisualAnimatedVector2D(
            p2.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector2D operator +(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange,
            time => p1.GetValue(time) + p2.GetValue(time),
            time => p1.GetDerivative1Value(time) + p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1, ILinFloat64Vector2D p2)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange, 
            time => p1.GetValue(time) - p2,
            p1.GetDerivative1Value
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator -(ILinFloat64Vector2D p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p2.TimeRange, 
            time => p1 - p2.GetValue(time),
            time => -p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedVector2D(
            p2.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange,
            time => p1.GetValue(time) - p2.GetValue(time),
            time => p1.GetDerivative1Value(time) - p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector2D operator *(double p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p2.TimeRange, 
            time => p1 * p2.GetValue(time),
            time => p1 * p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedVector2D(
            p2.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector2D p1, double p2)
    {
        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange, 
            time => p1.GetValue(time) * p2,
            time => p1.GetDerivative1Value(time) * p2
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange, 
            time => p1.GetValue(time) * p2.GetValue(time),
            time => p1.GetDerivative1Value(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetValue(time)
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector2D p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange,
            time => p1.GetValue(time) * p2.GetValue(time),
            time => p1.GetDerivative1Value(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetValue(time)
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector2D operator /(GrVisualAnimatedVector2D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = Float64ComputedPath2D.Finite(
            p1.TimeRange, 
            time => p1.GetValue(time) * p2,
            time => p1.GetDerivative1Value(time) * p2
        );

        return new GrVisualAnimatedVector2D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    
    public Float64Path2D BasePath { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVector2D(Float64SamplingSpecs samplingSpecs, Float64Path2D baseCurve)
        : base(samplingSpecs)
    {
        BasePath = baseCurve.MapTimeRangeTo(
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
               BasePath.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetValue(double time)
    {
        return BasePath.GetValue(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Value(double time)
    {
        return BasePath.GetDerivative1Value(time);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative2Value(double time)
    {
        return BasePath.GetDerivative2Value(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path2DLocalFrame GetFrame(double time)
    {
        return BasePath.GetFrame(time);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector2D>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, LinFloat64Vector2D>(
                    frameIndex,
                    BasePath.GetValue(time)
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<LinFloat64Vector2D, double> positionToValueMap)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;

                return new KeyValuePair<int, double>(
                    frameIndex,
                    positionToValueMap(BasePath.GetValue(time))
                );
            }
        );
    }

}