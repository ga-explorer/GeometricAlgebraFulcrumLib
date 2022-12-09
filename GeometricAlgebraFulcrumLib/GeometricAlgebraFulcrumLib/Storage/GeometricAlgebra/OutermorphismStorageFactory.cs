using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public static class OutermorphismStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismStorage<T> CreateOutermorphismStorage<T>(this ILinMatrixGradedStorage<T> gradeIndexToKVectorMatrix)
        {
            return OutermorphismStorage<T>.Create(gradeIndexToKVectorMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismStorage<T> CreateOutermorphismStorage<T>(this ILinearAlgebraProcessor<T> linearProcessor, ILinMatrixGradedStorage<T> gradeIndexToKVectorMatrix)
        {
            return OutermorphismStorage<T>.Create(gradeIndexToKVectorMatrix);
        }
    }
}