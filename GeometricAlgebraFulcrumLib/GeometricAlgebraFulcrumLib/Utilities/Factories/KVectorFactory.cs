using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class KVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> storage)
        {
            return new KVector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this KVectorStorage<T> storage, IGeometricAlgebraProcessor<T> processor)
        {
            return new KVector<T>(processor, storage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVectorZero<T>(this IGeometricAlgebraProcessor<T> processor, uint grade)
        {
            return KVectorStorage<T>.CreateKVectorZero(grade).CreateKVector(processor);
        }

    }
}