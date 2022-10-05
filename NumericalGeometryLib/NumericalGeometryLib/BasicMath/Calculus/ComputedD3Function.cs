using System;
using System.Runtime.CompilerServices;
using MathNet.Numerics;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class ComputedD3Function :
    ComputedD2Function,
    IScalarD3Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD3Function CreateD3Function(Func<double, double> valueFunc)
    {
        return new ComputedD3Function(
            valueFunc,
            Differentiate.FirstDerivativeFunc(valueFunc),
            Differentiate.SecondDerivativeFunc(valueFunc),
            Differentiate.DerivativeFunc(valueFunc, 3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD3Function CreateD3Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc)
    {
        return new ComputedD3Function(
            valueFunc,
            firstDerivativeFunc,
            Differentiate.FirstDerivativeFunc(firstDerivativeFunc),
            Differentiate.SecondDerivativeFunc(firstDerivativeFunc)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD3Function CreateD3Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc)
    {
        return new ComputedD3Function(
            valueFunc,
            firstDerivativeFunc,
            secondDerivativeFunc,
            Differentiate.FirstDerivativeFunc(secondDerivativeFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD3Function CreateD3Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc, Func<double, double> thirdDerivativeFunc)
    {
        return new ComputedD3Function(
            valueFunc,
            firstDerivativeFunc,
            secondDerivativeFunc,
            thirdDerivativeFunc
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD3Function CreateD3Function(ComputedD0Function valueFunc, ComputedD0Function firstDerivativeFunc, ComputedD0Function secondDerivativeFunc, ComputedD0Function thirdDerivativeFunc)
    {
        return new ComputedD3Function(
            valueFunc.GetValue,
            firstDerivativeFunc.GetValue,
            secondDerivativeFunc.GetValue,
            thirdDerivativeFunc.GetValue
        );
    }


    public Func<double, double> ThirdDerivativeFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ComputedD3Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc, Func<double, double> thirdDerivativeFunc)
        : base(valueFunc, firstDerivativeFunc, secondDerivativeFunc)
    {
        ThirdDerivativeFunc = thirdDerivativeFunc;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetThirdDerivative(double t)
    {
        return ThirdDerivativeFunc(t);
    }
}