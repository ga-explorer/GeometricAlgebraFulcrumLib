using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedScalar :
    GrVisualAnimatedGeometry,
    IParametricScalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar Create(GrVisualAnimationSpecs animationSpecs, IParametricScalar baseCurve)
    {
        return new GrVisualAnimatedScalar(
            animationSpecs,
            baseCurve,
            animationSpecs.FrameTimeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedScalar Create(GrVisualAnimationSpecs animationSpecs, IParametricScalar baseCurve, Float64ScalarRange baseParameterRange)
    {
        return new GrVisualAnimatedScalar(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }
    

    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1)
    {
        var baseCurve = ComputedParametricScalar.Create(
            time => -p1.GetValue(time),
            time => -p1.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedScalar(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedScalar operator +(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricScalar.Create(
            time => p1.GetValue(time) + p2.GetValue(time),
            time => p1.GetDerivative1Value(time) + p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedScalar(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedScalar operator -(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricScalar.Create(
            time => p1.GetValue(time) - p2.GetValue(time),
            time => p1.GetDerivative1Value(time) - p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedScalar(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedScalar operator *(double p1, GrVisualAnimatedScalar p2)
    {
        var baseCurve = ComputedParametricScalar.Create(
            time => p1 * p2.GetValue(time),
            time => p1 * p2.GetDerivative1Value(time)
        );

        return new GrVisualAnimatedScalar(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedScalar operator *(GrVisualAnimatedScalar p1, double p2)
    {
        var baseCurve = ComputedParametricScalar.Create(
            time => p1.GetValue(time) * p2,
            time => p1.GetDerivative1Value(time) * p2
        );

        return new GrVisualAnimatedScalar(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedScalar operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedScalar p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricScalar.Create(
            time => p1.GetValue(time) * p2.GetValue(time),
            time => p1.GetDerivative1Value(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetValue(time)
        );

        return new GrVisualAnimatedScalar(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedScalar operator /(GrVisualAnimatedScalar p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricScalar.Create(
            time => p1.GetValue(time) * p2,
            time => p1.GetDerivative1Value(time) * p2
        );

        return new GrVisualAnimatedScalar(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    

    public IParametricScalar BaseCurve { get; }
    
    public Float64ScalarRange BaseParameterRange { get; }
    
    public DifferentialFunction BaseParameterToTimeMap { get; }

    public DifferentialFunction TimeToBaseParameterMap { get; }
    
    public double MinBaseParameter 
        => BaseParameterRange.MinValue;

    public double MaxBaseParameter 
        => BaseParameterRange.MaxValue;
    
    public Float64ScalarRange ParameterRange 
        => FrameTimeRange;


    private GrVisualAnimatedScalar(GrVisualAnimationSpecs animationSpecs, IParametricScalar baseCurve, Float64ScalarRange baseParameterRange)
        : base(animationSpecs)
    {
        BaseCurve = baseCurve;
        BaseParameterRange = baseParameterRange;

        if (baseParameterRange == animationSpecs.FrameTimeRange)
        {
            BaseParameterToTimeMap = DfAffinePolynomial.Identity;
            TimeToBaseParameterMap = DfAffinePolynomial.Identity;
        }
        else
        {
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
        }
        
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
    public Float64Scalar GetValue(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetValue(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Value(
            TimeToBaseParameterMap.GetValue(time)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return FrameIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, double>(
                    frameIndex,
                    BaseCurve.GetValue(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<double, double> positionToValueMap)
    {
        return FrameIndexTimePairs.Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;

                var t = TimeToBaseParameterMap.GetValue(time);

                return new KeyValuePair<int, double>(
                    frameIndex,
                    positionToValueMap(BaseCurve.GetValue(t))
                );
            }
        );
    }

}