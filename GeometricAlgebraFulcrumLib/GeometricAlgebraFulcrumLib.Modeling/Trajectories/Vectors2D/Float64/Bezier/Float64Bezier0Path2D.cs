using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public sealed class Float64Bezier0Path2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier0Path2D Finite(ILinFloat64Vector2D point1)
    {
        return new Float64Bezier0Path2D(false, point1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier0Path2D Periodic(ILinFloat64Vector2D point1)
    {
        return new Float64Bezier0Path2D(true, point1);
    }


    public LinFloat64Vector2D Point1 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bezier0Path2D(bool isPeriodic, ILinFloat64Vector2D point1)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return Point1;
    }

    public override Float64Path2D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path2D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return LinFloat64Vector2D.Zero;
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
            Point1,
            LinFloat64Vector2D.Zero
        );
    }
}