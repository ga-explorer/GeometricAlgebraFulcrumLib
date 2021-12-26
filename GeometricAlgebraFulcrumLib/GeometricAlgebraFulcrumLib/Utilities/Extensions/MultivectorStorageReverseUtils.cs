using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageReverseUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Reverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
    }
}