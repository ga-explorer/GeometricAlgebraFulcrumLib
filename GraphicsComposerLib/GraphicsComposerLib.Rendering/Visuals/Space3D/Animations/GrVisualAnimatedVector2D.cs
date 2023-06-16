using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector2D :
    GrVisualAnimatedGeometry,
    IParametricCurve2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedVector2D Create(IParametricCurve2D baseCurve, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedVector2D(
            baseCurve,
            timeRange,
            timeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedVector2D Create(IParametricCurve2D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedVector2D(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }

    
    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => -p1.GetPoint(time),
            time => -p1.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator +(GrVisualAnimatedVector2D p1, IFloat64Tuple2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) + p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator +(IFloat64Tuple2D p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1 + p2.GetPoint(time),
            p2.GetDerivative1Point);

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator +(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1, IFloat64Tuple2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) - p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator -(IFloat64Tuple2D p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1 - p2.GetPoint(time),
            time => -p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator *(double p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector2D p1, double p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector1D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector1D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator /(GrVisualAnimatedVector2D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector2D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    
    public IParametricCurve2D BaseCurve { get; }
    
    public Float64Range1D BaseParameterRange { get; }

    public override Float64Range1D TimeRange { get; }

    public DifferentialFunction BaseParameterToTimeMap { get; }

    public DifferentialFunction TimeToBaseParameterMap { get; }
    
    public double MinBaseParameter 
        => BaseParameterRange.MinValue;

    public double MaxBaseParameter 
        => BaseParameterRange.MaxValue;

    public Float64Range1D ParameterRange 
        => TimeRange;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVector2D(IParametricCurve2D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        BaseCurve = baseCurve;
        BaseParameterRange = baseParameterRange;
        TimeRange = timeRange;

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
    public Float64Vector2D GetPoint(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative1Point(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Point(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetFrame(
            TimeToBaseParameterMap.GetValue(time)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, Float64Vector2D>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return GetKeyFrameIndexTimePairs(frameRate).Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, Float64Vector2D>(
                    frameIndex,
                    BaseCurve.GetPoint(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<Float64Vector2D, double> positionToValueMap)
    {
        return GetKeyFrameIndexTimePairs(frameRate).Select(
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

}