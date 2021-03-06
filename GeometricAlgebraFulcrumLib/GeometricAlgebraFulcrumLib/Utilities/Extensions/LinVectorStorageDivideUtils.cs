using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinVectorStorageDivideUtils
    {
        
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
    }
}