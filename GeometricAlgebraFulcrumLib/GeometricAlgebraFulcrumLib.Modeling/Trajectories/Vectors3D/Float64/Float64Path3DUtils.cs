using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Samplers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

public static class Float64Path3DUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64AffineFrame3D ToAffineFrame(this IFloat64Path3DLocalFrame frame)
    {
        return LinFloat64AffineFrame3D.Create(
            frame.Point,
            frame.Tangent,
            frame.Normal1,
            frame.Normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3DLocalFrame ToLocalCurveFrame(this LinFloat64AffineFrame3D frame, double t)
    {
        return Float64Path3DLocalFrame.Create(
            t,
            frame.Origin,
            frame.Direction1,
            frame.Direction2,
            frame.Direction3
        );
    }

    public static LinFloat64Quaternion FrameToFrameRotationQuaternion(this Float64Path3DLocalFrame frame1, Float64Path3DLocalFrame frame2)
    {
        var q1 =
            frame1
                .Tangent
                .ToUnitLinVector3D()
                .VectorToVectorRotationQuaternion(frame2.Tangent.ToUnitLinVector3D());

        Debug.Assert(
            (q1.RotateVector(frame1.Tangent) - frame2.Tangent).VectorENormSquared().IsNearZero(1e-7)
        );

        var f1 =
            frame1.GetRotatedFrameUsingQuaternion(q1);

        var dot12 =
            q1
                .RotateVector(frame1.Normal1)
                .ToUnitLinVector3D()
                .VectorESp(frame2.Normal1.ToUnitLinVector3D());

        var q2 =
            q1
                .RotateVector(frame1.Normal1)
                .ToUnitLinVector3D()
                .VectorToVectorRotationQuaternion(frame2.Normal1.ToUnitLinVector3D(), frame2.Tangent);

        var quaternion = q2.Concatenate(q1);

        var f2 =
            f1.GetRotatedFrameUsingQuaternion(q2);

        Debug.Assert(
            (quaternion.RotateVector(frame1.Tangent) - frame2.Tangent).VectorENormSquared().IsNearZero(1e-7)
        );

        Debug.Assert(
            (quaternion.RotateVector(frame1.Normal1) - frame2.Normal1).VectorENormSquared().IsNearZero(1e-7)
        );

        Debug.Assert(
            (quaternion.RotateVector(frame1.Normal2) - frame2.Normal2).VectorENormSquared().IsNearZero(1e-7)
        );

        return quaternion;
    }

    public static double ComputePathLength(this IEnumerable<Float64Path3DLocalFrame> framesList)
    {
        var arcLength = 0d;

        Float64Path3DLocalFrame? frame1 = null;
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
    public static IEnumerable<double> GetTimeValues(this IEnumerable<Float64Path3DLocalFrame> path)
    {
        return path.Select(f => f.TimeValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector3D> GetPoints(this IEnumerable<Float64Path3DLocalFrame> path)
    {
        return path.Select(f => f.Point);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector3D> GetPoints(this Float64Path3D path, IEnumerable<double> tList)
    {
        return tList.Select(path.GetValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D[] GetPoints(this Float64Path3D path, params double[] tList)
    {
        return tList.Select(path.GetValue).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetTangent(this Float64Path3D path, double t)
    {
        return path.GetDerivative1Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetUnitTangent(this Float64Path3D path, double t)
    {
        return path.GetDerivative1Value(t).ToUnitLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector3D> GetTangents(this IEnumerable<Float64Path3DLocalFrame> path)
    {
        return path.Select(f => f.Tangent);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Path3DLocalFrame> GetLocalFrames(this Float64Path3D path, IEnumerable<double> tList)
    {
        return tList.Select(
            t =>
                Float64Path3DLocalFrame.Create(
                    t,
                    path.GetValue(t),
                    path.GetDerivative1Value(t)
                )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotatedNormalsPath3D RotateNormals(this Float64Path3D basePath, Func<double, LinFloat64Angle> angleFunction)
    {
        return new Float64RotatedNormalsPath3D(
            basePath,
            Float64ScalarSignal.CreateComputed(
                basePath.TimeRange, 
                basePath.IsPeriodic,
                t => angleFunction(t).RadiansValue
            ).RadiansToPolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotatedNormalsPath3D RotateNormals(this Float64Path3D basePath, LinFloat64PolarAngleTimeSignal angleFunction)
    {
        return new Float64RotatedNormalsPath3D(
            basePath,
            angleFunction
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RotatedNormalsPath3D RotateNormals(this Float64Path3D basePath, LinFloat64PolarAngle angle)
    {
        return new Float64RotatedNormalsPath3D(
            basePath,
            LinFloat64PolarAngleTimeSignal.CreateConstant(
                basePath.TimeRange,
                basePath.IsPeriodic,
                angle
            )
        );
    }
    

    public static Triplet<Float64Path3D> GetComponents(this Float64Path3D path)
    {
        var path1 = Float64ComputedPath3D.Finite(
            path.TimeRange,
            t => LinFloat64Vector3D.Create(
                path.GetValue(t).X,
                0,
                0
            ),
            t => LinFloat64Vector3D.Create(
                path.GetDerivative1Value(t).X,
                0,
                0
            )
        );

        var path2 = Float64ComputedPath3D.Finite(
            path.TimeRange,
            t => LinFloat64Vector3D.Create(
                0,
                path.GetValue(t).Y,
                0
            ),
            t => LinFloat64Vector3D.Create(
                0,
                path.GetDerivative1Value(t).Y,
                0
            )
        );

        var path3 = Float64ComputedPath3D.Finite(
            path.TimeRange,
            t => LinFloat64Vector3D.Create(
                0,
                0,
                path.GetValue(t).Z
            ),
            t => LinFloat64Vector3D.Create(
                0,
                0,
                path.GetDerivative1Value(t).Z
            )
        );

        return new Triplet<Float64Path3D>(path1, path2, path3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetGetDerivative1NormValue(this Float64Path3D path, double t)
    {
        return path is Float64DifferentialPath3D dCurve
            ? dCurve.GetTangentNormValue(t)
            : path.GetDerivative1Value(t).Norm().ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UniformParameterCurveSampler3D GetUniformParameterSampler(this Float64Path3D path, Float64ScalarRange parameterRange, int sampleCount, bool isPeriodic)
    {
        return new UniformParameterCurveSampler3D(
            path,
            parameterRange,
            sampleCount,
            isPeriodic
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AdaptiveCurveSampler3D GetAdaptiveSampler(this Float64Path3D path, Float64ScalarRange parameterRange, Float64AdaptivePath3DSamplingOptions samplingOptions, bool isPeriodic)
    {
        return new AdaptiveCurveSampler3D(
            path,
            parameterRange,
            samplingOptions,
            isPeriodic
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetMinVector3D(this ITriplet<Float64ScalarRange> range)
    {
        return LinFloat64Vector3D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue,
            range.Item3.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetMidVector3D(this ITriplet<Float64ScalarRange> range)
    {
        return LinFloat64Vector3D.Create(
            range.Item1.MidValue,
            range.Item2.MidValue,
            range.Item3.MidValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetMaxVector3D(this ITriplet<Float64ScalarRange> range)
    {
        return LinFloat64Vector3D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue,
            range.Item3.MaxValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector3D> GetMinMaxVector3D(this ITriplet<Float64ScalarRange> range)
    {
        var minVector = LinFloat64Vector3D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue,
            range.Item3.MinValue
        );

        var maxVector = LinFloat64Vector3D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue,
            range.Item3.MaxValue
        );

        return new Pair<LinFloat64Vector3D>(minVector, maxVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D NegativeTime(this Float64Path3D baseSignal)
    {
        var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)(-1));

        return baseSignal.MapTimeUsing(timeMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ScaleTimeBy(this Float64Path3D baseSignal, double timeScaling)
    {
        var timeMap = Float64AffineMap1D.CreateScale((Float64Scalar)timeScaling);

        return baseSignal.MapTimeUsing(timeMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ScaleTimeUsing(this Float64Path3D baseSignal, double inTime, double outTime)
    {
        return baseSignal.ScaleTimeBy(outTime / inTime);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetTimeBy(this Float64Path3D baseSignal, double timeOffset)
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
    public static Float64Path3D OffsetTimeUsing(this Float64Path3D baseSignal, double inTime, double outTime)
    {
        return baseSignal.OffsetTimeBy(outTime - inTime);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetTimeMinTo(this Float64Path3D baseSignal, double outTimeMin)
    {
        return baseSignal.OffsetTimeBy(outTimeMin - baseSignal.MinTime);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetTimeMaxTo(this Float64Path3D baseSignal, double outTimeMax)
    {
        return baseSignal.OffsetTimeBy(outTimeMax - baseSignal.MaxTime);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetTimeMidTo(this Float64Path3D baseSignal, double outTimeMid)
    {
        return baseSignal.OffsetTimeBy(outTimeMid - baseSignal.MidTime);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapTimeUsing(this Float64Path3D baseSignal, Float64AffineMap1D affineMap)
    {
        if (affineMap.IsIdentity())
            return baseSignal;

        if (baseSignal is Float64AffineMappedTimePath3D affineScalar)
            return Float64AffineMappedTimePath3D.Create(
                affineScalar.BasePath,
                affineMap * affineScalar.TimeMap
            );

        return Float64AffineMappedTimePath3D.Create(
            baseSignal,
            affineMap
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapTimeUsing(this Float64Path3D baseSignal, double inTime1, double inTime2, double outTime1, double outTime2)
    {
        var timeMap = Float64AffineMap1D.CreateFromRanges(inTime1, inTime2, outTime1, outTime2);

        return baseSignal.MapTimeUsing(timeMap);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapTimeRangeTo(this Float64Path3D baseSignal, Float64ScalarRange timeRange)
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
    public static Float64Path3D MapTimeRangeTo(this Float64Path3D baseSignal, double outTime1, double outTime2)
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
    public static Float64Path3D FlipTimeRange(this Float64Path3D baseSignal)
    {
        return baseSignal.MapTimeUsing(
            baseSignal.MinTime,
            baseSignal.MaxTime,
            baseSignal.MaxTime,
            baseSignal.MinTime
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D FlipTimeRange(this Float64Path3D baseSignal, double time1, double time2)
    {
        return baseSignal.MapTimeUsing(
            time1,
            time2,
            time2,
            time1
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D NegativeValue(this Float64Path3D baseSignal)
    {
        var valueMap = Float64InvertibleAffineMap3D.Create().ReflectOrigin();

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ScaleValueBy(this Float64Path3D baseSignal, double valueScaling)
    {
        var valueMap = Float64InvertibleAffineMap3D.Create().Scale(valueScaling);

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ScaleValueUsing(this Float64Path3D baseSignal, double inValue, double outValue)
    {
        return baseSignal.ScaleValueBy(outValue / inValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetValueBy(this Float64Path3D baseSignal, LinFloat64Vector3D valueOffset)
    {
        var valueMap = Float64InvertibleAffineMap3D.Create().Translate(valueOffset);

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetValueUsing(this Float64Path3D baseSignal, LinFloat64Vector3D inValue, LinFloat64Vector3D outValue)
    {
        return baseSignal.OffsetValueBy(outValue - inValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetValueMinTo(this Float64Path3D baseSignal, LinFloat64Vector3D outValueMin)
    {
        return baseSignal.OffsetValueBy(outValueMin - baseSignal.GetValueRange().GetMinVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetValueMaxTo(this Float64Path3D baseSignal, LinFloat64Vector3D outValueMax)
    {
        return baseSignal.OffsetValueBy(outValueMax - baseSignal.GetValueRange().GetMaxVector3D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D OffsetValueMidTo(this Float64Path3D baseSignal, LinFloat64Vector3D outValueMid)
    {
        return baseSignal.OffsetValueBy(outValueMid - baseSignal.GetValueRange().GetMidVector3D());
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Vector3DTrajectory MapValueUsing(this LinFloat64Vector3DTrajectory baseSignal, Func<LinFloat64Vector3D, LinFloat64Vector3D> valueMap)
    //{
    //    return Float64MappedVector3DPointPath3D.Create(baseSignal, valueMap);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapValueUsing(this Float64Path3D baseSignal, IFloat64AffineMap3D affineMap)
    {
        if (affineMap.IsIdentity())
            return baseSignal;

        if (baseSignal is Float64AffineMappedPath3D affineScalar)
            return Float64AffineMappedPath3D.Create(
                affineScalar.BasePath,
                affineScalar.AffineMap.TransformUsing(affineMap)
            );

        return Float64AffineMappedPath3D.Create(
            baseSignal,
            affineMap
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapValueUsing(this Float64Path3D baseSignal, ITriplet<Float64ScalarRange> inValueRange, ITriplet<Float64ScalarRange> outValueRange)
    {
        var valueMap = Float64ScaleTranslateAffineMap3D.CreateFromRanges(
            inValueRange, 
            outValueRange
        );

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapValueUsing(this Float64Path3D baseSignal, LinFloat64Vector3D inValue1, LinFloat64Vector3D inValue2, LinFloat64Vector3D outValue1, LinFloat64Vector3D outValue2)
    {
        var valueMap = Float64ScaleTranslateAffineMap3D.CreateFromRanges(
            inValue1, 
            inValue2, 
            outValue1, 
            outValue2
        );

        return baseSignal.MapValueUsing(valueMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapValueRangeTo(this Float64Path3D baseSignal, Triplet<Float64ScalarRange> valueRange)
    {
        return baseSignal.MapValueUsing(
            baseSignal.GetValueRange(),
            valueRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D MapValueRangeTo(this Float64Path3D baseSignal, LinFloat64Vector3D outValue1, LinFloat64Vector3D outValue2)
    {
        var (inValue1, inValue2) =
            baseSignal.GetValueRange().GetMinMaxVector3D();

        return baseSignal.MapValueUsing(
            inValue1,
            inValue2,
            outValue1,
            outValue2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D FlipValueRange(this Float64Path3D baseSignal)
    {
        var (valueMin, valueMax) =
            baseSignal.GetValueRange().GetMinMaxVector3D();

        return baseSignal.MapValueUsing(
            valueMin,
            valueMax,
            valueMax,
            valueMin
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D FlipValueRange(this Float64Path3D baseSignal, LinFloat64Vector3D value1, LinFloat64Vector3D value2)
    {
        return baseSignal.MapValueUsing(
            value1,
            value2,
            value2,
            value1
        );
    }

}