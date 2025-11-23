using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors4D.Float64;

public static class Float64Path4DUtils
{
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64AffineFrame4D ToAffineFrame(this IFloat64Path4DLocalFrame frame)
    //{
    //    return LinFloat64AffineFrame4D.Create(
    //        frame.Point,
    //        frame.Tangent,
    //        frame.Normal1,
    //        frame.Normal2
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Float64Path4DLocalFrame ToLocalCurveFrame(this LinFloat64AffineFrame4D frame, double t)
    //{
    //    return Float64Path4DLocalFrame.CreateFromAffineFrame(t, frame);
    //}

    //public static LinFloat64Quaternion FrameToFrameRotationQuaternion(this Float64Path4DLocalFrame frame1, Float64Path4DLocalFrame frame2)
    //{
    //    var q1 =
    //        frame1
    //            .Tangent
    //            .ToUnitLinVector4D()
    //            .VectorToVectorRotationQuaternion(frame2.Tangent.ToUnitLinVector4D());

    //    Debug.Assert(
    //        LinFloat64Vector4DUtils.L2NormSquared((q1.RotateVector(frame1.Tangent) - frame2.Tangent)).IsNearZero(1e-7)
    //    );

    //    var f1 =
    //        frame1.GetRotatedFrameUsingQuaternion(q1);

    //    var dot12 =
    //        q1
    //            .RotateVector(frame1.Normal1)
    //            .ToUnitLinVector4D()
    //            .VectorDot(frame2.Normal1.ToUnitLinVector4D());

    //    var q2 =
    //        q1
    //            .RotateVector(frame1.Normal1)
    //            .ToUnitLinVector4D()
    //            .VectorToVectorRotationQuaternion(frame2.Normal1.ToUnitLinVector4D(), frame2.Tangent);

    //    var quaternion = q2.Concatenate(q1);

    //    var f2 =
    //        f1.GetRotatedFrameUsingQuaternion(q2);

    //    Debug.Assert(
    //        LinFloat64Vector4DUtils.L2NormSquared((quaternion.RotateVector(frame1.Tangent) - frame2.Tangent)).IsNearZero(1e-7)
    //    );

    //    Debug.Assert(
    //        LinFloat64Vector4DUtils.L2NormSquared((quaternion.RotateVector(frame1.Normal1) - frame2.Normal1)).IsNearZero(1e-7)
    //    );

    //    Debug.Assert(
    //        LinFloat64Vector4DUtils.L2NormSquared((quaternion.RotateVector(frame1.Normal2) - frame2.Normal2)).IsNearZero(1e-7)
    //    );

    //    return quaternion;
    //}

    //public static double ComputePathLength(this IEnumerable<Float64Path4DLocalFrame> framesList)
    //{
    //    var arcLength = 0d;

    //    Float64Path4DLocalFrame? frame1 = null;
    //    var firstFrame = true;
    //    foreach (var frame2 in framesList)
    //    {
    //        if (firstFrame)
    //        {
    //            frame1 = frame2;
    //            firstFrame = false;
    //            continue;
    //        }

    //        arcLength += frame2.Point.GetDistanceToPoint(frame1?.Point ?? frame2.Point);

    //        frame1 = frame2;
    //    }

    //    return arcLength;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<double> GetTimeValues(this IEnumerable<Float64Path4DLocalFrame> path)
    //{
    //    return path.Select(f => f.TimeValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<LinFloat64Vector4D> GetPoints(this IEnumerable<Float64Path4DLocalFrame> path)
    //{
    //    return path.Select(f => f.Point);
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<LinFloat64Vector4D> GetTangents(this IEnumerable<Float64Path4DLocalFrame> path)
    //{
    //    return path.Select(f => f.Tangent);
    //}
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D GetMinVector4D(this IQuad<Float64ScalarRange> range)
    {
        return LinFloat64Vector4D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue,
            range.Item3.MinValue,
            range.Item4.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D GetMidVector4D(this IQuad<Float64ScalarRange> range)
    {
        return LinFloat64Vector4D.Create(
            range.Item1.MidValue,
            range.Item2.MidValue,
            range.Item3.MidValue,
            range.Item4.MidValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D GetMaxVector4D(this IQuad<Float64ScalarRange> range)
    {
        return LinFloat64Vector4D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue,
            range.Item3.MaxValue,
            range.Item4.MaxValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector4D> GetMinMaxVector4D(this IQuad<Float64ScalarRange> range)
    {
        var minVector = LinFloat64Vector4D.Create(
            range.Item1.MinValue,
            range.Item2.MinValue,
            range.Item3.MinValue,
            range.Item4.MinValue
        );

        var maxVector = LinFloat64Vector4D.Create(
            range.Item1.MaxValue,
            range.Item2.MaxValue,
            range.Item3.MaxValue,
            range.Item4.MaxValue
        );

        return new Pair<LinFloat64Vector4D>(minVector, maxVector);
    }


}