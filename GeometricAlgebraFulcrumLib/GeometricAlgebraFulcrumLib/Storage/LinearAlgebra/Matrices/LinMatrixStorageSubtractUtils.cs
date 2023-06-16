using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public static class LinMatrixStorageSubtractUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, T scalar)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Subtract(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Subtract(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixDenseStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixDenseStorage<T> v1, ILinMatrixDenseStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> v1, ILinMatrixStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Subtract);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> v1, ILinMatrixGradedStorage<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty()
                    ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                    : scalarProcessor.Negative(v2);

            return scalarProcessor
                .CreateLinMatrixGradedStorageComposer()
                .SetTerms(v1)
                .SubtractTerms(v2)
                .RemoveZeroTerms()
                .CreateLinMatrixGradedStorage();
        }



    }
}