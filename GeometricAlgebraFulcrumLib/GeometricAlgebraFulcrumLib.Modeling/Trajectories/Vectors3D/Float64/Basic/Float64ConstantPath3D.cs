using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public sealed class Float64ConstantPath3D :
    Float64Path3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath3D Finite(ILinFloat64Vector3D point)
    {
        return new Float64ConstantPath3D(
            Float64ScalarRange.SymmetricOne,
            false,
            point,
            LinFloat64Vector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath3D Finite(Float64ScalarRange timeRange, ILinFloat64Vector3D point)
    {
        return new Float64ConstantPath3D(
            timeRange,
            false,
            point,
            LinFloat64Vector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath3D Finite(double pointX, double pointY, double pointZ)
    {
        return new Float64ConstantPath3D(
            Float64ScalarRange.SymmetricOne,
            false,
            LinFloat64Vector3D.Create(pointX, pointY, pointZ),
            LinFloat64Vector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath3D Finite(ILinFloat64Vector3D point, ILinFloat64Vector3D tangent)
    {
        return new Float64ConstantPath3D(
            Float64ScalarRange.SymmetricOne,
            false,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath3D Finite(Float64ScalarRange timeRange, ILinFloat64Vector3D point, ILinFloat64Vector3D tangent)
    {
        return new Float64ConstantPath3D(
            timeRange,
            false,
            point,
            tangent
        );
    }


    public LinFloat64Vector3D Point { get; }

    public LinFloat64Vector3D Tangent { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ConstantPath3D(Float64ScalarRange timeRange, bool isPeriodic, ILinFloat64Vector3D point, ILinFloat64Vector3D tangent)
        : base(timeRange, isPeriodic)
    {
        Point = point.ToLinVector3D();
        Tangent = tangent.ToLinVector3D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64ConstantPath3D(TimeRange, false, Point, Tangent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64ConstantPath3D(TimeRange, true, Point, Tangent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return LinFloat64Vector3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        return Float64Path3DLocalFrame.Create(
            t,
            Point,
            Tangent
        );
    }
}