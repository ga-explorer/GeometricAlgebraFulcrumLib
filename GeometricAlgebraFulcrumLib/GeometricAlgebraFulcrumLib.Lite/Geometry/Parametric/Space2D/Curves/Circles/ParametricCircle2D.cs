using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Circles;

public class ParametricCircle2D :
    IGraphicsParametricCircle2D
{
    private readonly double _directionFactor;

    public bool ReverseDirection { get; }

    public double Radius { get; }

    public Float64Vector2D Center { get; }
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.ZeroToOne;
        

    public ParametricCircle2D(IFloat64Vector2D center, double radius, bool reverseDirection = false)
    {
        if (radius < 0)
            throw new ArgumentException(nameof(radius));

        _directionFactor = 2 * Math.PI;

        if (reverseDirection)
            _directionFactor *= -1;

        ReverseDirection = reverseDirection;
        Radius = radius;
        Center = center.ToVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Radius.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetPoint(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;

        return Float64Vector2D.Create((Float64Scalar)(Radius * Math.Cos(angle)),
            (Float64Scalar)(Radius * Math.Sin(angle)));
    }

    public Float64Vector2D GetTangent(double parameterValue)
    {
        throw new NotImplementedException();
    }

    public Float64Vector2D GetUnitTangent(double parameterValue)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative1Point(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor;

        return Float64Vector2D.Create((Float64Scalar)(-magnitude * Math.Sin(angle)),
            (Float64Scalar)(magnitude * Math.Cos(angle)));
    }

    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        var point = Float64Vector2D.Create((Float64Scalar)(Radius * cosAngle), (Float64Scalar)(Radius * sinAngle));
        var tangent = Float64Vector2D.Create((Float64Scalar)(-sinAngle), (Float64Scalar)cosAngle);

        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative2Point(double parameterValue)
    {
        var angle = parameterValue * _directionFactor;
        var magnitude = Radius * _directionFactor * _directionFactor;

        return Float64Vector2D.Create((Float64Scalar)(-magnitude * Math.Cos(angle)),
            (Float64Scalar)(-magnitude * Math.Sin(angle)));
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