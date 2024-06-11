using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves.Bezier;

public class BezierCurve1Degree2D :
    IFloat64ParametricCurve2D
{
    public LinFloat64Vector2D Point1 { get; }

    public LinFloat64Vector2D Point2 { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    public BezierCurve1Degree2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        Point1 = point1.ToLinVector2D();
        Point2 = point2.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BezierCurve0Degree2D GetDerivativeCurve()
    {
        return new BezierCurve0Degree2D(Point2 - Point1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        var (p1, p2) = parameterValue.BernsteinBasis_1();

        return LinFloat64Vector2D.Create(p1 * Point1.X + p2 * Point2.X,
            p1 * Point1.Y + p2 * Point2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        return Point2 - Point1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            GetPoint(parameterValue),
            GetDerivative1Point(parameterValue)
        );
    }
}