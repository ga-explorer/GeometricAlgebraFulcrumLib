using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector3D :
    GrVisualAnimatedGeometry,
    IParametricCurve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D Create(GrVisualAnimationSpecs animationSpecs, IParametricCurve3D baseCurve)
    {
        return new GrVisualAnimatedVector3D(
            animationSpecs,
            baseCurve,
            animationSpecs.FrameTimeRange
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D Create(GrVisualAnimationSpecs animationSpecs, IParametricCurve3D baseCurve)
    //{
    //    return new GrVisualAnimatedVector3D(
    //        animationSpecs,
    //        baseCurve,
    //        animationSpecs.FrameTimeRange,
    //        invalidFrameIndices
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GrVisualAnimatedVector3D Create(GrVisualAnimationSpecs animationSpecs, IParametricCurve3D baseCurve, Float64ScalarRange baseParameterRange)
    {
        return new GrVisualAnimatedVector3D(
            animationSpecs,
            baseCurve,
            baseParameterRange
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GrVisualAnimatedVector3D Create(GrVisualAnimationSpecs animationSpecs, IParametricCurve3D baseCurve, Float64ScalarRange baseParameterRange)
    //{
    //    return new GrVisualAnimatedVector3D(
    //        animationSpecs,
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
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, ILinFloat64Vector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) + p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(ILinFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1 + p2.GetPoint(time),
            p2.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, ILinFloat64Vector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) - p2,
            p1.GetDerivative1Point
        );

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(ILinFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1 - p2.GetPoint(time),
            time => -p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator *(double p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time)
        );

        return new GrVisualAnimatedVector3D(
            p2.AnimationSpecs,
            baseCurve,
            p2.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, double p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2
        );

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedScalar p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetValue(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Value(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetValue(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, GrVisualAnimatedScalar p2)
    {
        if (p1.AnimationSpecs != p2.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time => p1.GetPoint(time) * p2.GetValue(time),
            time => p1.GetDerivative1Point(time) * p2.GetValue(time) + p2.GetDerivative1Value(time) * p1.GetPoint(time)
        );

        return new GrVisualAnimatedVector3D(
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
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
            p1.AnimationSpecs,
            baseCurve,
            p1.FrameTimeRange
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
        => FrameTimeRange;

    
    private GrVisualAnimatedVector3D(GrVisualAnimationSpecs animationSpecs, IParametricCurve3D baseCurve, Float64ScalarRange baseParameterRange)
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
    public LinFloat64Vector3D GetPoint(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public LinFloat64Vector3D GetDerivative1Point(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Point(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public ParametricCurveLocalFrame3D GetFrame(double time)
    {
        if (!FrameTimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetFrame(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, LinFloat64Vector3D>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return FrameIndexTimePairs.Select(
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedScalar GetLength()
    {
        return GrVisualAnimatedScalar.Create(
            AnimationSpecs,
            ComputedParametricScalar.Create(time => GetPoint(time).VectorENorm()),
            BaseParameterRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D SetLength(double length)
    {
        var baseCurve = ComputedParametricCurve3D.Create(
            time => GetPoint(time).SetLength(length)
        );

        return new GrVisualAnimatedVector3D(
            AnimationSpecs,
            baseCurve,
            FrameTimeRange
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
            AnimationSpecs,
            baseCurve,
            FrameTimeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(GrVisualAnimatedScalar length)
    {
        if (AnimationSpecs != length.AnimationSpecs)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(
            time=> GetPoint(time).AddLength(length.GetValue(time)),
            GetDerivative1Point
        );

        return new GrVisualAnimatedVector3D(
            AnimationSpecs,
            baseCurve,
            FrameTimeRange
        );
    }
}