using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Bezier;

public class BezierCurve1Degree2D :
    IParametricCurve2D
{
    public Float64Vector2D Point1 { get; }

    public Float64Vector2D Point2 { get; }
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;


    public BezierCurve1Degree2D(IFloat64Vector2D point1, IFloat64Vector2D point2)
    {
        Point1 = point1.ToVector2D();
        Point2 = point2.ToVector2D();

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
    public Float64Vector2D GetPoint(double parameterValue)
    {
        var (p1, p2) = parameterValue.BernsteinBasis_1();

        return Float64Vector2D.Create(p1 * Point1.X + p2 * Point2.X,
            p1 * Point1.Y + p2 * Point2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative1Point(double parameterValue)
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