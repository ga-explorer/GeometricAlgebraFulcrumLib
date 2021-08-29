using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorNegativeUtils
    {
        


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenList)
        {
            return evenList.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.MapScalars(scalarProcessor.Negative);
        }


    }
}
