using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public sealed class Float64Bezier1Path2D :
    Float64Path2D
{
    public LinFloat64Vector2D Point1 { get; }

    public LinFloat64Vector2D Point2 { get; }


    public Float64Bezier1Path2D(bool isPeriodic, ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector2D();
        Point2 = point2.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bezier0Path2D GetDerivativeCurve()
    {
        return Float64Bezier0Path2D.Finite(Point2 - Point1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        var (p1, p2) = t.BernsteinBasis_1();

        return LinFloat64Vector2D.Create(p1 * Point1.X + p2 * Point2.X,
            p1 * Point1.Y + p2 * Point2.Y);
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
        return Point2 - Point1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return LinFloat64Vector2D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path2DLocalFrame GetFrame(double t)
    {
        return Float64Path2DLocalFrame.Create(
            t,
            GetValue(t),
            GetDerivative1Value(t)
        );
    }
}