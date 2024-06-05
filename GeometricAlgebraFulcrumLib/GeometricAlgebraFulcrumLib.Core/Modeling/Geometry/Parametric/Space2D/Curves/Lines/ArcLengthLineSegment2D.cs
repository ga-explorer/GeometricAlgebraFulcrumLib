using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Lines;

public class ArcLengthLineSegment2D :
    IParametricC2Curve2D,
    IArcLengthCurve2D
{
    public LinFloat64Vector2D Point1 { get; }

    public LinFloat64Vector2D Point2 { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.ZeroToOne;
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArcLengthLineSegment2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        Point1 = point1.ToLinVector2D();
        Point2 = point2.ToLinVector2D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetLength()
    {
        return Point1.GetDistanceToPoint(Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ParameterToLength(double parameterValue)
    {
        return parameterValue.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar LengthToParameter(double length)
    {
        var curveLength = GetLength();

        return length.ClampPeriodic(curveLength) / curveLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        return parameterValue.Lerp(Point1, Point2);
    }

    public LinFloat64Vector2D GetTangent(double parameterValue)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector2D GetUnitTangent(double parameterValue)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        return Point2 - Point1;
    }

    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame2D.Create(
            parameterValue.ClampPeriodic(1d),
            GetPoint(parameterValue),
            (Point2 - Point1).ToUnitLinVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative2Point(double parameterValue)
    {
        return LinFloat64Vector2D.Zero;
    }
}