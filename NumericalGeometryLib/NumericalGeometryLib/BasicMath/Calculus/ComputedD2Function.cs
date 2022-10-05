using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class ComputedD2Function :
    ComputedD1Function,
    IScalarD2Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD2Function CreateD2Function(Func<double, double> valueFunc)
    {
        return new ComputedD2Function(
            valueFunc,
            MathNet.Numerics.Differentiate.FirstDerivativeFunc(valueFunc),
            MathNet.Numerics.Differentiate.SecondDerivativeFunc(valueFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD2Function CreateD2Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc)
    {
        return new ComputedD2Function(
            valueFunc,
            firstDerivativeFunc,
            MathNet.Numerics.Differentiate.FirstDerivativeFunc(firstDerivativeFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD2Function CreateD2Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc)
    {
        return new ComputedD2Function(
            valueFunc,
            firstDerivativeFunc,
            secondDerivativeFunc
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD2Function CreateD2Function(ComputedD0Function valueFunc, ComputedD0Function firstDerivativeFunc, ComputedD0Function secondDerivativeFunc)
    {
        return new ComputedD2Function(
            valueFunc.GetValue,
            firstDerivativeFunc.GetValue,
            secondDerivativeFunc.GetValue
        );
    }


    public Func<double, double> SecondDerivativeFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ComputedD2Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc)
        : base(valueFunc, firstDerivativeFunc)
    {
        SecondDerivativeFunc = secondDerivativeFunc;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSecondDerivative(double t)
    {
        return SecondDerivativeFunc(t);
    }
}