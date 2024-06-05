using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves.Circles;

public class ParametricCircleZx3D :
    IGraphicsParametricCircle3D
{
    private readonly double _directionFactor;
        
    public bool ReverseDirection 
        => RotationCount < 0;

    public int RotationCount { get; }

    public double Radius { get; }

    public LinFloat64Vector3D Center
        => LinFloat64Vector3D.Zero;

    public LinFloat64Vector3D UnitNormal
        => ReverseDirection 
            ? LinFloat64Vector3D.NegativeE2 
            : LinFloat64Vector3D.E2;

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.ZeroToOne;
        

    public ParametricCircleZx3D(double radius, int rotationCount = 1)
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
    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;

        return LinFloat64Vector3D.Create(Radius * Math.Sin(angle),
            0,
            Radius * Math.Cos(angle));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor;

        return LinFloat64Vector3D.Create(magnitude * Math.Cos(angle),
            0,
            -magnitude * Math.Sin(angle));
    }

    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        var point = LinFloat64Vector3D.Create(Radius * sinAngle, 0d, Radius * cosAngle);
        var normal1 = LinFloat64Vector3D.Create(-sinAngle, 0d, -cosAngle);
        var normal2 = LinFloat64Vector3D.E2;
        var tangent = LinFloat64Vector3D.Create(cosAngle, 0d, -sinAngle);

        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            point,
            tangent,
            normal1,
            normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative2Point(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor * _directionFactor;

        return LinFloat64Vector3D.Create(-magnitude * Math.Sin(angle),
            0,
            -magnitude * Math.Cos(angle));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetLength()
    {
        return 2d * Math.PI * Radius;
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