using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

public static class MultivectorStorageComposerFactory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorGradedStorageComposer<T> CreateMultivectorGradedStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorGradedStorageComposer<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorGradedStorageComposer<T> CreateVectorGradedStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorGradedStorageComposer<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorSparseStorageComposer<T> CreateVectorStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorSparseStorageComposer<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorDenseStorageComposer<T> CreateVectorStorageComposer<T>(this IScalarProcessor<T> scalarProcessor, int count)
    {
        return new LinVectorDenseStorageComposer<T>(scalarProcessor, count);
    }
}