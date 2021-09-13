using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinMatrixStorageAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Add(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Add(scalar, value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorDenseStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, ILinVectorStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade1, ILinVectorStorage<T> v1, uint grade2, ILinVectorStorage<T> v2)
        {
            return grade1 == grade2
                ? new LinVectorSingleGradeStorage<T>(
                    grade1,
                    scalarProcessor.Add(v1, v2)
                )
                : new LinVectorSparseGradedStorage<T>()
                    .AddList(grade1, v1)
                    .AddList(grade2, v2)
                    .GetCompactStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> v1, ILinVectorGradedStorage<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? LinVectorEmptyGradedStorage<T>.EmptyStorage 
                    : v2;

            return scalarProcessor
                .CreateVectorGradedStorageComposer()
                .SetTerms(v1)
                .AddTerms(v2)
                .RemoveZeroTerms()
                .CreateLinVectorGradedStorage();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, T scalar)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Add(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.MapScalars(value => scalarProcessor.Add(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixDenseStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixDenseStorage<T> v1, ILinMatrixDenseStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> v1, ILinMatrixStorage<T> v2)
        {
            return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> v1, ILinMatrixGradedStorage<T> v2)
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