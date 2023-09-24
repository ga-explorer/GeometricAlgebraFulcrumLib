using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageReverseUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
    }
}