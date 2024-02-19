using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;

public class ComputedParametricCurve3D :
    IParametricCurve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Func<double, Float64Vector3D> getPointFunc)
    {
        return new ComputedParametricCurve3D(
            Float64ScalarRange.Infinite, 
            getPointFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Float64ScalarRange parameterRange, Func<double, Float64Vector3D> getPointFunc)
    {
        return new ComputedParametricCurve3D(
            parameterRange, 
            getPointFunc
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Func<double, Float64Vector3D> getPointFunc, Func<double, Float64Vector3D> getTangentFunc)
    {
        return new ComputedParametricCurve3D(
            Float64ScalarRange.Infinite, 
            getPointFunc, 
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Float64ScalarRange parameterRange, Func<double, Float64Vector3D> getPointFunc, Func<double, Float64Vector3D> getTangentFunc)
    {
        return new ComputedParametricCurve3D(
            parameterRange, 
            getPointFunc, 
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
    {
        return Create(
            Float64ScalarRange.Infinite, 
            xFunc, 
            yFunc,
            zFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Float64ScalarRange parameterRange, DifferentialFunction xFunc, DifferentialFunction yFunc, DifferentialFunction zFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();
        var yDtFunc = yFunc.GetDerivative1();
        var zDtFunc = zFunc.GetDerivative1();
            
        return new ComputedParametricCurve3D(
            parameterRange, 
            t => 
                Float64Vector3D.Create(
                    xFunc.GetValue(t), 
                    yFunc.GetValue(t), 
                    zFunc.GetValue(t)
                ),
            t => 
                Float64Vector3D.Create(
                    xDtFunc.GetValue(t), 
                    yDtFunc.GetValue(t), 
                    zDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
    {
        return Create(
            Float64ScalarRange.Infinite, 
            xFunc, 
            yFunc, 
            zFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricCurve3D Create(Float64ScalarRange parameterRange, Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
    {
        return new ComputedParametricCurve3D(
            parameterRange, 
            t => 
                Float64Vector3D.Create(
                    xFunc(t), 
                    yFunc(t), 
                    zFunc(t)
                ),
            t => 
                Float64Vector3D.Create(
                    Differentiate.FirstDerivative(xFunc, t),
                    Differentiate.FirstDerivative(yFunc, t),
                    Differentiate.FirstDerivative(zFunc, t)
                )
        );
    }


    public Float64ScalarRange ParameterRange { get; }
        
    public Func<double, Float64Vector3D> GetPointFunc { get; }

    public Func<double, Float64Vector3D> GetTangentFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricCurve3D(Float64ScalarRange parameterRange, Func<double, Float64Vector3D> getPointFunc)
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
    private ComputedParametricCurve3D(Float64ScalarRange parameterRange, Func<double, Float64Vector3D> getPointFunc, Func<double, Float64Vector3D> getTangentFunc)
    {
        ParameterRange = parameterRange;
        GetPointFunc = getPointFunc;
        GetTangentFunc = getTangentFunc;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetPoint(double parameterValue)
    {
        return GetPointFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetDerivative1Point(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.Create(
            parameterValue,
            GetPoint(parameterValue),
            GetDerivative1Point(parameterValue)
        );
    }
}