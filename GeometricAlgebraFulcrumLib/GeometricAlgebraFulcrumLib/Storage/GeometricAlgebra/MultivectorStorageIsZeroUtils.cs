using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageIsZeroUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.IsEmpty() || mv.GetScalars().All(scalarProcessor.IsZero);
        }

        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, bool nearZeroFlag)
        {
            return mv.IsEmpty() ||
                nearZeroFlag
                   ? mv.GetScalars().All(scalarProcessor.IsNearZero)
                   : mv.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.IsEmpty() || mv.GetScalars().All(scalarProcessor.IsNearZero);
        }

    }
}