using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Polynomials;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedQuaternion :
    GrVisualAnimatedGeometry,
    IParametricQuaternion
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedQuaternion Create(Float64SamplingSpecs samplingSpecs, IParametricQuaternion baseCurve)
    {
        return new GrVisualAnimatedQuaternion(
            samplingSpecs,
            baseCurve,
            samplingSpecs.TimeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedQuaternion Create(Float64SamplingSpecs samplingSpecs, IParametricQuaternion baseCurve, Float64ScalarRange baseParameterRange)
    {
        return new GrVisualAnimatedQuaternion(
            samplingSpecs,
            baseCurve,
            baseParameterRange
        );
    }


    public static GrVisualAnimatedQuaternion operator -(GrVisualAnimatedQuaternion p1)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => -p1.GetQuaternion(time),
            time => -p1.GetDerivative1Quaternion(time));

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator +(GrVisualAnimatedQuaternion p1, LinFloat64Quaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(
            time => p1.GetQuaternion(time) + p2,
            p1.GetDerivative1Quaternion
        );

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator +(LinFloat64Quaternion p1, GrVisualAnimatedQuaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(
            time => p1 + p2.GetQuaternion(time),
            p2.GetDerivative1Quaternion
        );

        return new GrVisualAnimatedQuaternion(
            p2.SamplingSpecs,
            baseCurve,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion operator +(GrVisualAnimatedQuaternion p1, GrVisualAnimatedQuaternion p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetQuaternion(time) + p2.GetQuaternion(time),
            time => p1.GetDerivative1Quaternion(time) + p2.GetDerivative1Quaternion(time));

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator -(GrVisualAnimatedQuaternion p1, LinFloat64Quaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(
            time => p1.GetQuaternion(time) - p2,
            p1.GetDerivative1Quaternion
        );

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator -(LinFloat64Quaternion p1, GrVisualAnimatedQuaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => p1 - p2.GetQuaternion(time),
            time => -p2.GetDerivative1Quaternion(time));

        return new GrVisualAnimatedQuaternion(
            p2.SamplingSpecs,
            baseCurve,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion operator -(GrVisualAnimatedQuaternion p1, GrVisualAnimatedQuaternion p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetQuaternion(time) - p2.GetQuaternion(time),
            time => p1.GetDerivative1Quaternion(time) - p2.GetDerivative1Quaternion(time));

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion operator *(double p1, GrVisualAnimatedQuaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => p1 * p2.GetQuaternion(time),
            time => p1 * p2.GetDerivative1Quaternion(time));

        return new GrVisualAnimatedQuaternion(
            p2.SamplingSpecs,
            baseCurve,
            p2.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator *(GrVisualAnimatedQuaternion p1, double p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetQuaternion(time) * p2,
            time => p1.GetDerivative1Quaternion(time) * p2);

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedQuaternion p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetValue(time) * p2.GetQuaternion(time),
            time => p1.GetDerivative1Value(time) * p2.GetQuaternion(time) + p2.GetDerivative1Quaternion(time) * p1.GetValue(time));

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion operator *(GrVisualAnimatedQuaternion p1, GrVisualAnimatedScalar p2)
    {
        if (p1.SamplingSpecs != p2.SamplingSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetQuaternion(time) * p2.GetValue(time),
            time => p1.GetDerivative1Quaternion(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetQuaternion(time));

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion operator /(GrVisualAnimatedQuaternion p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetQuaternion(time) * p2,
            time => p1.GetDerivative1Quaternion(time) * p2);

        return new GrVisualAnimatedQuaternion(
            p1.SamplingSpecs,
            baseCurve,
            p1.TimeRange
        );
    }
    
    
    public IParametricQuaternion BaseCurve { get; }
    
    public Float64ScalarRange BaseParameterRange { get; }
    
    public DifferentialFunction BaseParameterToTimeMap { get; }

    public DifferentialFunction TimeToBaseParameterMap { get; }
    
    public double MinBaseParameter 
        => BaseParameterRange.MinValue;

    public double MaxBaseParameter 
        => BaseParameterRange.MaxValue;
    
    public Float64ScalarRange ParameterRange 
        => TimeRange;

    
    private GrVisualAnimatedQuaternion(Float64SamplingSpecs samplingSpecs, IParametricQuaternion baseCurve, Float64ScalarRange baseParameterRange)
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
        return BaseCurve.IsValid() &&
               BaseParameterRange.IsValid() &&
               BaseParameterRange.IsFinite;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetQuaternion(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetQuaternion(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public LinFloat64Quaternion GetDerivative1Quaternion(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Quaternion(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    //public ParametricCurveLocalFrame3D GetFrame(double time)
    //{
    //    if (!FrameTimeRange.Contains(time))
    //        throw new ArgumentOutOfRangeException();

    //    return BaseCurve.GetFrame(
    //        TimeToBaseParameterMap.GetValue(time)
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Quaternion>> GetKeyFrameIndexPositionPairs()
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, LinFloat64Quaternion>(
                    frameIndex,
                    BaseCurve.GetQuaternion(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(Func<LinFloat64Quaternion, double> positionToValueMap)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;

                var t = TimeToBaseParameterMap.GetValue(time);

                return new KeyValuePair<int, double>(
                    frameIndex,
                    positionToValueMap(BaseCurve.GetQuaternion(t))
                );
            }
        );
    }
    
}