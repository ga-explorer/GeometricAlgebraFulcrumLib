using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Lines;

public class ParametricLine2D :
    IParametricC2Curve2D
{
    public LinFloat64Vector2D Point { get; }

    public LinFloat64Vector2D Vector { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricLine2D(ILinFloat64Vector2D point, ILinFloat64Vector2D vector)
    {
        Point = point.ToLinVector2D();
        Vector = vector.ToLinVector2D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Vector.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        return LinFloat64Vector2D.Create(
            Point.X + parameterValue * Vector.X,
            Point.Y + parameterValue * Vector.Y
        );
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
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            GetPoint(parameterValue),
            Vector.ToUnitLinVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative2Point(double parameterValue)
    {
        return LinFloat64Vector2D.Zero;
    }
}