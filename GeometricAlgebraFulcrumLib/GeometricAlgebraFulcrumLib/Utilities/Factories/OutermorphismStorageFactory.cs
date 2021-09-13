using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class OutermorphismStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismStorage<T> CreateOutermorphismStorage<T>(this ILinMatrixStorage<T> idToKVectorMatrix)
        {
            return OutermorphismStorage<T>.Create(idToKVectorMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismStorage<T> CreateOutermorphismStorage<T>(this ILinearAlgebraProcessor<T> linearProcessor, ILinMatrixStorage<T> idToKVectorMatrix)
        {
            return OutermorphismStorage<T>.Create(idToKVectorMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismGradedStorage<T> CreateOutermorphismStorage<T>(this ILinMatrixGradedStorage<T> gradeIndexToKVectorMatrix)
        {
            return OutermorphismGradedStorage<T>.Create(gradeIndexToKVectorMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismGradedStorage<T> CreateOutermorphismStorage<T>(this ILinearAlgebraProcessor<T> linearProcessor, ILinMatrixGradedStorage<T> gradeIndexToKVectorMatrix)
        {
            return OutermorphismGradedStorage<T>.Create(gradeIndexToKVectorMatrix);
        }
    }
}