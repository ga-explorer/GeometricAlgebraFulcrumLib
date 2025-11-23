using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

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
        return Float64Path3DLocalFrame.CreateFromAffineFrame(t, frame);
    }

    public static LinFloat64Quaternion FrameToFrameRotationQuaternion(this Float64Path3DLocalFrame frame1, Float64Path3DLocalFrame frame2)
    {
        var q1 =
            frame1
                .Tangent
                .ToUnitLinVector3D()
                .VectorToVectorRotationQuaternion(frame2.Tangent.ToUnitLinVector3D());

        Debug.Assert(
            LinFloat64Vector3DUtils.L2NormSquared((q1.RotateVector(frame1.Tangent) - frame2.Tangent)).IsNearZero(1e-7)
        );

        var f1 =
            frame1.GetRotatedFrameUsingQuaternion(q1);

        var dot12 =
            q1
                .RotateVector(frame1.Normal1)
                .ToUnitLinVector3D()
                .VectorDot(frame2.Normal1.ToUnitLinVector3D());

        var q2 =
            q1
                .RotateVector(frame1.Normal1)
                .ToUnitLinVector3D()
                .VectorToVectorRotationQuaternion(frame2.Normal1.ToUnitLinVector3D(), frame2.Tangent);

        var quaternion = q2.Concatenate(q1);

        var f2 =
            f1.GetRotatedFrameUsingQuaternion(q2);

        Debug.Assert(
            LinFloat64Vector3DUtils.L2NormSquared((quaternion.RotateVector(frame1.Tangent) - frame2.Tangent)).IsNearZero(1e-7)
        );

        Debug.Assert(
            LinFloat64Vector3DUtils.L2NormSquared((quaternion.RotateVector(frame1.Normal1) - frame2.Normal1)).IsNearZero(1e-7)
        );

        Debug.Assert(
            LinFloat64Vector3DUtils.L2NormSquared((quaternion.RotateVector(frame1.Normal2) - frame2.Normal2)).IsNearZero(1e-7)
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
    public static IEnumerable<LinFloat64Vector3D> GetTangents(this IEnumerable<Float64Path3DLocalFrame> path)
    {
        return path.Select(f => f.Tangent);
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


}