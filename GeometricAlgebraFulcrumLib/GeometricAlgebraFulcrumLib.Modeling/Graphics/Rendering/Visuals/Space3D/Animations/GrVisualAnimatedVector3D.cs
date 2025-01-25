using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Polynomials;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector3D :
    GrVisualAnimatedGeometry,
    IParametricCurve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D Create(Float64SamplingSpecs samplingSpecs, IParametricCurve3D baseCurve)
    {
        return new GrVisualAnimatedVector3D(
            samplingSpecs,
            baseCurve,
            samplingSpecs.TimeRange
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D Create(GrVisualAnimationSpecs samplingSpecs, IParametricCurve3D baseCurve)
    //{
    //    return new GrVisualAnimatedVector3D(
    //        samplingSpecs,
    //        baseCurve,
    //        samplingSpecs.FrameTimeRange,
    //        invalidFrameIndices
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D Create(Float64SamplingSpecs samplingSpecs, IParametricCurve3D baseCurve, Float64ScalarRange baseParameterRange)
    {
        return new GrVisualAnimatedVector3D(
            samplingSpecs,
            baseCurve,
            baseParameterRange
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D Create(GrVisualAnimationSpecs samplingSpecs, IParametricCurve3D baseCurve, Float64ScalarRange baseParameterRange)
    //{
    //    return new GrVisualAnimatedVector3D(
    //        samplingSpecs,
    //        baseCurve,
    //        baseParameterRange,
    //        invalidFrameIndices
    //    );
    //}


    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => -p1.GetPoint(time),
            time => -p1.GetDerivative1Point(time));

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, ILinFloat64Vector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) + p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(ILinFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1 + p2.GetPoint(time),
            p2.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            p2.SamplingSpecs,
            baseCurve,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, ILinFloat64Vector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) - p2,
            p1.GetDerivative1Point
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(ILinFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1 - p2.GetPoint(time),
            time => -p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p2.SamplingSpecs,
            baseCurve,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator *(double p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p2.SamplingSpecs,
            baseCurve,
            p2.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, double p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetValue(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Value(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetValue(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) * p2.GetValue(time),
            time => p1.GetDerivative1Point(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetPoint(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator /(GrVisualAnimatedVector3D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2
        );

        return new GrVisualAnimatedVector3D(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    
    public IParametricCurve3D BaseCurve { get; }
    
    public Float64ScalarRange BaseParameterRange { get; }
    
    public DifferentialFunction BaseParameterToTimeMap { get; }

    public DifferentialFunction TimeToBaseParameterMap { get; }
    
    public double MinBaseParameter 
        => BaseParameterRange.MinValue;

    public double MaxBaseParameter 
        => BaseParameterRange.MaxValue;
    
    public Float64ScalarRange ParameterRange 
        => TimeRange;

    
    private GrVisualAnimatedVector3D(Float64SamplingSpecs samplingSpecs, IParametricCurve3D baseCurve, Float64ScalarRange baseParameterRange)
        : base(samplingSpecs)
    {
        BaseCurve = baseCurve;
        BaseParameterRange = baseParameterRange;

        BaseParameterToTimeMap = DfAffinePolynomial.Create(
            MinBaseParameter,
            MaxBaseParameter,
            MinTime,
            MaxTime
        );
            
        TimeToBaseParameterMap = DfAffinePolynomial.Create(
            MinTime,
            MaxTime,
            MinBaseParameter,
            MaxBaseParameter
        );
        
        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return TimeRange.IsValid() &&
               TimeRange.IsFinite &&
               TimeRange.MinValue >= 0 &&
               BaseCurve.IsValid() &&
               BaseParameterRange.IsValid() &&
               BaseParameterRange.IsFinite;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public LinFloat64Vector3D GetDerivative1Point(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Point(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public ParametricCurveLocalFrame3D GetFrame(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetFrame(
            TimeToBaseParameterMap.GetValue(time)
        );
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
                    BaseCurve.GetPoint(
                        TimeToBaseParameterMap.GetValue(time)
                    )
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

                var t = TimeToBaseParameterMap.GetValue(time);

                return new KeyValuePair<int, double>(
                    frameIndex,
                    positionToValueMap(BaseCurve.GetPoint(t))
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedScalar GetLength()
    {
        return GrVisualAnimatedScalar.Create(
            TemporalFloat64Scalar.Computed(
                time => GetPoint(time).VectorENorm(),
                SamplingSpecs.MinTime,
                SamplingSpecs.MaxTime
            ),
            SamplingSpecs
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D SetLength(double length)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => GetPoint(time).SetLength(length)
        );

        return new GrVisualAnimatedVector3D(
            SamplingSpecs,
            baseCurve,
            TimeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(double length)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => GetPoint(time).AddLength(length),
            GetDerivative1Point
        );

        return new GrVisualAnimatedVector3D(
            SamplingSpecs,
            baseCurve,
            TimeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(GrVisualAnimatedScalar length)
    {
        if (SamplingSpecs != length.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time=> GetPoint(time).AddLength(length.GetValue(time)),
            GetDerivative1Point
        );

        return new GrVisualAnimatedVector3D(
            SamplingSpecs,
            baseCurve,
            TimeRange
        );
    }
}