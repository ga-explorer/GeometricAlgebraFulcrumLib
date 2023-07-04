using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector3D :
    GrVisualAnimatedGeometry,
    IParametricCurve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedVector3D Create(IParametricCurve3D baseCurve, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedVector3D(
            baseCurve,
            timeRange,
            timeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedVector3D Create(IParametricCurve3D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedVector3D(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }


    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => -p1.GetPoint(time),
            time => -p1.GetDerivative1Point(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, IFloat64Vector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) + p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator +(IFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1 + p2.GetPoint(time),
            p2.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator +(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, IFloat64Vector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) - p2,
            p1.GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator -(IFloat64Vector3D p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1 - p2.GetPoint(time),
            time => -p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator -(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator *(double p1, GrVisualAnimatedVector3D p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, double p2)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector1D p1, GrVisualAnimatedVector3D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector3D operator *(GrVisualAnimatedVector3D p1, GrVisualAnimatedVector1D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector3D operator /(GrVisualAnimatedVector3D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricCurve3D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    
    public IParametricCurve3D BaseCurve { get; }
    
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

    
    private GrVisualAnimatedVector3D(IParametricCurve3D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
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
    public Float64Vector3D GetPoint(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public Float64Vector3D GetDerivative1Point(double time)
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
    public IEnumerable<KeyValuePair<int, Float64Vector3D>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return GetKeyFrameIndexTimePairs(frameRate).Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, Float64Vector3D>(
                    frameIndex,
                    BaseCurve.GetPoint(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<Float64Vector3D, double> positionToValueMap)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector1D GetLength()
    {
        return ComputedParametricCurve1D.Create(
            time => GetPoint(time).ENorm()
        ).CreateAnimatedVector(TimeRange);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(double length)
    {
        var baseCurve = ComputedParametricCurve3D.Create(time => GetPoint(time).AddLength(length),
            GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            TimeRange,
            TimeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVisualAnimatedVector3D AddLength(GrVisualAnimatedVector1D length)
    {
        if (TimeRange != length.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve3D.Create(time=> GetPoint(time).AddLength(length.GetPoint(time)),
            GetDerivative1Point);

        return new GrVisualAnimatedVector3D(
            baseCurve,
            TimeRange,
            TimeRange
        );
    }
}