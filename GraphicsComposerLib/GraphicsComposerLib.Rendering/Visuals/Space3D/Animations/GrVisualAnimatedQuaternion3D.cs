using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Polynomials;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Quaternions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public class GrVisualAnimatedQuaternion3D :
    GrVisualAnimatedGeometry,
    IParametricQuaternion
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedQuaternion3D Create(IParametricQuaternion baseCurve, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            timeRange,
            timeRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static GrVisualAnimatedQuaternion3D Create(IParametricQuaternion baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
    {
        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            baseParameterRange,
            timeRange
        );
    }


    public static GrVisualAnimatedQuaternion3D operator -(GrVisualAnimatedQuaternion3D p1)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => -p1.GetPoint(time),
            time => -p1.GetDerivative1Point(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator +(GrVisualAnimatedQuaternion3D p1, Float64Quaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(
            time => p1.GetPoint(time) + p2,
            p1.GetDerivative1Point
        );

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator +(Float64Quaternion p1, GrVisualAnimatedQuaternion3D p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(
            time => p1 + p2.GetPoint(time),
            p2.GetDerivative1Point
        );

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion3D operator +(GrVisualAnimatedQuaternion3D p1, GrVisualAnimatedQuaternion3D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetPoint(time) + p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) + p2.GetDerivative1Point(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator -(GrVisualAnimatedQuaternion3D p1, Float64Quaternion p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(
            time => p1.GetPoint(time) - p2,
            p1.GetDerivative1Point
        );

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator -(Float64Quaternion p1, GrVisualAnimatedQuaternion3D p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => p1 - p2.GetPoint(time),
            time => -p2.GetDerivative1Point(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion3D operator -(GrVisualAnimatedQuaternion3D p1, GrVisualAnimatedQuaternion3D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetPoint(time) - p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) - p2.GetDerivative1Point(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion3D operator *(double p1, GrVisualAnimatedQuaternion3D p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => p1 * p2.GetPoint(time),
            time => p1 * p2.GetDerivative1Point(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p2.TimeRange,
            p2.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator *(GrVisualAnimatedQuaternion3D p1, double p2)
    {
        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator *(GrVisualAnimatedVector1D p1, GrVisualAnimatedQuaternion3D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    public static GrVisualAnimatedQuaternion3D operator *(GrVisualAnimatedQuaternion3D p1, GrVisualAnimatedVector1D p2)
    {
        if (p1.TimeRange != p2.TimeRange)
            throw new InvalidOperationException();

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetPoint(time) * p2.GetPoint(time),
            time => p1.GetDerivative1Point(time) * p2.GetPoint(time) + p2.GetDerivative1Point(time) * p1.GetPoint(time));

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }

    public static GrVisualAnimatedQuaternion3D operator /(GrVisualAnimatedQuaternion3D p1, double p2)
    {
        p2 = 1d / p2;

        var baseCurve = ComputedParametricQuaternion.Create(time => p1.GetPoint(time) * p2,
            time => p1.GetDerivative1Point(time) * p2);

        return new GrVisualAnimatedQuaternion3D(
            baseCurve,
            p1.TimeRange,
            p1.TimeRange
        );
    }
    
    
    public IParametricQuaternion BaseCurve { get; }
    
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

    
    private GrVisualAnimatedQuaternion3D(IParametricQuaternion baseCurve, Float64Range1D baseParameterRange, Float64Range1D timeRange)
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
    public Float64Quaternion GetPoint(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetPoint(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    public Float64Quaternion GetDerivative1Point(double time)
    {
        if (!TimeRange.Contains(time))
            throw new ArgumentOutOfRangeException();

        return BaseCurve.GetDerivative1Point(
            TimeToBaseParameterMap.GetValue(time)
        );
    }

    //public ParametricCurveLocalFrame3D GetFrame(double time)
    //{
    //    if (!TimeRange.Contains(time))
    //        throw new ArgumentOutOfRangeException();

    //    return BaseCurve.GetFrame(
    //        TimeToBaseParameterMap.GetValue(time)
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, Float64Quaternion>> GetKeyFrameIndexPositionPairs(int frameRate)
    {
        return GetKeyFrameIndexTimePairs(frameRate).Select(
            indexTimePair =>
            {
                var (frameIndex, time) = indexTimePair;
                
                return new KeyValuePair<int, Float64Quaternion>(
                    frameIndex,
                    BaseCurve.GetPoint(
                        TimeToBaseParameterMap.GetValue(time)
                    )
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetKeyFrameIndexValuePairs(int frameRate, Func<Float64Quaternion, double> positionToValueMap)
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
            time => GetPoint(time).Norm()
        ).CreateAnimatedVector(TimeRange);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public GrVisualAnimatedQuaternion3D AddLength(double length)
    //{
    //    var baseCurve = new ComputedParametricQuaternion(
    //        time => GetPoint(time).AddLength(length),
    //        GetDerivative1Point
    //    );

    //    return new GrVisualAnimatedQuaternion3D(
    //        baseCurve,
    //        TimeRange,
    //        TimeRange
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public GrVisualAnimatedQuaternion3D AddLength(GrVisualAnimatedVector1D length)
    //{
    //    if (TimeRange != length.TimeRange)
    //        throw new InvalidOperationException();

    //    var baseCurve = new ComputedParametricQuaternion(
    //        time=> GetPoint(time).AddLength(length.GetPoint(time)),
    //        GetDerivative1Point
    //    );

    //    return new GrVisualAnimatedQuaternion3D(
    //        baseCurve,
    //        TimeRange,
    //        TimeRange
    //    );
    //}
}