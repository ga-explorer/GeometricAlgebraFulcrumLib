using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;

public class ConstantParametricCurve2D :
    IFloat64ParametricCurve2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(ILinFloat64Vector2D point)
    {
        return new ConstantParametricCurve2D(
            Float64ScalarRange.Infinite,
            point,
            LinFloat64Vector2D.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(Float64ScalarRange parameterRange, ILinFloat64Vector2D point)
    {
        return new ConstantParametricCurve2D(
            parameterRange,
            point,
            LinFloat64Vector2D.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(double pointX, double pointY)
    {
        return new ConstantParametricCurve2D(
            Float64ScalarRange.Infinite,
            LinFloat64Vector2D.Create(pointX, pointY),
            LinFloat64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        return new ConstantParametricCurve2D(
            Float64ScalarRange.Infinite,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(Float64ScalarRange parameterRange, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        return new ConstantParametricCurve2D(
            parameterRange,
            point,
            tangent
        );
    }


    public LinFloat64Vector2D Point { get; }

    public LinFloat64Vector2D Tangent { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricCurve2D(Float64ScalarRange parameterRange, ILinFloat64Vector2D point, ILinFloat64Vector2D tangent)
    {
        ParameterRange = parameterRange;
        Point = point.ToLinVector2D();
        Tangent = tangent.ToLinVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame2D.Create(
            parameterValue,
            Point,
            Tangent
        );
    }
}