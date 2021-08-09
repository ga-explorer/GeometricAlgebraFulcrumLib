using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories
{
    public static class GaMultivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateGenericMultivector<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> storage)
        {
            return new GaMultivector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateGenericMultivector<T>(this IGaStorageMultivector<T> storage, IGaProcessor<T> processor)
        {
            return new GaMultivector<T>(processor, storage);
        }
    }
}
