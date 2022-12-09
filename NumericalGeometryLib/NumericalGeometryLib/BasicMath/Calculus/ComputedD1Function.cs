using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class ComputedD1Function :
    ComputedD0Function,
    IScalarD1Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD1Function CreateD1Function(Func<double, double> valueFunc)
    {
        return new ComputedD1Function(
            valueFunc,
            MathNet.Numerics.Differentiate.FirstDerivativeFunc(valueFunc)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD1Function CreateD1Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc)
    {
        return new ComputedD1Function(
            valueFunc,
            firstDerivativeFunc
        );
    }


    public Func<double, double> FirstDerivativeFunc { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ComputedD1Function(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc)
        : base(valueFunc)
    {
        FirstDerivativeFunc = firstDerivativeFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFirstDerivativeValue(double t)
    {
        return FirstDerivativeFunc(t);
    }
}