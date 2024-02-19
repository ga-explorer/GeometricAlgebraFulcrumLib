using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;

public class ComputedParametricCurve2D :
    IParametricCurve2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Func<double, Float64Vector2D> getPointFunc)
    {
        return new ComputedParametricCurve2D(Float64ScalarRange.Infinite, getPointFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Float64ScalarRange parameterRange, Func<double, Float64Vector2D> getPointFunc)
    {
        return new ComputedParametricCurve2D(
            parameterRange, 
            getPointFunc
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Func<double, Float64Vector2D> getPointFunc, Func<double, Float64Vector2D> getTangentFunc)
    {
        return new ComputedParametricCurve2D(
            Float64ScalarRange.Infinite, 
            getPointFunc, 
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Float64ScalarRange parameterRange, Func<double, Float64Vector2D> getPointFunc, Func<double, Float64Vector2D> getTangentFunc)
    {
        return new ComputedParametricCurve2D(parameterRange, getPointFunc, getTangentFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(DifferentialFunction xFunc, DifferentialFunction yFunc)
    {
        return Create(Float64ScalarRange.Infinite, xFunc, yFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Float64ScalarRange parameterRange, DifferentialFunction xFunc, DifferentialFunction yFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
            
        return new ComputedParametricCurve2D(
            parameterRange, 
            t => 
                Float64Vector2D.Create(
                    xFunc.GetValue(t), 
                    yFunc.GetValue(t)
                ),
            t => 
                Float64Vector2D.Create(
                    xDtFunc.GetValue(t), 
                    yDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Func<double, double> xFunc, Func<double, double> yFunc)
    {
        return Create(
            Float64ScalarRange.Infinite, 
            xFunc, 
            yFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve2D Create(Float64ScalarRange parameterRange, Func<double, double> xFunc, Func<double, double> yFunc)
    {
        return new ComputedParametricCurve2D(
            parameterRange, 
            t => 
                Float64Vector2D.Create(
                    xFunc(t), 
                    yFunc(t)
                ),
            t => 
                Float64Vector2D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t)
                )
        );
    }


    public Float64ScalarRange ParameterRange { get; }
        
    public Func<double, Float64Vector2D> GetPointFunc { get; }

    public Func<double, Float64Vector2D> GetTangentFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricCurve2D(Float64ScalarRange parameterRange, Func<double, Float64Vector2D> getPointFunc)
    {
        ParameterRange = parameterRange;
        GetPointFunc = getPointFunc;
        GetTangentFunc = 
            t =>
            {
                const double epsilon = 1e-7;

                var p1 = getPointFunc(t - epsilon);
                var p2 = getPointFunc(t + epsilon);

                return (p2 - p1) / (2 * epsilon);
            };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricCurve2D(Float64ScalarRange parameterRange, Func<double, Float64Vector2D> getPointFunc, Func<double, Float64Vector2D> getTangentFunc)
    {
        ParameterRange = parameterRange;
        GetPointFunc = getPointFunc;
        GetTangentFunc = getTangentFunc;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetPoint(double parameterValue)
    {
        return GetPointFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetDerivative1Point(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
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