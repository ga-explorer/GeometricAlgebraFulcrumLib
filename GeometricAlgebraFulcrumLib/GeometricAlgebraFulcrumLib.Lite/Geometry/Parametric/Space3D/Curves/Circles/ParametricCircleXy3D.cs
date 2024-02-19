using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Circles;

public class ParametricCircleXy3D :
    IGraphicsParametricCircle3D
{
    private readonly double _directionFactor;

    public bool ReverseDirection 
        => RotationCount < 0;

    public int RotationCount { get; }

    public double Radius { get; }

    public Float64Vector3D Center
        => Float64Vector3D.Zero;

    public Float64Vector3D UnitNormal
        => ReverseDirection 
            ? Float64Vector3D.NegativeE3 
            : Float64Vector3D.E3;

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.ZeroToOne;
        

    public ParametricCircleXy3D(double radius, int rotationCount = 1)
    {
        if (radius < 0)
            throw new ArgumentException(nameof(radius));

        if (rotationCount == 0 || rotationCount > 100 || rotationCount < -100)
            throw new ArgumentException(nameof(rotationCount));

        _directionFactor = 2 * Math.PI * rotationCount;
            
        RotationCount = rotationCount;
        Radius = radius;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Radius.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetPoint(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;

        return Float64Vector3D.Create(Radius * Math.Cos(angle),
            Radius * Math.Sin(angle),
            0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetDerivative1Point(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor;

        return Float64Vector3D.Create(-magnitude * Math.Sin(angle),
            magnitude * Math.Cos(angle),
            0);
    }

    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        var point = Float64Vector3D.Create(Radius * cosAngle, Radius * sinAngle, 0d);
        var normal1 = Float64Vector3D.Create(-cosAngle, -sinAngle, 0d);
        var normal2 = Float64Vector3D.E3;
        var tangent = Float64Vector3D.Create(-sinAngle, cosAngle, 0d);

        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            point,
            tangent,
            normal1,
            normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetDerivative2Point(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor * _directionFactor;

        return Float64Vector3D.Create(-magnitude * Math.Cos(angle),
            -magnitude * Math.Sin(angle),
            0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetLength()
    {
        return Math.Abs(2d * Math.PI * Radius * RotationCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ParameterToLength(double parameterValue)
    {
        return parameterValue.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar LengthToParameter(double length)
    {
        var maxLength = GetLength();

        return length.ClampPeriodic(maxLength) / maxLength;
    }
}