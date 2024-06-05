using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus;

public static class ScalarFunctionFactory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FnSin<T> SinFn<T>(this IScalarFunctionProcessor<T> processor, T magnitude, T frequency)
    {
        return FnSin<T>.Create(processor, magnitude, frequency);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FnSin<T> SinFn<T>(this IScalarFunctionProcessor<T> processor, T magnitude, T frequency, T phase)
    {
        return FnSin<T>.Create(processor, magnitude, frequency, phase);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FnCos<T> CosFn<T>(this IScalarFunctionProcessor<T> processor, T magnitude, T frequency)
    {
        return FnCos<T>.Create(processor, magnitude, frequency);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FnCos<T> CosFn<T>(this IScalarFunctionProcessor<T> processor, T magnitude, T frequency, T phase)
    {
        return FnCos<T>.Create(processor, magnitude, frequency, phase);
    }
}