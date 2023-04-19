using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageCliffordConjugateUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, T mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.CliffordConjugateIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.CliffordConjugateIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.CliffordConjugateIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.CliffordConjugateIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.CliffordConjugateIsNegativeOfGrade);
        }
    }
}