using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Lines;

public class ParametricLine2D :
    IParametricC2Curve2D
{
    public Float64Vector2D Point { get; }

    public Float64Vector2D Vector { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricLine2D(IFloat64Vector2D point, IFloat64Vector2D vector)
    {
        Point = point.ToVector2D();
        Vector = vector.ToVector2D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Vector.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetPoint(double parameterValue)
    {
        return Float64Vector2D.Create(
            Point.X + parameterValue * Vector.X,
            Point.Y + parameterValue * Vector.Y
        );
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
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            GetPoint(parameterValue),
            Vector.ToUnitVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative2Point(double parameterValue)
    {
        return Float64Vector2D.Zero;
    }
}