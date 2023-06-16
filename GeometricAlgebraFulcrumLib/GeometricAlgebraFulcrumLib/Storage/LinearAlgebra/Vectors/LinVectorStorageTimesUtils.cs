using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public static class LinVectorStorageTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Times(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Times(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        {
            return scalarProcessor.MapScalarsIndicesIntersection(vector1, vector2, scalarProcessor.Times);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesInner<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1)
        {
            return scalarProcessor.Add(
                vector1
                    .GetIndexScalarRecords()
                    .Select(record => scalarProcessor.Times(record.Scalar, record.Scalar))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TimesInner<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        {
            return scalarProcessor.Add(
                scalarProcessor
                    .MapScalarsIndicesIntersection(vector1, vector2, scalarProcessor.Times)
                    .GetScalars()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesIntersection(v1, v2, scalarProcessor.Times);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage, T scalar)
        {
            return vectorGradedStorage.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

    }
}