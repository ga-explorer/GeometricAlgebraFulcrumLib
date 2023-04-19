using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public static class LinVectorStorageDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Divide(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        {
            return scalarProcessor.MapScalarsIndicesIntersection(vector1, vector2, scalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DivideInner<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1)
        {
            return scalarProcessor.Add(
                vector1
                    .GetIndexScalarRecords()
                    .Select(record => scalarProcessor.Divide(record.Scalar, record.Scalar))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DivideInner<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        {
            return scalarProcessor.Add(
                scalarProcessor
                    .MapScalarsIndicesIntersection(vector1, vector2, scalarProcessor.Divide)
                    .GetScalars()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesIntersection(v1, v2, scalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage, T scalar)
        {
            return vectorGradedStorage.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.MapScalars(s => scalarProcessor.Divide(scalar, s));
        }



        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ILinVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
        //{
        //    return vector.MapScalars(value => scalarProcessor.Divide(value, scalar));
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ILinVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
        //{
        //    return vector.MapScalars(value => scalarProcessor.Divide(scalar, value));
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ILinVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
        //{
        //    return scalarProcessor.MapScalarsIndicesIntersection(vector1, vector2, scalarProcessor.Divide);
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ILinVectorGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage, T scalar)
        //{
        //    return vectorGradedStorage.MapScalars(s => scalarProcessor.Divide(s, scalar));
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ILinVectorGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinVectorGradedStorage<T> vectorGradedStorage)
        //{
        //    return vectorGradedStorage.MapScalars(s => scalarProcessor.Divide(scalar, s));
        //}
    }
}