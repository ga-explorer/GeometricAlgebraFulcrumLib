using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public static class LinMatrixStorageComposerFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixGradedStorageComposer<T> CreateMatrixGradedStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LinMatrixGradedStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixSparseStorageComposer<T> CreateMatrixStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LinMatrixSparseStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixDenseStorageComposer<T> CreateMatrixStorageComposer<T>(this IScalarProcessor<T> scalarProcessor, int count1, int count2)
        {
            return new LinMatrixDenseStorageComposer<T>(scalarProcessor, count1, count2);
        }
    }
}