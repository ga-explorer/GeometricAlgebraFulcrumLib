using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinMatrixStorageDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
        {
            return scalarProcessor.MapScalarsIndicesIntersection(matrixStorage1, matrixStorage2, scalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, T scalar)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Divide(scalar, value));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage, T scalar)
        {
            return matrixGradedStorage.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.MapScalars(s => scalarProcessor.Divide(scalar, s));
        }
    }
}