using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class VectorStorageComposerFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorGradedStorageComposer<T> CreateVectorGradedStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new VectorGradedStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorSparseStorageComposer<T> CreateVectorStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new VectorSparseStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorDenseStorageComposer<T> CreateVectorStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count)
        {
            return new VectorDenseStorageComposer<T>(scalarProcessor, count);
        }
    }
}