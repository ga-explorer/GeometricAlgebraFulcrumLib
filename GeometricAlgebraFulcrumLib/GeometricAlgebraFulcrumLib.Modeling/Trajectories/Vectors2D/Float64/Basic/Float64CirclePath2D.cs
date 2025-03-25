using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64CirclePath2D :
    Float64ArcLengthPath2D
{
    private readonly double _directionFactor;

    public bool ReverseDirection { get; }

    public double Radius { get; }

    public LinFloat64Vector2D Center { get; }


    public Float64CirclePath2D(ILinFloat64Vector2D center, double radius, bool reverseDirection = false)
        : base(Float64ScalarRange.ZeroToOne, true)
    {
        if (radius < 0)
            throw new ArgumentException(nameof(radius));

        _directionFactor = Math.Tau;

        if (reverseDirection)
            _directionFactor *= -1;

        ReverseDirection = reverseDirection;
        Radius = radius;
        Center = center.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Radius.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        var angle = t * _directionFactor;

        return LinFloat64Vector2D.Create((Float64Scalar)(Radius * Math.Cos(angle)),
            (Float64Scalar)(Radius * Math.Sin(angle)));
    }

    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        var angle = t * _directionFactor;
        var magnitude = Radius * _directionFactor;

        return LinFloat64Vector2D.Create((Float64Scalar)(-magnitude * Math.Sin(angle)),
            (Float64Scalar)(magnitude * Math.Cos(angle)));
    }

    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        var angle = t * _directionFactor;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        var point = LinFloat64Vector2D.Create((Float64Scalar)(Radius * cosAngle), (Float64Scalar)(Radius * sinAngle));
        var tangent = LinFloat64Vector2D.Create((Float64Scalar)(-sinAngle), (Float64Scalar)cosAngle);

        return Float64Path2DLocalFrame.Create(
            t,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        var angle = t * _directionFactor;
        var magnitude = Radius * _directionFactor * _directionFactor;

        return LinFloat64Vector2D.Create((Float64Scalar)(-magnitude * Math.Cos(angle)),
            (Float64Scalar)(-magnitude * Math.Sin(angle)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return Math.Tau * Radius;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double t)
    {
        return t.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        var maxLength = GetLength();

        return length.ClampPeriodic(maxLength) / maxLength;
    }
}