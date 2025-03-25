using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Circles;

public sealed class Float64YzCirclePath3D :
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
            ? LinFloat64Vector3D.NegativeE1
            : LinFloat64Vector3D.E1;


    public Float64YzCirclePath3D(double radius, int rotationCount = 1)
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
    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;

        return LinFloat64Vector3D.Create(
            0,
            Radius * Math.Cos(angle),
            Radius * Math.Sin(angle)
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
    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor;

        return LinFloat64Vector3D.Create(
            0d,
            -magnitude * Math.Sin(angle),
            magnitude * Math.Cos(angle)
        );
    }

    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        var point = LinFloat64Vector3D.Create(0d, Radius * cosAngle, Radius * sinAngle);
        var normal1 = LinFloat64Vector3D.Create(0d, -cosAngle, -sinAngle);
        var normal2 = LinFloat64Vector3D.E1;
        var tangent = LinFloat64Vector3D.Create(0d, -sinAngle, cosAngle);

        return Float64Path3DLocalFrame.Create(
            parameterValue,
            point,
            tangent,
            normal1,
            normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor * _directionFactor;

        return LinFloat64Vector3D.Create(
            0d,
            -magnitude * Math.Cos(angle),
            -magnitude * Math.Sin(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return Math.Tau * Radius;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double parameterValue)
    {
        return parameterValue.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        var maxLength = GetLength();

        return length.ClampPeriodic(maxLength) / maxLength;
    }
}