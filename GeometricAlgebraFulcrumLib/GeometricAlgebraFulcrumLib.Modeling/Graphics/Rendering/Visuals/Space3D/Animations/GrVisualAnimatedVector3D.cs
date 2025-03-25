using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector3D :
    GrVisualAnimatedGeometry
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D Create(Float64Path3D baseCurve, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualAnimatedVector3D(
            samplingSpecs,
            baseCurve
        );
    }


    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => -p1.GetValue(time),
                time => -p1.GetDerivative1Value(time)
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, ILinFloat64Vector3D p2)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) + p2,
                p1.GetDerivative1Value
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(ILinFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p2.TimeRange,
                time => p1 + p2.GetValue(time),
                p2.GetDerivative1Value
            );

        return new GrVisualAnimatedVector3D(
            p2.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) + p2.GetValue(time),
                time => p1.GetDerivative1Value(time) + p2.GetDerivative1Value(time)
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, ILinFloat64Vector3D p2)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) - p2,
                p1.GetDerivative1Value
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(ILinFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p2.TimeRange,
                time => p1 - p2.GetValue(time),
                time => -p2.GetDerivative1Value(time)
            );

        return new GrVisualAnimatedVector3D(
            p2.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) - p2.GetValue(time),
                time => p1.GetDerivative1Value(time) - p2.GetDerivative1Value(time)
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector3D operator *(double p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p2.TimeRange,
                time => p1 * p2.GetValue(time),
                time => p1 * p2.GetDerivative1Value(time)
            );

        return new GrVisualAnimatedVector3D(
            p2.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, double p2)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) * p2,
                time => p1.GetDerivative1Value(time) * p2
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) * p2.GetValue(time),
                time => p1.GetDerivative1Value(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetValue(time)
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) * p2.GetValue(time),
                time => p1.GetDerivative1Value(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetValue(time)
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }

    public static GrVisualAnimatedVector3D operator /(GrVisualAnimatedVector3D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = 
            Float64ComputedPath3D.Finite(
                p1.TimeRange,
                time => p1.GetValue(time) * p2,
                time => p1.GetDerivative1Value(time) * p2
            );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve
        );
    }
    
    
    public Float64Path3D BasePath { get; }
    
    
    private GrVisualAnimatedVector3D(Float64SamplingSpecs samplingSpecs, Float64Path3D baseCurve)
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
    public LinFloat64Vector3D GetValue(double time)
    {
        return BasePath.GetValue(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Value(double time)
    {
        return BasePath.GetDerivative1Value(time);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative2Value(double time)
    {
        return BasePath.GetDerivative2Value(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3DLocalFrame GetFrame(double time)
    {
        return BasePath.GetFrame(time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector3D>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, LinFloat64Vector3D>(
                    frameIndex,
                    BasePath.GetValue(time)
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<LinFloat64Vector3D, double> positionToValueMap)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedScalar GetLength()
    {
        return GrVisualAnimatedScalar.Create(
            Float64ScalarSignal.FiniteComputed(SamplingSpecs.MinTime,
                SamplingSpecs.MaxTime, time => GetValue(time).VectorENorm()),
            SamplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D SetLength(double length)
    {
        var baseCurve = Float64ComputedPath3D.Finite(
            time => GetValue(time).SetLength(length)
        );

        return new GrVisualAnimatedVector3D(
            SamplingSpecs,
            baseCurve
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(double length)
    {
        var baseCurve = 
            Float64ComputedPath3D.Finite(
                TimeRange,
                time => GetValue(time).AddLength(length),
                GetDerivative1Value
            );

        return new GrVisualAnimatedVector3D(
            SamplingSpecs,
            baseCurve
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(GrVisualAnimatedScalar length)
    {
        if (SamplingSpecs != length.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = 
            Float64ComputedPath3D.Finite(
                TimeRange,
                time=> GetValue(time).AddLength(length.GetValue(time)),
                GetDerivative1Value
            );

        return new GrVisualAnimatedVector3D(
            SamplingSpecs,
            baseCurve
        );
    }
}