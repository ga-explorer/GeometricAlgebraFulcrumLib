using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinMatrixStorageIsZeroUtils
    {
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.IsEmpty() || matrixStorage.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.IsEmpty() || matrixGradedStorage.GetScalars().All(scalarProcessor.IsZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(matrixStorage)
                : scalarProcessor.IsZero(matrixStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(matrixGradedStorage)
                : scalarProcessor.IsZero(matrixGradedStorage);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage)
        {
            return matrixStorage.IsEmpty() || matrixStorage.GetScalars().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.IsEmpty() || matrixGradedStorage.GetScalars().All(scalarProcessor.IsNearZero);
        }
    }
}