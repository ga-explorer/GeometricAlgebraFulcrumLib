using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

public class ComputedParametricScalar :
    IParametricScalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Create(Func<double, double> xFunc)
    {
        return new ComputedParametricScalar(
            Float64ScalarRange.Infinite,
            xFunc, 
            Differentiate.FirstDerivativeFunc(xFunc)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Create(Float64ScalarRange parameterRange, Func<double, double> xFunc)
    {
        return new ComputedParametricScalar(
            parameterRange,
            xFunc, 
            Differentiate.FirstDerivativeFunc(xFunc)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Create(Float64ScalarRange parameterRange, Func<double, double> getPointFunc, Func<double, double> getTangentFunc)
    {
        return new ComputedParametricScalar(
            parameterRange,
            getPointFunc, 
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Create(Func<double, double> getPointFunc, Func<double, double> getTangentFunc)
    {
        return new ComputedParametricScalar(
            Float64ScalarRange.Infinite,
            getPointFunc, 
            getTangentFunc
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Create(DifferentialFunction xFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();

        return new ComputedParametricScalar(
            Float64ScalarRange.Infinite,
            xFunc.GetValue, 
            xDtFunc.GetValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Create(Float64ScalarRange parameterRange, DifferentialFunction xFunc)
    {
        var xDtFunc = xFunc.GetDerivative1();

        return new ComputedParametricScalar(
            parameterRange,
            xFunc.GetValue, 
            xDtFunc.GetValue
        );
    }

        
    public Float64ScalarRange ParameterRange { get; }

    public Func<double, double> GetPointFunc { get; }

    public Func<double, double> GetTangentFunc { get; }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricScalar(Float64ScalarRange parameterRange, Func<double, double> getPointFunc, Func<double, double> getTangentFunc)
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
    public Float64Scalar GetValue(double parameterValue)
    {
        return GetPointFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricScalarLocalFrame GetFrame(double parameterValue)
    {
        return ParametricScalarLocalFrame.Create(
            parameterValue,
            GetValue(parameterValue),
            GetDerivative1Value(parameterValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar Add(double value)
    {
        return new ComputedParametricScalar(
            ParameterRange,
            t => GetPointFunc(t) + value,
            GetTangentFunc
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar Subtract(double value)
    {
        return new ComputedParametricScalar(
            ParameterRange,
            t => GetPointFunc(t) - value,
            GetTangentFunc
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar Times(double value)
    {
        return new ComputedParametricScalar(
            ParameterRange,
            t => GetPointFunc(t) * value,
            t => GetTangentFunc(t) * value
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar Divide(double value)
    {
        return new ComputedParametricScalar(
            ParameterRange,
            t => GetPointFunc(t) / value,
            t => GetTangentFunc(t) / value
        );
    }
}