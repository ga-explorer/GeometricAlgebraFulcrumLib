using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class KVectorStorageFactory
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasisScalar<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return KVectorStorage<T>.CreateKVectorScalar(scalarProcessor.ScalarOne);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this T scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this IScalarProcessor<T> scalarProcessor, uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this ILinVectorStorage<T> termsList, uint grade)
        {
            if (grade == 0)
            {
                return termsList.TryGetScalar(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(termsList),
                2 => BivectorStorage<T>.Create(termsList),
                _ => KVectorStorage<T>.CreateKVector(grade, termsList)
            };
        }
        
    }
}