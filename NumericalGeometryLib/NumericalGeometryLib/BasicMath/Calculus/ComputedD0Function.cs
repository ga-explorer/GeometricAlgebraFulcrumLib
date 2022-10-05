using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Calculus;

public class ComputedD0Function :
    IScalarD0Function
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedD0Function CreateD0Function(Func<double, double> valueFunc)
    {
        return new ComputedD0Function(valueFunc);
    }


    public Func<double, double> ValueFunc { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ComputedD0Function(Func<double, double> valueFunc)
    {
        ValueFunc = valueFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double t)
    {
        return ValueFunc(t);
    }
}