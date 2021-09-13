using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

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
        public static ILinVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Divide(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        {
            return scalarProcessor.MapScalarsIndicesIntersection(vector1, vector2, scalarProcessor.Divide);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage, T scalar)
        {
            return vectorGradedStorage.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.MapScalars(s => scalarProcessor.Divide(scalar, s));
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