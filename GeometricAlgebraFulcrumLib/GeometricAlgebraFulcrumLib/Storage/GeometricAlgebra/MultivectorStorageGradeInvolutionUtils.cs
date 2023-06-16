using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageGradeInvolutionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.GradeInvolutionIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.GradeInvolutionIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.GradeInvolutionIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.GradeInvolutionIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GradeInvolution<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.GradeInvolutionIsNegativeOfGrade);
        }
    }
}