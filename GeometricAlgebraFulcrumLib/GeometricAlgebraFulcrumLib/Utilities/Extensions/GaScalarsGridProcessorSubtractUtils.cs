using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorSubtractUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Subtract(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaVectorEvenStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Subtract(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorDenseEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorDenseEvenStorage<T> v1, ILaVectorDenseEvenStorage<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> v1, ILaVectorEvenStorage<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint grade1, ILaVectorEvenStorage<T> v1, uint grade2, ILaVectorEvenStorage<T> v2)
        {
            return grade1 == grade2
                ? new LaVectorSingleGradeStorage<T>(
                    grade1,
                    scalarProcessor.Subtract(v1, v2)
                )
                : new LaVectorSparseGradedStorage<T>()
                    .AddList(grade1, v1)
                    .AddList(grade2, scalarProcessor.Negative(v2))
                    .GetCompactList();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> v1, ILaVectorGradedStorage<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? LaVectorEmptyGradedStorage<T>.EmptyList 
                    : scalarProcessor.Negative(v2);

            return scalarProcessor
                .CreateLaVectorGradedStorageComposer()
                .SetTerms(v1)
                .SubtractTerms(v2)
                .RemoveZeroTerms()
                .CreateLaVectorGradedStorage();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid, T scalar)
        {
            return grid.MapScalars(value => scalarProcessor.Subtract(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaMatrixEvenStorage<T> grid)
        {
            return grid.MapScalars(value => scalarProcessor.Subtract(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixDenseEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixDenseEvenStorage<T> v1, ILaMatrixDenseEvenStorage<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> v1, ILaMatrixEvenStorage<T> v2)
        {
            return scalarProcessor.MapValuesKeysUnion(v1, v2, scalarProcessor.Subtract);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> v1, ILaMatrixGradedStorage<T> v2)
        {
            if (v1.IsEmpty())
                return v2.IsEmpty() 
                    ? LaMatrixEmptyGradedStorage<T>.EmptyGrid 
                    : scalarProcessor.Negative(v2);

            return scalarProcessor
                .CreateLaMatrixGradedStorageComposer()
                .SetTerms(v1)
                .SubtractTerms(v2)
                .RemoveZeroTerms()
                .CreateLaMatrixGradedStorage();
        }
    }
}