using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid1, ILaMatrixEvenStorage<T> grid2)
        {
            return scalarProcessor.MapValuesKeysIntersection(grid1, grid2, scalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid, T scalar)
        {
            return grid.MapScalars(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaMatrixEvenStorage<T> grid)
        {
            return grid.MapScalars(value => scalarProcessor.Divide(scalar, value));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector, T scalar)
        {
            return vector.MapScalars(value => scalarProcessor.Divide(value, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaVectorEvenStorage<T> vector)
        {
            return vector.MapScalars(value => scalarProcessor.Add(scalar, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector1, ILaVectorEvenStorage<T> vector2)
        {
            return scalarProcessor.MapValuesKeysIntersection(vector1, vector2, scalarProcessor.Divide);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> gradedList, T scalar)
        {
            return gradedList.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.MapScalars(s => scalarProcessor.Divide(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> gradedGrid, T scalar)
        {
            return gradedGrid.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.MapScalars(s => scalarProcessor.Divide(scalar, s));
        }
    }
}