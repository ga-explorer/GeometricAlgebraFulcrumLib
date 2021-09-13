using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class MatrixStorageComposerFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixGradedStorageComposer<T> CreateMatrixGradedStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new MatrixGradedStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixSparseStorageComposer<T> CreateMatrixStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new MatrixSparseStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixDenseStorageComposer<T> CreateMatrixStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count1, int count2)
        {
            return new MatrixDenseStorageComposer<T>(scalarProcessor, count1, count2);
        }
    }
}