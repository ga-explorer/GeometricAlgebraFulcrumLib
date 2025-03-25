using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64ConstantPath2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath2D Finite(ILinFloat64Vector2D point)
    {
        return new Float64ConstantPath2D(
            Float64ScalarRange.SymmetricOne,
            false,
            point,
            LinFloat64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath2D Finite(Float64ScalarRange timeRange, ILinFloat64Vector2D point)
    {
        return new Float64ConstantPath2D(
            timeRange,
            false,
            point,
            LinFloat64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath2D Finite(double pointX, double pointY)
    {
        return new Float64ConstantPath2D(
            Float64ScalarRange.SymmetricOne,
            false,
            LinFloat64Vector2D.Create(pointX, pointY),
            LinFloat64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath2D Finite(ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        return new Float64ConstantPath2D(
            Float64ScalarRange.SymmetricOne,
            false,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ConstantPath2D Finite(Float64ScalarRange timeRange, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        return new Float64ConstantPath2D(
            timeRange,
            false,
            point,
            tangent
        );
    }


    public LinFloat64Vector2D Point { get; }

    public LinFloat64Vector2D Tangent { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ConstantPath2D(Float64ScalarRange timeRange, bool isPeriodic, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
        : base(timeRange, isPeriodic)
    {
        Point = point.ToLinVector2D();
        Tangent = tangent.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64ConstantPath2D(TimeRange, false, Point, Tangent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64ConstantPath2D(TimeRange, true, Point, Tangent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return LinFloat64Vector2D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        return Float64Path2DLocalFrame.Create(
            t,
            Point,
            Tangent
        );
    }
}