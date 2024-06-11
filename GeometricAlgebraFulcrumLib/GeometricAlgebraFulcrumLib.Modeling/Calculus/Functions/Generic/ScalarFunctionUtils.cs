using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;

public static class ScalarFunctionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetValue<T>(this IScalarFunction<T> f, Scalar<T> t)
    {
        return f.ScalarProcessor.ScalarFromValue(
            f.GetValue(t.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDerivativeValue<T>(this IScalarFunction<T> f, Scalar<T> t)
    {
        return f.ScalarProcessor.ScalarFromValue(
            f.GetDerivativeValue(t.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDerivativeValue<T>(this IScalarFunction<T> f, Scalar<T> t, int order)
    {
        return f.ScalarProcessor.ScalarFromValue(
            f.GetDerivativeValue(t.ScalarValue, order)
        );
    }
}