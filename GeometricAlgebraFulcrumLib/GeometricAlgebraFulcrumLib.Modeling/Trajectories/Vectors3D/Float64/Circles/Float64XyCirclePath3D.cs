using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Circles;

public sealed class Float64XyCirclePath3D :
    Float64AxisAlignedCirclePath3D
{
    private readonly double _directionFactor;

    public bool ReverseDirection
        => RotationCount < 0;

    public override int RotationCount { get; }

    public override double Radius { get; }

    public override LinFloat64Vector3D Center
        => LinFloat64Vector3D.Zero;

    public override LinFloat64Vector3D UnitNormal
        => ReverseDirection
            ? LinFloat64Vector3D.NegativeE3
            : LinFloat64Vector3D.E3;


    public Float64XyCirclePath3D(double radius, int rotationCount = 1)
        : base(Float64ScalarRange.ZeroToOne, true)
    {
        if (radius < 0)
            throw new ArgumentException(nameof(radius));

        if (rotationCount == 0 || rotationCount > 100 || rotationCount < -100)
            throw new ArgumentException(nameof(rotationCount));

        _directionFactor = Math.Tau * rotationCount;

        RotationCount = rotationCount;
        Radius = radius;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Radius.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        var angle = t * _directionFactor;

        return LinFloat64Vector3D.Create(
            Radius * Math.Cos(angle),
            Radius * Math.Sin(angle),
            0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        var angle = t * _directionFactor;
        var magnitude = Radius * _directionFactor;

        return LinFloat64Vector3D.Create(
            -magnitude * Math.Sin(angle),
            magnitude * Math.Cos(angle),
            0
        );
    }

    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        var angle = t * _directionFactor;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        var point = LinFloat64Vector3D.Create(Radius * cosAngle, Radius * sinAngle, 0d);
        var normal1 = LinFloat64Vector3D.Create(-cosAngle, -sinAngle, 0d);
        var normal2 = LinFloat64Vector3D.E3;
        var tangent = LinFloat64Vector3D.Create(-sinAngle, cosAngle, 0d);

        return Float64Path3DLocalFrame.Create(
            t,
            point,
            tangent,
            normal1,
            normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        var angle = t * _directionFactor;
        var magnitude = Radius * _directionFactor * _directionFactor;

        return LinFloat64Vector3D.Create(-magnitude * Math.Cos(angle),
            -magnitude * Math.Sin(angle),
            0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return Math.Abs(Math.Tau * Radius * RotationCount);
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