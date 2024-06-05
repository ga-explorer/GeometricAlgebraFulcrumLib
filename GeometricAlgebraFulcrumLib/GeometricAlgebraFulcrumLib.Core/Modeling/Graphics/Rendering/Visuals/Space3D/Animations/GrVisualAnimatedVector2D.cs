using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector2D :
    GrVisualAnimatedGeometry,
    IFloat64ParametricCurve2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D Create(GrVisualAnimationSpecs animationSpecs, IFloat64ParametricCurve2D baseCurve)
    {
        return new GrVisualAnimatedVector2D(
            animationSpecs,
            baseCurve,
            animationSpecs.FrameTimeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector2D Create(GrVisualAnimationSpecs animationSpecs, IFloat64ParametricCurve2D baseCurve, Float64ScalarRange baseParameterRange)
    {
        return new GrVisualAnimatedVector2D(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }
    

    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => -p1.GetPoint(time),
            time => -p1.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator +(GrVisualAnimatedVector2D p1, ILinFloat64Vector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) + p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator +(ILinFloat64Vector2D p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1 + p2.GetPoint(time),
            p2.GetDerivative1Point);

        return new GrVisualAnimatedVector2D(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator +(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1, ILinFloat64Vector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) - p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator -(ILinFloat64Vector2D p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1 - p2.GetPoint(time),
            time => -p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator -(GrVisualAnimatedVector2D p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator *(double p1, GrVisualAnimatedVector2D p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector2D(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector2D p1, double p2)
    {
        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedVector2D p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetValue(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Value(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetValue(time));

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector2D operator *(GrVisualAnimatedVector2D p1, GrVisualAnimatedScalar p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2.GetValue(time),
            time => p1.GetDerivative1Point(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetPoint(time));

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector2D operator /(GrVisualAnimatedVector2D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricCurve2D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector2D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    
    public IFloat64ParametricCurve2D BaseCurve { get; }
    
    public Float64ScalarRange BaseParameterRange { get; }
    
    public DifferentialFunction BaseParameterToTimeMap { get; }

    public DifferentialFunction TimeToBaseParameterMap { get; }
    
    public double MinBaseParameter 
        => BaseParameterRange.MinValue;

    public double MaxBaseParameter 
        => BaseParameterRange.MaxValue;

    public Float64ScalarRange ParameterRange 
        => FrameTimeRange;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrVisualAnimatedVector2D(GrVisualAnimationSpecs animationSpecs, IFloat64ParametricCurve2D baseCurve, Float64ScalarRange baseParameterRange)
        : base(animationSpecs)
    {
        BaseCurve = baseCurve;
        BaseParameterRange = baseParameterRange;

        BaseParameterToTimeMap = DfAffinePolynomial.Create(
            MinBaseParameter,
            MaxBaseParameter,
            MinFrameTime,
            MaxFrameTime
        );
            
        TimeToBaseParameterMap = DfAffinePolynomial.Create(
            MinFrameTime,
            MaxFrameTime,
            MinBaseParameter,
            MaxBaseParameter
        );

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return FrameTimeRange.IsValid() &&
               FrameTimeRange.IsFinite &&
               FrameTimeRange.MinValue >= 0 &&
               BaseCurve.IsValid() &&
               BaseParameterRange.IsValid() &&
               BaseParameterRange.IsFinite;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Point(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetFrame(
            TimeToBaseParameterMap.GetValue(time)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector2D>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return FrameIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, LinFloat64Vector2D>(
                    frameIndex,
                    BaseCurve.GetPoint(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<LinFloat64Vector2D, double> positionToValueMap)
    {
        return FrameIndexTimePairs.Select(
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