using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageConjugateEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EConjugate<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
    }
}