using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorIsZeroUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenList)
        {
            return evenList.IsEmpty() || evenList.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.IsEmpty() || gradedList.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.IsEmpty() || evenGrid.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.IsEmpty() || gradedGrid.GetScalars().All(scalarProcessor.IsZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenList, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(evenList)
                : scalarProcessor.IsZero(evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> gradedList, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(gradedList)
                : scalarProcessor.IsZero(gradedList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> evenGrid, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(evenGrid)
                : scalarProcessor.IsZero(evenGrid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> gradedGrid, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(gradedGrid)
                : scalarProcessor.IsZero(gradedGrid);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenList)
        {
            return evenList.IsEmpty() || evenList.GetScalars().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.IsEmpty() || gradedList.GetScalars().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.IsEmpty() || evenGrid.GetScalars().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.IsEmpty() || gradedGrid.GetScalars().All(scalarProcessor.IsNearZero);
        }
    }
}