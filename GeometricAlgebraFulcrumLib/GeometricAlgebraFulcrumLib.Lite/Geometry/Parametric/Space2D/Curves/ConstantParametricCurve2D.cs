using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;

public class ConstantParametricCurve2D :
    IParametricCurve2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(IFloat64Vector2D point)
    {
        return new ConstantParametricCurve2D(
            Float64ScalarRange.Infinite,
            point,
            Float64Vector2D.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(Float64ScalarRange parameterRange, IFloat64Vector2D point)
    {
        return new ConstantParametricCurve2D(
            parameterRange,
            point,
            Float64Vector2D.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(double pointX, double pointY)
    {
        return new ConstantParametricCurve2D(
            Float64ScalarRange.Infinite,
            Float64Vector2D.Create(pointX, pointY),
            Float64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(IFloat64Vector2D point, IFloat64Vector2D tangent)
    {
        return new ConstantParametricCurve2D(
            Float64ScalarRange.Infinite,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricCurve2D Create(Float64ScalarRange parameterRange, IFloat64Vector2D point, IFloat64Vector2D tangent)
    {
        return new ConstantParametricCurve2D(
            parameterRange,
            point,
            tangent
        );
    }


    public Float64Vector2D Point { get; }

    public Float64Vector2D Tangent { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricCurve2D(Float64ScalarRange parameterRange, IFloat64Vector2D point, IFloat64Vector2D tangent)
    {
        ParameterRange = parameterRange;
        Point = point.ToVector2D();
        Tangent = tangent.ToVector2D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetPoint(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative1Point(double parameterValue)
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