using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public static class LinMatrixStorageAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, T scalar)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Add(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Add(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixDenseStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixDenseStorage<T> v1, ILinMatrixDenseStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> v1, ILinMatrixStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params ILinMatrixStorage<T>[] vectorsList)
        {
            return vectorsList.Aggregate(
                (ILinMatrixStorage<T>)LinMatrixEmptyStorage<T>.EmptyStorage,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ILinMatrixStorage<T>> vectorsList)
        {
            return vectorsList.Aggregate(
                (ILinMatrixStorage<T>)LinMatrixEmptyStorage<T>.EmptyStorage,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> v1, ILinMatrixGradedStorage<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty()
                    ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                    : v2;

            return scalarProcessor
                .CreateLinMatrixGradedStorageComposer()
                .SetTerms(v1)
                .AddTerms(v2)
                .RemoveZeroTerms()
                .CreateLinMatrixGradedStorage();
        }
    }
}