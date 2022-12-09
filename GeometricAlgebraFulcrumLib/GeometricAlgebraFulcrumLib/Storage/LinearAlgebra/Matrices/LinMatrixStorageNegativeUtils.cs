using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public static class LinMatrixStorageNegativeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.MapScalars(scalarProcessor.Negative);
        }
    }
}
