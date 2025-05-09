using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;

public static class Float64Path2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RouletteMappedPath2D GetRouletteMappedCurve(this Float64ArcLengthPath2D baseCurve, Float64RouletteAffineMap2D rouletteMap)
    {
        return Float64RouletteMappedPath2D.Create(baseCurve,
            rouletteMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMappedTimePath2D GetMappedParameterCurve(this Float64Path2D baseCurve, Float64AffineMap1D timeMap)
    {
        return Float64AffineMappedTimePath2D.Create(baseCurve, timeMap);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Float64AffineMappedTimePointPath2D GetMappedParameterCurveCosWave(this Float64PointPath2D baseCurve, int cycleCount = 1)
    //{
    //    return new Float64AffineMappedTimePointPath2D(
    //        baseCurve, t =>
    //            t.CosWave(
    //                baseCurve.TimeRange,
    //                cycleCount
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Float64AffineMappedTimePointPath2D GetMappedParameterCurveTriangleWave(this Float64PointPath2D baseCurve, int cycleCount = 1)
    //{
    //    return new Float64AffineMappedTimePointPath2D(
    //        baseCurve, t =>
    //            t.TriangleWave(
    //                baseCurve.TimeRange,
    //                cycleCount
    //            )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetTangent(this Float64Path2D curve, double t)
    {
        return curve.GetDerivative1Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Path2DLocalFrame> GetTangentData(this Float64Path2D curve, IEnumerable<double> parameterValueList)
    {
        return parameterValueList.Select(
            t =>
                Float64Path2DLocalFrame.Create(
                    t,
                    curve.GetValue(t),
                    curve.GetDerivative1Value(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetUnitTangent(this Float64Path2D curve, double t)
    {
        return curve.GetDerivative1Value(t).ToUnitLinVector2D();
    }

    public static double ComputeCurveLength(this IEnumerable<Float64Path2DLocalFrame> framesList)
    {
        var arcLength = 0d;

        Float64Path2DLocalFrame? frame1 = null;
        var firstFrame = true;
        foreach (var frame2 in framesList)
        {
            if (firstFrame)
            {
                frame1 = frame2;
                firstFrame = false;
                continue;
            }

            arcLength += frame2.Point.GetDistanceToPoint(frame1?.Point ?? frame2.Point);

            frame1 = frame2;
        }

        return arcLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> GetPointsAt(this Float64Path2D curve, IEnumerable<double> tList)
    {
        return tList.Select(curve.GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D[] GetPointsAt(this Float64Path2D curve, params double[] tList)
    {
        return tList.Select(curve.GetValue).ToArray();
    }

    //public static Tuple2D[] ToPolyline(this ICurve2D curve, Float64Range1D curveParamRange, int pointsCount, double lengthError = 0.95)
    //{
    //    pointsCount = (Math.Max(pointsCount, 2) - 2).UpperPower2Limit();


    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<double> GetParameterValues(this Float64AdaptivePath2D curve)
    {
        return curve.Select(f => f.Time.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> GetPoints(this Float64AdaptivePath2D curve)
    {
        return curve.Select(f => f.Point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> GetPoints(this Float64Path2D curve, IEnumerable<double> tList)
    {
        return tList.Select(curve.GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D[] GetPoints(this Float64Path2D curve, params double[] tList)
    {
        return tList.Select(curve.GetValue).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> GetTangents(this Float64AdaptivePath2D curve)
    {
        return curve.Select(f => f.Tangent);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AffineFrame2D ToAffineFrame(this Float64Path2DLocalFrame frame)
    {
        return LinFloat64AffineFrame2D.Create(
            frame.Point,
            frame.Tangent,
            frame.Normal
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2DLocalFrame ToLocalCurveFrame(this LinFloat64AffineFrame2D frame, double t)
    {
        return Float64Path2DLocalFrame.Create(
            t,
            frame.Origin,
            frame.UDirection
        );
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Float64Path2DLocalFrame GetFrenetSerretFrame(this Float64Path2D curve, double t)
    {
        var origin = curve.GetValue(t);

        var vDt1 = curve.GetDerivative1Value(t);
        var sDt1 = vDt1.VectorENorm();
        var vDs1 = vDt1 / sDt1;

        return Float64Path2DLocalFrame.Create(
            t,
            origin,
            vDs1
        );
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static LinFloat64AffineFrame2D GetFrenetSerretAffineFrame(this Float64Path2D curve, double t)
    {
        var origin = curve.GetValue(t);

        var vDt1 = curve.GetDerivative1Value(t);
        var vDt2 = curve.GetDerivative2Value(t);

        var sDt1 = vDt1.VectorENorm();
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

        var e1 = vDs1;
        var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitLinVector2D();

        return LinFloat64AffineFrame2D.Create(origin, e1, e2);
    }
    


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetMinVector2D(this IPair<Float64ScalarRange> range)
    {
        return LinFloat64Vector2D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetMidVector2D(this IPair<Float64ScalarRange> range)
    {
        return LinFloat64Vector2D.Create(
            range.Item1.MidValue,
            range.Item2.MidValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetMaxVector2D(this IPair<Float64ScalarRange> range)
    {
        return LinFloat64Vector2D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> GetMinMaxVector2D(this IPair<Float64ScalarRange> range)
    {
        var minVector = LinFloat64Vector2D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue
        );

        var maxVector = LinFloat64Vector2D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue
        );

        return new Pair<LinFloat64Vector2D>(minVector, maxVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D NegativeTime(this Float64Path2D baseSignal)
    {
        var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)(-1));

        return baseSignal.MapTimeUsing(timeMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ScaleTimeBy(this Float64Path2D baseSignal, double timeScaling)
    {
        var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)timeScaling);

        return baseSignal.MapTimeUsing(timeMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ScaleTimeUsing(this Float64Path2D baseSignal, double inTime, double outTime)
    {
        return baseSignal.ScaleTimeBy(outTime / inTime);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetTimeBy(this Float64Path2D baseSignal, double timeOffset)
    {
        if (timeOffset.IsZero()) 
            return baseSignal;
        
        return baseSignal.MapTimeUsing(
            Float64AffineMap1D.Create(
                Float64Scalar.One, 
                timeOffset
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetTimeUsing(this Float64Path2D baseSignal, double inTime, double outTime)
    {
        return baseSignal.OffsetTimeBy(outTime - inTime);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetTimeMinTo(this Float64Path2D baseSignal, double outTimeMin)
    {
        return baseSignal.OffsetTimeBy(outTimeMin - baseSignal.MinTime);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetTimeMaxTo(this Float64Path2D baseSignal, double outTimeMax)
    {
        return baseSignal.OffsetTimeBy(outTimeMax - baseSignal.MaxTime);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetTimeMidTo(this Float64Path2D baseSignal, double outTimeMid)
    {
        return baseSignal.OffsetTimeBy(outTimeMid - baseSignal.MidTime);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapTimeUsing(this Float64Path2D baseSignal, Float64AffineMap1D affineMap)
    {
        if (affineMap.IsIdentity())
            return baseSignal;

        if (baseSignal is Float64AffineMappedTimePath2D affineScalar)
            return Float64AffineMappedTimePath2D.Create(
                affineScalar.BasePath,
                affineMap * affineScalar.TimeMap
            );

        return Float64AffineMappedTimePath2D.Create(
            baseSignal,
            affineMap
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapTimeUsing(this Float64Path2D baseSignal, double inTime1, double inTime2, double outTime1, double outTime2)
    {
        var timeMap = Float64AffineMap1D.CreateFromRanges(inTime1, inTime2, outTime1, outTime2);

        return baseSignal.MapTimeUsing(timeMap);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapTimeRangeTo(this Float64Path2D baseSignal, Float64ScalarRange timeRange)
    {
        if (baseSignal.TimeRange.Equals(timeRange))
            return baseSignal;

        var (outTime1, outTime2) = timeRange;

        return baseSignal.MapTimeUsing(
            baseSignal.MinTime,
            baseSignal.MaxTime,
            outTime1,
            outTime2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapTimeRangeTo(this Float64Path2D baseSignal, double outTime1, double outTime2)
    {
        if (baseSignal.TimeRange.Equals(outTime1, outTime2))
            return baseSignal;

        return baseSignal.MapTimeUsing(
            baseSignal.MinTime,
            baseSignal.MaxTime,
            outTime1,
            outTime2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D FlipTimeRange(this Float64Path2D baseSignal)
    {
        return baseSignal.MapTimeUsing(
            baseSignal.MinTime,
            baseSignal.MaxTime,
            baseSignal.MaxTime,
            baseSignal.MinTime
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D FlipTimeRange(this Float64Path2D baseSignal, double time1, double time2)
    {
        return baseSignal.MapTimeUsing(
            time1,
            time2,
            time2,
            time1
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D NegativeValue(this Float64Path2D baseSignal)
    {
        var valueMap = Float64InvertibleAffineMap2D.Create().ReflectOrigin();

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ScaleValueBy(this Float64Path2D baseSignal, LinFloat64Vector2D valueScaling)
    {
        var valueMap = Float64InvertibleAffineMap2D.Create().Scale(valueScaling);

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ScaleValueUsing(this Float64Path2D baseSignal, LinFloat64Vector2D inValue, LinFloat64Vector2D outValue)
    {
        return baseSignal.ScaleValueBy(
            LinFloat64Vector2D.Create(
                outValue.Item1 / inValue.Item1, 
                outValue.Item2 / inValue.Item2
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetValueBy(this Float64Path2D baseSignal, LinFloat64Vector2D valueOffset)
    {
        var valueMap = Float64InvertibleAffineMap2D.Create().Translate(valueOffset);

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetValueUsing(this Float64Path2D baseSignal, LinFloat64Vector2D inValue, LinFloat64Vector2D outValue)
    {
        return baseSignal.OffsetValueBy(outValue - inValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetValueMinTo(this Float64Path2D baseSignal, LinFloat64Vector2D outValueMin)
    {
        return baseSignal.OffsetValueBy(outValueMin - baseSignal.GetValueRange().GetMinVector2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetValueMaxTo(this Float64Path2D baseSignal, LinFloat64Vector2D outValueMax)
    {
        return baseSignal.OffsetValueBy(outValueMax - baseSignal.GetValueRange().GetMaxVector2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D OffsetValueMidTo(this Float64Path2D baseSignal, LinFloat64Vector2D outValueMid)
    {
        return baseSignal.OffsetValueBy(outValueMid - baseSignal.GetValueRange().GetMidVector2D());
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Float64Path2D MapValueUsing(this Float64Path2D baseSignal, Func<double, double> valueMap)
    //{
    //    if (baseSignal is Float64MappedScalarSignal mappedScalar)
    //        return Float64MappedScalarSignal.Create(mappedScalar.BaseSignal,
    //            t => valueMap(mappedScalar.ValueMap(t)));

    //    return Float64MappedScalarSignal.Create(baseSignal, valueMap);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapValueUsing(this Float64Path2D baseSignal, IFloat64AffineMap2D affineMap)
    {
        if (affineMap.IsIdentity())
            return baseSignal;

        if (baseSignal is Float64AffineMappedPath2D affineScalar)
            return Float64AffineMappedPath2D.Create(
                affineScalar.BasePath,
                affineScalar.AffineMap.TransformUsing(affineMap)
            );

        return Float64AffineMappedPath2D.Create(
            baseSignal,
            affineMap
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapValueUsing(this Float64Path2D baseSignal, IPair<Float64ScalarRange> inValueRange, IPair<Float64ScalarRange> outValueRange)
    {
        var valueMap = Float64ScaleTranslateAffineMap2D.CreateFromRanges(
            inValueRange, 
            outValueRange
        );

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapValueUsing(this Float64Path2D baseSignal, LinFloat64Vector2D inValue1, LinFloat64Vector2D inValue2, LinFloat64Vector2D outValue1, LinFloat64Vector2D outValue2)
    {
        var valueMap = Float64ScaleTranslateAffineMap2D.CreateFromRanges(
            inValue1, 
            inValue2, 
            outValue1, 
            outValue2
        );

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapValueRangeTo(this Float64Path2D baseSignal, Pair<Float64ScalarRange> valueRange)
    {
        return baseSignal.MapValueUsing(
            baseSignal.GetValueRange(),
            valueRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D MapValueRangeTo(this Float64Path2D baseSignal, LinFloat64Vector2D outValue1, LinFloat64Vector2D outValue2)
    {
        var (inValue1, inValue2) =
            baseSignal.GetValueRange().GetMinMaxVector2D();

        return baseSignal.MapValueUsing(
            inValue1,
            inValue2,
            outValue1,
            outValue2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D FlipValueRange(this Float64Path2D baseSignal)
    {
        var (valueMin, valueMax) =
            baseSignal.GetValueRange().GetMinMaxVector2D();

        return baseSignal.MapValueUsing(
            valueMin,
            valueMax,
            valueMax,
            valueMin
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D FlipValueRange(this Float64Path2D baseSignal, LinFloat64Vector2D value1, LinFloat64Vector2D value2)
    {
        return baseSignal.MapValueUsing(
            value1,
            value2,
            value2,
            value1
        );
    }

}