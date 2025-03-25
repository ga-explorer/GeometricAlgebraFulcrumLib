using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64LineSegmentPath2D :
    Float64ArcLengthPath2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegmentPath2D Finite(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        return new Float64LineSegmentPath2D(false, point1, point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegmentPath2D Periodic(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        return new Float64LineSegmentPath2D(true, point1, point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegmentPath2D Create(bool isPeriodic, ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        return new Float64LineSegmentPath2D(isPeriodic, point1, point2);
    }


    public LinFloat64Vector2D Point1 { get; }

    public LinFloat64Vector2D Point2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64LineSegmentPath2D(bool isPeriodic, ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector2D();
        Point2 = point2.ToLinVector2D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return Point1.GetDistanceToPoint(Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double t)
    {
        return t.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        var curveLength = GetLength();

        return length.ClampPeriodic(curveLength) / curveLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return t.Lerp(Point1, Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
    {
        return IsFinite
            ? this
            : new Float64LineSegmentPath2D(true, Point1, Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
    {
        return IsPeriodic
            ? this
            : new Float64LineSegmentPath2D(false, Point1, Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return Point2 - Point1;
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
            t.ClampPeriodic(1d),
            GetValue(t),
            (Point2 - Point1).ToUnitLinVector2D()
        );
    }
}