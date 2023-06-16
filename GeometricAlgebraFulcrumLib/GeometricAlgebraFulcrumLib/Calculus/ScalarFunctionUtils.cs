using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus
{
    public static class ScalarFunctionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetValue<T>(this IScalarFunction<T> f, Scalar<T> t)
        {
            return f.ScalarProcessor.CreateScalar(
                f.GetValue(t.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetDerivativeValue<T>(this IScalarFunction<T> f, Scalar<T> t)
        {
            return f.ScalarProcessor.CreateScalar(
                f.GetDerivativeValue(t.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetDerivativeValue<T>(this IScalarFunction<T> f, Scalar<T> t, int order)
        {
            return f.ScalarProcessor.CreateScalar(
                f.GetDerivativeValue(t.ScalarValue, order)
            );
        }
    }
}