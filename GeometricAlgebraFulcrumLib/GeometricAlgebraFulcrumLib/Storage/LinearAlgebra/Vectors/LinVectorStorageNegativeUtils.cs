using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public static class LinVectorStorageNegativeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.MapScalars(scalarProcessor.Negative);
        }
    }
}