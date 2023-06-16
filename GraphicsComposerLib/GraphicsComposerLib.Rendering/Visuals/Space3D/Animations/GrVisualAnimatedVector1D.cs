using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedVector1D :
    GrVisualAnimatedGeometry,
    IParametricCurve1D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedVector1D Create(IParametricCurve1D baseCurve, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedVector1D(
            baseCurve,
            timeRange,
            timeRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedVector1D Create(IParametricCurve1D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedVector1D(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }


    public static GrVisualAnimatedVector1D operator -(GrVisualAnimatedVector1D p1)
    {
        var baseCurve = ComputedParametricCurve1D.Create(time => -p1.GetPoint(time),
            time => -p1.GetDerivative1Point(time));

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector1D operator +(GrVisualAnimatedVector1D p1, GrVisualAnimatedVector1D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve1D.Create(time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector1D operator -(GrVisualAnimatedVector1D p1, GrVisualAnimatedVector1D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve1D.Create(time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector1D operator *(double p1, GrVisualAnimatedVector1D p2)
    {
        var baseCurve = ComputedParametricCurve1D.Create(time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time));

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector1D operator *(GrVisualAnimatedVector1D p1, double p2)
    {
        var baseCurve = ComputedParametricCurve1D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedVector1D operator *(GrVisualAnimatedVector1D p1, GrVisualAnimatedVector1D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricCurve1D.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedVector1D operator /(GrVisualAnimatedVector1D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricCurve1D.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedVector1D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    

    public IParametricCurve1D BaseCurve { get; }
    
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


    private GrVisualAnimatedVector1D(IParametricCurve1D baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        BaseCurve = baseCurve;
        BaseParameterRange = baseParameterRange;
        TimeRange = timeRange;

        if (baseParameterRange == timeRange)
        {
            BaseParameterToTimeMap = DfAffinePolynomial.Identity;
            TimeToBaseParameterMap = DfAffinePolynomial.Identity;
        }
        else
        {
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
        }

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
    public double GetPoint(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivative1Point(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Point(
            TimeToBaseParameterMap.GetValue(time)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return GetKeyFrameIndexTimePairs(frameRate).Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, double>(
                    frameIndex,
                    BaseCurve.GetPoint(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<double, double> positionToValueMap)
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