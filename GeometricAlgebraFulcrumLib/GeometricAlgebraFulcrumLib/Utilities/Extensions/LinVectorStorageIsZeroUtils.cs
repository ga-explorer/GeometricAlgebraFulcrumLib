using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinVectorStorageIsZeroUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.IsEmpty() || vectorStorage.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.IsEmpty() || vectorGradedStorage.GetScalars().All(scalarProcessor.IsZero);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vectorStorage, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(vectorStorage)
                : scalarProcessor.IsZero(vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalarProcessor.IsNearZero(vectorGradedStorage)
                : scalarProcessor.IsZero(vectorGradedStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.IsEmpty() || vectorStorage.GetScalars().All(scalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.IsEmpty() || vectorGradedStorage.GetScalars().All(scalarProcessor.IsNearZero);
        }
    }
}